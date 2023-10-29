using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context
                            .Products
                            .Find(x => true)
                            .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context
                            .Products
                            .Find(x => x.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context
                        .Products
                        .InsertOneAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x=>x.Id, product.Id);
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var deleteResult = await _context
                                        .Products
                                        .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
