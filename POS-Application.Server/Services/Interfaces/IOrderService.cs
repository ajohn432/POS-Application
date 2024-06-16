using POS_Application.Server.Models;

namespace POS_Application.Server.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<StartNewBillResponse> StartNewBillAsync(StartNewBillRequest request);
        public Task<AddItemToBillResponse> AddItemToBillAsync(string orderId, AddItemToBillRequest request);
        public Task RemoveItemFromBillAsync(string orderId, string itemId);
        public Task<Bill> GetBillByOrderIdAsync(string orderId);
        public Task<ModifyItemOnBillResponse> ModifyItemOnBillAsync(string orderId, string itemId, ModifyItemOnBillRequest request);
        public Task<ModifyIngredientResponse> ModifyIngredientOnBillAsync(string orderId, string itemId, string ingredientId, ModifyIngredientRequest request);
        public Task<ApplyDiscountResponse> AddDiscountToBillAsync(string orderId, ApplyDiscountRequest request);
        public Task CancelBillAsync(string orderId);
        public Task<bool> ChangeTipAmountAsync(string orderId, decimal tipAmount);
    }
}
