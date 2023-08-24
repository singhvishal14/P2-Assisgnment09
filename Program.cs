using System;
using System.Data;
using System.Data.SqlClient;

namespace Assignment9
{
    internal class Program
    {
        static string connectionString = "server=DESKTOP-FUGQNF4;database=OrderDB;trusted_connection=true;";

        static void Main(string[] args)
        {
            ExecutePlaceOrder();
        }
        static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        static void ExecutePlaceOrder()
        {
            using (SqlConnection connection = GetConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int orderId = GetNextOrderId(connection, transaction);
                    int customerId = 2;
                    decimal totalAmount = 2200.00M; // Replace with actual total amount

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "INSERT INTO Orders (OrderId, CustomerId, OrderDate, TotalAmount) VALUES (@OrderId, @CustomerId, GETDATE(), @TotalAmount)";

                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@CustomerId", customerId);
                        command.Parameters.AddWithValue("@TotalAmount", totalAmount);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    Console.WriteLine("Order Placed Successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    transaction.Rollback();
                }
            }
        }

        static int GetNextOrderId(SqlConnection connection, SqlTransaction transaction)
        {
            int nextOrderId = 0;

            using (SqlCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction; // Associate the existing transaction with the command
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT ISNULL(MAX(OrderId), 0) + 1 FROM Orders";
                nextOrderId = Convert.ToInt32(command.ExecuteScalar());
            }

            return nextOrderId;
        }
    }
}