using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace YourNamespace.Tests
{
    [TestClass]
    public class ConnectionStringTest
    {
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; Database=rms; Uid=root; Pwd=aGibxMhi";

        [TestMethod]
        public void TestConnection()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Assert.AreEqual(connection.State, System.Data.ConnectionState.Open);
                }
                catch (MySqlException ex)
                {
                    Assert.Fail($"Failed to connect to the database with error message: {ex.Message}");
                }
            }
        }
    }
}