using POS_Application.Server.Models;

namespace POS_Application.Server.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<List<BillItem>> GetAllBillItemsWithIngredientsAsync();
    }
}
