using Npgsql;

namespace Discount.Grpc.Migrate_Database;

public static class MigrateDatabase 
{
    public static int MigrateInitialDatabase(IConfiguration _configuration)
    {
		try
		{
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = new NpgsqlCommand { Connection = connection };
            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 1501);";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
            command.ExecuteNonQuery();
        }
		catch (Exception ex)
		{
			throw ex;
		}

        return 0;
    }
}
