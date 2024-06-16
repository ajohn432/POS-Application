using Microsoft.EntityFrameworkCore;
using POS_Application.Server.db;
using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;

namespace POS_Application.Server.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _dbContext;
        public InventoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<BillItem>> GetAllBillItemsWithIngredientsAsync()
        {
            return await _dbContext.BaseBillItems
                .Include(bi => bi.Ingredients)
                .ToListAsync();
        }
    }
}
