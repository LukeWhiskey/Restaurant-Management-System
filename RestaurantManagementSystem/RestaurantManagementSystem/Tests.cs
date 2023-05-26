using Xunit;
using MySql.Data.MySqlClient;

namespace DbConnection.Test
{
    public class ConnectionStringTest
    {
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; Database=rms; Uid=root; Pwd=#######";

        [Fact]
        public void TestConnection()
        {
            // Arrange
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Act
                    connection.Open();

                    // Assert
                    Assert.Equal(System.Data.ConnectionState.Open, connection.State);

                    // Cleanup
                    connection.Close();
                }
                catch (MySqlException ex)
                {
                    // Assert.Fail will cause the test to fail
                    Assert.Fail($"Failed to connect to the database with error message: {ex.Message}");
                }
            }
        }
    }
}