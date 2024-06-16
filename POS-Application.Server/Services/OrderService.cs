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
                Status = "Active",
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
            await IsOrderActive(orderId);

            //Fetch the bill from the database
            var bill = await _dbContext.Bills
                .Include(b => b.Items)
                    .ThenInclude(i => i.Ingredients)
                .FirstOrDefaultAsync(b => b.BillId == orderId);

            //Fetch the item details from the database based on the itemId in the request
            var item = await _dbContext.BaseBillItems
                .Include(i => i.Ingredients)
                .FirstOrDefaultAsync(i => i.ItemId == request.ItemId);
            if (item == null)
            {
                //Handle the case where the item is not found
                throw new ArgumentException("Item not found.");
            }

            //Create a new LinkedBillItem and add it to the bill
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


            //Save changes to the database
            await _dbContext.SaveChangesAsync();

            //Prepare the response
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

        public async Task RemoveItemFromBillAsync(string orderId, string itemId)
        {
            await IsOrderActive(orderId);

            //Fetch the bill from the database
            var bill = await _dbContext.Bills
                .Include(b => b.Items)
                    .ThenInclude(i => i.Ingredients)
                .FirstOrDefaultAsync(b => b.BillId == orderId);

            //Find the item in the bill
            var item = bill.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (item == null)
            {
                //Handle the case where the item is not found
                throw new ArgumentException("Item not found in the bill.");
            }

            //Remove the item from the bill
            bill.Items.Remove(item);

            //Save changes to the database
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Bill> GetBillByOrderIdAsync(string orderId)
        {
            //Fetch the bill from the database
            var bill = await _dbContext.Bills
                .Include(b => b.Items)
                    .ThenInclude(i => i.Ingredients)
                .Include(b => b.Discounts)
                .FirstOrDefaultAsync(b => b.BillId == orderId);

            if (bill == null)
            {
                throw new ArgumentException("Bill not found.");
            }

            return bill;
        }

        public async Task<ModifyItemOnBillResponse> ModifyItemOnBillAsync(string orderId, string itemId, ModifyItemOnBillRequest request)
        {
            await IsOrderActive(orderId);

            //Fetch the bill from the database
            var bill = await _dbContext.Bills
                .Include(b => b.Items)
                    .ThenInclude(i => i.Ingredients)
                .FirstOrDefaultAsync(b => b.BillId == orderId);

            //Find the item to modify in the bill's items
            var itemToModify = bill.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (itemToModify == null)
            {
                // Handle the case where the item is not found in the bill
                throw new ArgumentException("Item not found in the bill.");
            }

            //Update the quantity of the item
            itemToModify.Quantity = request.Quantity;

            //Save changes to the database
            await _dbContext.SaveChangesAsync();

            //Prepare the response
            var response = new ModifyItemOnBillResponse
            {
                OrderId = orderId,
                ItemId = itemId,
                Quantity = itemToModify.Quantity,
                Price = CalculateTotalCostOfLinkedBillItem(itemToModify),
                Status = "Item quantity modified successfully."
            };

            return response;
        }

        public async Task<ModifyIngredientResponse> ModifyIngredientOnBillAsync(string orderId, string itemId, string ingredientId, ModifyIngredientRequest request)
        {
            await IsOrderActive(orderId);

            var bill = await _dbContext.Bills
                .Include(b => b.Items)
                    .ThenInclude(i => i.Ingredients)
                .FirstOrDefaultAsync(b => b.BillId == orderId);

            var item = bill.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (item == null)
            {
                throw new ArgumentException("Item not found.");
            }

            var ingredient = item.Ingredients.FirstOrDefault(ing => ing.IngredientId == ingredientId);
            if (ingredient == null)
            {
                throw new ArgumentException("Ingredient not found.");
            }

            //Update the ingredient quantity
            ingredient.Quantity = request.Quantity;

            //Save changes to the database
            await _dbContext.SaveChangesAsync();

            //Prepare the response
            var response = new ModifyIngredientResponse
            {
                OrderId = orderId,
                ItemId = itemId,
                Quantity = ingredient.Quantity,
                Price = ingredient.Price * ingredient.Quantity,
                Status = "Ingredient modified successfully."
            };

            return response;
        }

        public async Task<ApplyDiscountResponse> AddDiscountToBillAsync(string orderId, ApplyDiscountRequest request)
        {
            await IsOrderActive(orderId);

            //Fetch the bill from the database
            var bill = await _dbContext.Bills
                .Include(b => b.Items)
                .Include(b => b.Discounts) //Include the Discounts navigation property
                .FirstOrDefaultAsync(b => b.BillId == orderId);

            //Fetch discount details based on the discount code.
            var discount = await _dbContext.BaseDiscounts.FirstOrDefaultAsync(d => d.DiscountCode == request.DiscountCode);
            if (discount == null)
            {
                throw new ArgumentException("Invalid discount code.");
            }

            //Check if the discount is already applied
            var existingDiscount = bill.Discounts.FirstOrDefault(d => d.DiscountCode == request.DiscountCode);
            if (existingDiscount != null)
            {
                throw new InvalidOperationException("This discount is already applied to the bill.");
            }

            //Convert the Discount object to LinkedDiscount
            var linkedDiscount = new LinkedDiscount
            {
                DiscountId = discount.DiscountId,
                DiscountCode = discount.DiscountCode,
                DiscountPercentage = discount.DiscountPercentage,
                BillId = bill.BillId
            };

            //Apply the discount to the bill
            bill.Discounts.Add(linkedDiscount);

            //Save changes to the database
            await _dbContext.SaveChangesAsync();

            //Prepare the response
            var response = new ApplyDiscountResponse
            {
                OrderId = bill.BillId,
                DiscountCode = request.DiscountCode,
                DiscountPercentage = discount.DiscountPercentage,
                Status = bill.Status
            };

            return response;
        }

        public async Task CancelBillAsync(string orderId)
        {
            await IsOrderActive(orderId);

            var bill = await _dbContext.Bills.FirstOrDefaultAsync(b => b.BillId == orderId);
            if (bill == null)
            {
                throw new ArgumentException("Bill not found.");
            }

            bill.Status = "Cancelled";
            await _dbContext.SaveChangesAsync();
        }

        #region Helpers
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

        private async Task IsOrderActive(string orderId)
        {
            var bill = await _dbContext.Bills.FirstOrDefaultAsync(o => o.BillId == orderId);
            if (bill == null)
            {
                throw new ArgumentException("Bill not found.");
            }
            else if (bill.Status != "Active")
            {
                throw new ArgumentException("Bill no longer Active.");
            }
        }

        #endregion Helpers
    }
}
