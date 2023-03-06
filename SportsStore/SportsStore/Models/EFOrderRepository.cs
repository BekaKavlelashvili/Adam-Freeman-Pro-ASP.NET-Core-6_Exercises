using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext _dbContext;

        public EFOrderRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Order> Orders => _dbContext.Orders.Include(x => x.Lines).ThenInclude(x => x.Product);

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.Lines.Select(x => x.Product));
            if (order.OrderId == 0)
            {
                _dbContext.Orders.Add(order);
            }

            _dbContext.SaveChanges();
        }
    }
}
