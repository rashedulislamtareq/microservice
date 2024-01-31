using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var result = await _dbContext.Orders
                                        .Where(x => x.UserName == userName).ToListAsync();

        return result;
    }
}
