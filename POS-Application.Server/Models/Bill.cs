using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POS_Application.Server.Models
{
    #region Bill Models

    public class Bill
    {
        public string BillId { get; set; }
        public string CustomerName { get; set; }
        public List<LinkedBillItem> Items { get; set; }
        public string Status { get; set; }
        public List<LinkedDiscount> Discounts { get; set; }
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

    #region Base Related Bill Classes
    public class BillItem
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsInStock { get; set; }
        public List<BillItemLinkedIngredient> Ingredients { get; set; }

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

    #region BillItem-linked Class
    public class BillItemLinkedIngredient
    {
        public string IngredientId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ItemId { get; set; }
        [JsonIgnore]
        public BillItem BillItem { get; set; }
    }
    #endregion BillItem-linked Class

    public class Discount
    {
        public string DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountPercentage { get; set; }
    }

    public class Ingredient
    {
        public string IngredientId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    #endregion Base Related Bill Classes

    #region Bill-linked Classes
    public class LinkedBillItem
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsInStock { get; set; }
        public List<LinkedIngredient> Ingredients { get; set; }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = BasePrice;
            foreach (var ingredient in Ingredients)
            {
                totalPrice += ingredient.Price;
            }
            return totalPrice * Quantity;
        }
        public string BillId { get; set; }
        [JsonIgnore]
        public Bill Bill { get; set; }
    }

    public class LinkedDiscount
    {
        public string DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string BillId { get; set; }
        public Bill Bill { get; set; }
    }

    public class LinkedIngredient
    {
        public string IngredientId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ItemId { get; set; }
        [JsonIgnore]
        public LinkedBillItem BillItem { get; set; }
    }

    #endregion Bill-linked Classes

    #endregion Bill Models

    #region Order Request/Response Models

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

    #region AddItemToBill

    public class AddItemToBillRequest
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class AddItemToBillResponse
    {
        public string OrderId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
    #endregion AddItemToBill

    #region ModifyItemOnBill

    public class ModifyItemOnBillRequest
    {
        public int Quantity { get; set; }
    }

    public class ModifyItemOnBillResponse
    {
        public string OrderId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }

    #endregion ModifyItemOnBill

    #region ModifyIngredient

    public class ModifyIngredientRequest
    {
        public int Quantity { get; set; }
    }

    public class ModifyIngredientResponse
    {
        public string OrderId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }


    #endregion ModifyIngredient

    #region ApplyDiscount

    public class ApplyDiscountRequest
    {
        public string DiscountCode { get; set; }
    }

    public class ApplyDiscountResponse
    {
        public string OrderId { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Status { get; set; }
    }

    #endregion ApplyDiscount

    #endregion Order Request/Response Models
}
