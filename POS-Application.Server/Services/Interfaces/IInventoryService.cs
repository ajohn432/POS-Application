using POS_Application.Server.Models;

namespace POS_Application.Server.Services.Interfaces
{
    public interface IInventoryService
    {
        public Task<List<BillItem>> GetAllBillItemsWithIngredientsAsync();
        public Task<List<Ingredient>> GetAllIngredientsAsync();
        public Task AddBaseItemAsync(AddBaseItemRequest request);
        public Task<bool> MarkItemInStockAsync(string itemId);
        public Task<bool> MarkItemOutOfStockAsync(string itemId);
        public Task<GetItemStockStatusResponse> GetItemStockStatusAsync(string itemId);

        //Helpers
        public Task<bool> IsCurrentEmployeeManagerAsync(string token);
    }
}
