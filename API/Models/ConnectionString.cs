using System;

namespace API.Models
{
    /// <summary>
    /// Supplies the MySQL connection string from the FURNITURE_DB_CONNECTION
    /// environment variable, so database credentials never live in source control.
    /// </summary>
    public class ConnectionString
    {
        public const string EnvironmentVariableName = "FURNITURE_DB_CONNECTION";

        public string cs { get; set; }

        public ConnectionString()
        {
            cs = Environment.GetEnvironmentVariable(EnvironmentVariableName) ?? string.Empty;

            if (string.IsNullOrWhiteSpace(cs))
            {
                Console.WriteLine($"Database connection string not set. Set the {EnvironmentVariableName} environment variable.");
            }
        }
    }
}
