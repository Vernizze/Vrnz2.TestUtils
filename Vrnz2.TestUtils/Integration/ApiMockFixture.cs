using System;
using WireMock.Server;

namespace Vrnz2.TestUtils.Integration
{
    public sealed class ApiMockFixture 
        : IDisposable
    {
        public WireMockServer ServerMock { get; }

        public ApiMockFixture()
            => ServerMock = WireMockServer.Start();

        public void Dispose()
            => ServerMock?.Dispose();
    }
}
