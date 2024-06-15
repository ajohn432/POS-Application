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

        public async Task<AddItemToBillResponse> AddItemToBillAsync(string orderId, AddItemToBillRequest request)
        {
            // Fetch the bill from the database
            var bill = await _dbContext.Bills
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.BillId == orderId);

            if (bill == null)
            {
                // Handle the case where the bill is not found
                throw new ArgumentException("Bill not found.");
            }

            // Fetch the item details from the database based on the itemId in the request
            var item = await _dbContext.BaseBillItems
                .Include(i => i.Ingredients)
                .FirstOrDefaultAsync(i => i.ItemId == request.ItemId);
            if (item == null)
            {
                // Handle the case where the item is not found
                throw new ArgumentException("Item not found.");
            }

            // Create a new LinkedBillItem and add it to the bill
            var linkedBillItem = new LinkedBillItem
            {
                ItemId = Utilities.GenerateRandomId(10),
                ItemName = item.ItemName,
                Quantity = request.Quantity,
                BasePrice = item.BasePrice,
                IsInStock = item.IsInStock,
                Ingredients = item.Ingredients.Select(ing => new LinkedIngredient
                {
                    IngredientId = Utilities.GenerateRandomId(10),
                    Name = ing.Name,
                    Price = ing.Price,
                    Quantity = ing.Quantity,
                    ItemId = item.ItemId
                }).ToList(),
                BillId = bill.BillId
            };

            bill.Items.Add(linkedBillItem);


            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            // Prepare the response
            var response = new AddItemToBillResponse
            {
                OrderId = bill.BillId,
                ItemId = linkedBillItem.ItemId,
                Quantity = linkedBillItem.Quantity,
                Price = CalculateTotalCostOfLinkedBillItem(linkedBillItem),
                Status = "Item added to bill successfully."
            };

            return response;
        }

        private async Task<decimal> CalculateTotalCostOfLinkedBillItemAsync(string itemId)
        {
            var linkedBillItem = await _dbContext.BillLinkedBillItems
                .Include(i => i.Ingredients)
                .FirstOrDefaultAsync(i => i.ItemId == itemId);

            if (linkedBillItem == null)
            {
                throw new Exception("LinkedBillItem not found.");
            }

            decimal totalPrice = linkedBillItem.BasePrice;

            foreach (var ingredient in linkedBillItem.Ingredients)
            {
                totalPrice += ingredient.Price * ingredient.Quantity;
            }

            totalPrice *= linkedBillItem.Quantity;

            return totalPrice;
        }

        //Prefer this as it makes no calls to the db
        private decimal CalculateTotalCostOfLinkedBillItem(LinkedBillItem linkedBillItem)
        {
            if (linkedBillItem == null)
            {
                throw new Exception("LinkedBillItem not found.");
            }

            decimal totalPrice = linkedBillItem.BasePrice;

            foreach (var ingredient in linkedBillItem.Ingredients)
            {
                totalPrice += ingredient.Price * ingredient.Quantity;
            }

            totalPrice *= linkedBillItem.Quantity;

            return totalPrice;
        }
    }
}
