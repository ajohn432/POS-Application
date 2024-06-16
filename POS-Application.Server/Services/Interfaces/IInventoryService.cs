using POS_Application.Server.Models;

namespace POS_Application.Server.Services.Interfaces
{
    public interface IInventoryService
    {
        public Task<List<BillItem>> GetAllBillItemsWithIngredientsAsync();
        public Task AddBaseItemAsync(AddBaseItemRequest request);
        public Task<bool> MarkItemInStockAsync(string itemId);
        public Task<bool> MarkItemOutOfStockAsync(string itemId);

        //Helpers
        public Task<bool> IsCurrentEmployeeManagerAsync(string token);
    }
}
