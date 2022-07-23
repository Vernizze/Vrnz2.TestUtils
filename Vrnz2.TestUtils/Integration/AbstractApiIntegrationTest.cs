using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Vrnz2.TestUtils.Integration
{
    public abstract class AbstractApiIntegrationTest<TRestApiIntegration> 
        : AbstractTest, IClassFixture<ApiMockFixture>, IDisposable
    {
        private readonly ApiMockFixture _fixture;
        private readonly string MappingsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "Stubs");

        /// <summary>
        /// Integration class to be tested. Should be initialized at concrete test class constructor.
        /// </summary>
        protected TRestApiIntegration Integration { get; set; }

        /// <summary>
        /// Provisioned Wire Mock server listening base address.
        /// </summary>
        protected string BaseAddress { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AbstractApiIntegrationTest(ApiMockFixture fixture)
        {
            this._fixture = fixture;

            this.BaseAddress = $"http://localhost:{fixture.ServerMock.Ports.Single()}";
        }

        /// <summary>
        /// Add a new mapping endpoint to the WireMock Server based on a json mapping file.
        /// This method will search for a folder called "Stubs" as default.
        /// </summary>
        /// <remarks>
        /// The file must have "CopyToOutputDirectory" setting as "PreserveNewest" on csproj file. 
        /// </remarks>
        /// <param name="path">File path starting after "Stubs/" folder. To maintain a pattern, the path should follow the Test Class Namespace.</param>
        /// <param name="mapping">New mapping file name. Needs to be a valid configuration in JSON formart. Should not include the ".json" extension.</param>
        public void AddMapping(string path, string mapping)
        {
            string completePath = Path.Combine(MappingsFolder, path, $"{mapping}.json");

            if (!File.Exists(completePath))
                throw new FileNotFoundException(null, completePath);

            _fixture.ServerMock.ReadStaticMappingAndAddOrUpdate(completePath);
        }

        /// <summary>
        /// Resets the mocked server configured behaviors.
        /// </summary>
        public void Dispose()
            => this._fixture.ServerMock.Reset();
    }
}
