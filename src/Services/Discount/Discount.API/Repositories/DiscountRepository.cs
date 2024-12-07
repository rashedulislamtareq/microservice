using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            return coupon ?? new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("INSERT INTO COUPON (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount) ", new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            return (affected > 0);
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var query = "UPDATE COUPON SET ProductName = @ProductName , Description = @Description, Amount = @Amount WHERE Id = @Id";

            var affected = await connection.ExecuteAsync(query, new { Id = coupon.Id, ProductName = coupon.ProductName, Description = coupon.Description, coupon.Amount });

            return (affected > 0);
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var query = "DELETE FROM COUPON  WHERE ProductName = @ProductName";

            var affected = await connection.ExecuteAsync(query, new { ProductName = productName });

            return (affected > 0);
        }
    }
}
