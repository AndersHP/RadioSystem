using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace MotorolaRadioSystem.SystemTests
{
    public class TestClientProvider : IDisposable
    {
        private readonly TestServer _server;

        public TestClientProvider()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            Client = _server.CreateClient();
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}