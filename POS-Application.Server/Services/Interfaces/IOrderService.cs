using POS_Application.Server.Models;

namespace POS_Application.Server.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<StartNewBillResponse> StartNewBillAsync(StartNewBillRequest request);
        //public Task<BillItem> AddItemToBillAsync(string orderId, BillItem item);
        //public Task<BillItem> RemoveItemFromBillAsync(string orderId, string itemId);
        //public Task<BillItem> ModifyItemOnBillAsync(string orderId, string itemId, int quantity);
        //public Task<Discount> AddDiscountToBillAsync(string orderId, string discountCode);
        //public Task<string> ClearBillAsync(string orderId);
    }
}
