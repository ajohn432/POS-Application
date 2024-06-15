using Microsoft.EntityFrameworkCore;
using POS_Application.Server.db;
using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;
using POS_Application.Server.Helpers;

namespace POS_Application.Server.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;
        public OrderService(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<StartNewBillResponse> StartNewBillAsync(StartNewBillRequest request)
        {
            Bill bill = new Bill
            {
                BillId = Utilities.GenerateRandomId(10),
                CustomerName = request.CustomerName,
                Items = new(),
                Status = "New",
                Discounts = new(),
                TipAmount = 0
            };

            _dbContext.Bills.Add(bill);
            await _dbContext.SaveChangesAsync();

            StartNewBillResponse response = new StartNewBillResponse
            {
                OrderId = bill.BillId,
                OrderStatus = bill.Status
            };

            return response;
        }
    }
}
