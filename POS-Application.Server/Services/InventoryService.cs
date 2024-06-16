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

        public async Task AddBaseItemAsync(AddBaseItemRequest request)
        {
            //1. Create a new BaseBillItem
            var newItem = new BillItem
            {
                ItemId = Guid.NewGuid().ToString(),
                ItemName = request.ItemName,
                BasePrice = request.BasePrice,
                IsInStock = true,
                Ingredients = new List<BillItemLinkedIngredient>()
            };

            _dbContext.BaseBillItems.Add(newItem);
            await _dbContext.SaveChangesAsync();

            //2. Add ingredients to BaseIngredients if they do not exist
            foreach (var ingredient in request.Ingredients)
            {
                var existingIngredient = await _dbContext.BaseIngredients
                    .FirstOrDefaultAsync(i => i.Name == ingredient.Name);

                if (existingIngredient == null)
                {
                    existingIngredient = new Ingredient
                    {
                        IngredientId = Guid.NewGuid().ToString(),
                        Name = ingredient.Name,
                        Quantity = ingredient.Quantity,
                        Price = ingredient.Price
                    };

                    _dbContext.BaseIngredients.Add(existingIngredient);
                    await _dbContext.SaveChangesAsync();
                }

                //3. Link ingredients to the new BaseBillItem
                var linkedIngredient = new BillItemLinkedIngredient
                {
                    IngredientId = existingIngredient.IngredientId,
                    Name = existingIngredient.Name,
                    Price = existingIngredient.Price,
                    Quantity = ingredient.Quantity,
                    ItemId = newItem.ItemId,
                    BillItem = newItem
                };

                newItem.Ingredients.Add(linkedIngredient);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> MarkItemInStockAsync(string itemId)
        {
            var item = await _dbContext.BaseBillItems.FirstOrDefaultAsync(bi => bi.ItemId == itemId);
            if (item != null)
            {
                item.IsInStock = true;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> MarkItemOutOfStockAsync(string itemId)
        {
            var item = await _dbContext.BaseBillItems.FirstOrDefaultAsync(bi => bi.ItemId == itemId);
            if (item != null)
            {
                item.IsInStock = false;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #region Helpers

        public async Task<bool> IsCurrentEmployeeManagerAsync(string token)
        {
            //Extract EmployeeId from the token
            var tokenInfo = await _dbContext.Tokens.FirstOrDefaultAsync(t => t.Token == token);

            if (tokenInfo == null)
            {
                return false;
            }

            var employeeId = tokenInfo.EmployeeId;

            //Retrieve the employee's role from the Users table
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.EmployeeId == employeeId);

            if (user == null)
            {
                return false;
            }

            // Check if the role is 'Manager'
            return user.Role == "Manager";
        }

        #endregion Helpers
    }
}
