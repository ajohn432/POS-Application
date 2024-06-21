import  { useEffect, useState } from "react";
import PropTypes from "prop-types";
import axios from "axios";
import OrderDiscount from "../OrderDiscount/OrderDiscount.jsx";
import OrderTip from "../OrderTip/OrderTip.jsx";
import PayNow from "../PayNow/PayNow.jsx";
import StockModal from "../StockModal/StockModal.jsx";
import { Button } from "@mui/material";
import "./OrderTotal.css";

function OrderTotal({ orderId, cart }) {
  const [amounts, setAmounts] = useState({
    preDiscountCost: 0,
    totalDiscountPercentage: 0,
    discountAmount: 0,
    billAfterDiscount: 0,
    taxRate: 0,
    taxAmount: 0,
    billAfterDiscountAndTax: 0,
    tipAmount: 0,
    finalBillAmount: 0,
  });
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [discountError, setDiscountError] = useState(null);
  const [stockModalOpen, setStockModalOpen] = useState(false);

  const calculateSubtotal = () => {
    return cart.reduce((total, cartItem) => {
      const itemTotal = cartItem.basePrice * cartItem.quantity;
      const ingredientsTotal = cartItem.ingredients.reduce(
        (ingredientSum, ingredient) =>
          ingredientSum + ingredient.price * ingredient.quantity,
        0
      );
      return total + itemTotal + ingredientsTotal;
    }, 0);
  };

  const fetchAmounts = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.get(
        `https://localhost:7007/api/orders/${orderId}/amounts`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      setAmounts(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching amounts:", error);
      setError("Error fetching amounts");
      setLoading(false);
    }
  };

  useEffect(() => {
    if (orderId) {
      fetchAmounts();
    }
  }, [orderId]);

  useEffect(() => {
    if (!loading) {
      const newSubtotal = calculateSubtotal();
      const taxAmount = newSubtotal * (amounts.taxRate / 100);
      const discountAmount = newSubtotal * (amounts.totalDiscountPercentage / 100);
      const finalBillAmount = newSubtotal + taxAmount - discountAmount + amounts.tipAmount;
      setAmounts((prevAmounts) => ({
        ...prevAmounts,
        preDiscountCost: newSubtotal,
        taxAmount: taxAmount,
        discountAmount: discountAmount,
        finalBillAmount: finalBillAmount,
      }));
    }
  }, [cart, amounts.totalDiscountPercentage, amounts.tipAmount]);

  const handleDiscountChange = async (discountCode) => {
    try {
      const token = localStorage.getItem("token");
      await axios.post(
        `https://localhost:7007/api/orders/${orderId}/discount`,
        { discountCode },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          }
        }
      );
      setDiscountError(null);
      await fetchAmounts();
    } catch (error) {
      console.error("Error applying discount:", error);
      setDiscountError("Invalid discount code");
    }
  };

  const handleTipChange = async (tipAmount) => {
    try {
      const token = localStorage.getItem("token");
      await axios.put(
        `https://localhost:7007/api/orders/${orderId}/tip`,
        { tipAmount },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          }
        }
      );
      await fetchAmounts();
    } catch (error) {
      console.error("Error applying tip:", error);
      setError("Error applying tip");
    }
  };

  const handleOpenStockModal = () => {
    setStockModalOpen(true);
  };

  const handleCloseStockModal = () => {
    setStockModalOpen(false);
  };

  if (!orderId) {
    return <div className="emptyCartMessage">Please start an order.</div>;
  }

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div className="orderTotalDiv">
      {discountError && <p className="error">{discountError}</p>}
      <p>Subtotal: {amounts.preDiscountCost.toFixed(2)}</p>
      <p>
        Tax ({amounts.taxRate}%): {amounts.taxAmount.toFixed(2)}
      </p>
      <p>
        Discount ({amounts.totalDiscountPercentage}%):{" "}
        {amounts.discountAmount.toFixed(2)}
      </p>
      <p>Tip: {amounts.tipAmount.toFixed(2)}</p>
      <p>Total: {amounts.finalBillAmount.toFixed(2)}</p>
      <div className="paymentButtons">
        <OrderDiscount onDiscountChange={handleDiscountChange} />
        <OrderTip onTipChange={handleTipChange} />
        <PayNow orderId={orderId} total={amounts.finalBillAmount} />
        <Button onClick={handleOpenStockModal}>Manage Stock</Button>
        <StockModal open={stockModalOpen} onClose={handleCloseStockModal} />
      </div>
    </div>
  );
}

OrderTotal.propTypes = {
  orderId: PropTypes.string.isRequired,
  cart: PropTypes.arrayOf(
    PropTypes.shape({
      basePrice: PropTypes.number.isRequired,
      quantity: PropTypes.number.isRequired,
      ingredients: PropTypes.arrayOf(
        PropTypes.shape({
          price: PropTypes.number.isRequired,
          quantity: PropTypes.number.isRequired,
        })
      ).isRequired,
    })
  ).isRequired,
};

export default OrderTotal;