using System.Collections.Generic;
using Vrnz2.TestUtils.Fixtures.Abstract;
using Vrnz2.TestUtils.Integration;

namespace Vrnz2.TestUtils.Tests.Fixtures
{
    public class MyTextFixture
        : AbstractFixture
    {
        #region Constructors

        public MyTextFixture()
            => Db = new MongoDocker("mongo", 27017, hostPort: 27025);

        #endregion

        #region Attributes

        public MongoDocker Db { get; }

        #endregion

        #region Methods

        public override void Dispose()
            => Db.Dispose();

        #endregion
    }

    public class MongoDocker
        : AbstractDockerContainerFixture
    {
        public MongoDocker(
            string imageName,
            ushort containerListeningPort,
            string imageTag = "latest",
            IDictionary<string, string> environmentVariableMap = null,
            ushort hostPort = 0)
            : base(imageName, containerListeningPort, imageTag, environmentVariableMap, hostPort)
        {
        }
    }
}
