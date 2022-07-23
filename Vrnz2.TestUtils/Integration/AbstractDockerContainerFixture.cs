using Docker.DotNet;
using Docker.DotNet.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Vrnz2.TestUtils.Integration
{
    public abstract class AbstractDockerContainerFixture : IDisposable
    {
        private readonly DockerClient _client;
        private readonly string _containerId;

        /// <summary>
        /// Local machine TCP port bonded to container listening port.
        /// </summary>
        public ushort ListeningPort { get; }

        /// <summary>
        /// Initializes a new instance of the concrete fixture class.
        /// Creates and starts a Docker container from a given image and binds a chosen container port to a random local machine port.
        /// The local machine port is randomly chosen by OS to avoid collisions with ports already in use.
        /// </summary>
        /// <param name="imageName">Image that container will be based.</param>
        /// <param name="containerListeningPort">
        /// Container TCP port that will be exposed on local machine.
        /// Docker CLI comparison: value will be set as '-p' argument in the format '<paramref name="hostPort"/> : <paramref name="containerListeningPort"/>'.
        /// </param>
        /// <param name="imageTag">
        /// Image tag.
        /// Docker CLI comparison: this value is concatenated with <paramref name="imageName"/> in the format '<paramref name="imageName"/>:<paramref name="imageTag"/>' and used as IMAGE argument.
        /// </param>
        /// <param name="environmentVariableMap">
        /// Key/value map containing environment variables to be set in the container. Key -> variable name / Value -> variable value.
        /// Docker CLI comparison: each key/value pair will be set as a '-e' argument in the format '{key}={value}'.
        /// </param>
        /// <param name="hostPort">
        /// Host TCP port that will be bound to <paramref name="containerListeningPort"/>.
        /// Use it only for debug purposes to avoid port binding conflicts when running tests.
        /// </param>
        protected AbstractDockerContainerFixture(string imageName, ushort containerListeningPort, string imageTag = "latest", IDictionary<string, string> environmentVariableMap = null, ushort hostPort = 0)
        {
            this._client = new DockerClientConfiguration()
                .CreateClient();

            this._client.Images.CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = imageName,
                Tag = imageTag
            },
            null,
            Mock.Of<IProgress<JSONMessage>>()).GetAwaiter().GetResult();

            IList<string> environmentVariableList = environmentVariableMap?.Select(e => $"{e.Key}={e.Value}")?.ToList();

            CreateContainerResponse response = this._client.Containers.CreateContainerAsync(new CreateContainerParameters()
            {
                Image = $"{imageName}:{imageTag}",
                Env = environmentVariableList,
                HostConfig = new HostConfig()
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>() { { $"{containerListeningPort}/tcp", new List<PortBinding>() { new PortBinding() { HostPort = hostPort.ToString() } } } }
                }
            }).GetAwaiter().GetResult();

            this._containerId = response.ID;

            this._client.Containers.StartContainerAsync(this._containerId, new ContainerStartParameters()).GetAwaiter().GetResult();

            // TODO: Docker container has a startup delay. Use another way to ensure container is running.
            Task.Delay(5000).GetAwaiter().GetResult();

            IEnumerable<ContainerListResponse> containerList = this._client.Containers.ListContainersAsync(new ContainersListParameters()
            {
                Filters = new Dictionary<string, IDictionary<string, bool>>()
                {
                    { "id", new Dictionary<string, bool>() { { this._containerId, true } } }
                }
            }).GetAwaiter().GetResult();

            // A container may contains more than one port exposed. We should return only the port bonded to a public port.
            Port containerPort = containerList.Single().Ports.Single(p =>
            {
                // The Docker API (Engine v20.0+) exposes container ports on both IPv4 and IPv6 addresses.
                // This filter ensures the chosen port is from an IPv4 address.
                IPAddress.TryParse(p.IP, out IPAddress containerIp);

                return (containerIp?.AddressFamily == AddressFamily.InterNetwork &&
                        p.PublicPort != 0);
            });

            this.ListeningPort = containerPort.PublicPort;
        }

        /// <summary>
        /// Stops and removes the Docker container.
        /// Docker CLI comparison: 'docker container stop {containerName}' and 'docker container rm {containerName}'.
        /// </summary>
        public void Dispose()
        {
            this._client?.Containers.RemoveContainerAsync(this._containerId, new ContainerRemoveParameters() { Force = true }).GetAwaiter().GetResult();
            this._client?.Dispose();
        }
    }
}
