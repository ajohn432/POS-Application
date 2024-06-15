using System.ComponentModel.DataAnnotations;

namespace POS_Application.Server.Models
{
    #region Bill Models

    public class Bill
    {
        public string BillId { get; set; }
        public string CustomerName { get; set; }
        public List<BillItem> Items { get; set; }
        public string Status { get; set; }
        public List<Discount> Discounts { get; set; }
        public decimal TipAmount { get; set; }

        public decimal CalculateTotalBillAmount()
        {
            decimal totalAmount = 0;
            foreach (var item in Items)
            {
                totalAmount += item.CalculateTotalPrice();
            }
            return totalAmount;
        }

        public decimal CalculateDiscountedTotalBillAmount()
        {
            decimal totalAmount = CalculateTotalBillAmount();
            decimal discountAmount = 0;

            if (Discounts != null && Discounts.Count > 0)
            {
                foreach (var discount in Discounts)
                {
                    discountAmount += (totalAmount * discount.DiscountPercentage) / 100;
                }
            }

            return totalAmount - discountAmount;
        }
    }

    public class BillItem
    {
        public string ItemId { get; set; }
        public string BillId { get; set; }
        public Bill Bill { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }

        public bool IsInStock { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = BasePrice;
            foreach (var ingredient in Ingredients)
            {
                totalPrice += ingredient.Price;
            }
            return totalPrice * Quantity;
        }
    }

    public class Discount
    {
        public string DiscountCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string BillId { get; set; }
        public Bill Bill { get; set; }
    }

    public class Ingredient
    {
        public string IngredientId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string ItemId { get; set; }
        public BillItem BillItem { get; set; }
    }

    #endregion Bill Models

    #region StartNewBill

    public class StartNewBillRequest
    {
        public string CustomerName { get; set; }
    }

    public class StartNewBillResponse
    {
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
    #endregion StartNewBill
}
