using API.Models;

namespace API.Tests
{
    public class ConnectionStringTests : IDisposable
    {
        private readonly string? _original =
            Environment.GetEnvironmentVariable(ConnectionString.EnvironmentVariableName);

        public void Dispose() =>
            Environment.SetEnvironmentVariable(ConnectionString.EnvironmentVariableName, _original);

        [Fact]
        public void ReadsConnectionStringFromEnvironment()
        {
            const string value = "server=localhost;database=furniture;user=test;password=test";
            Environment.SetEnvironmentVariable(ConnectionString.EnvironmentVariableName, value);

            var connection = new ConnectionString();

            Assert.Equal(value, connection.cs);
        }

        [Fact]
        public void FallsBackToEmptyString_WhenNotConfigured()
        {
            Environment.SetEnvironmentVariable(ConnectionString.EnvironmentVariableName, null);

            var connection = new ConnectionString();

            Assert.Equal(string.Empty, connection.cs);
        }
    }
}
