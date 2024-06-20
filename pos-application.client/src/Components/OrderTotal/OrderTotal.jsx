import PropTypes from "prop-types";
import { useEffect, useState } from "react";
import axios from "axios";
import OrderDiscount from "../OrderDiscount/OrderDiscount.jsx";
import OrderTip from "../OrderTip/OrderTip.jsx";
import PayNow from "../PayNow/PayNow.jsx";
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
    finalBillAmount: 0
  });
  const [subtotal, setSubtotal] = useState(0);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  const fetchAmounts = async () => {
    try {
      const token = localStorage.getItem("token");
      console.log(`Fetching amounts for order ID: ${orderId}`);
      const response = await axios.get(
        `https://localhost:7007/api/orders/${orderId}/amounts`,
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );
      console.log("API response:", response);
      setAmounts(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching amounts:", error);
      setError("Error fetching amounts");
      setLoading(false);
    }
  };

  const fetchSubtotal = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.get(
        `https://localhost:7007/api/orders/${orderId}/calculate-bill-cost`,
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );
      setSubtotal(response.data);
    } catch (error) {
      console.error("Error fetching subtotal:", error);
      setError("Error fetching subtotal");
    }
  };

  useEffect(() => {
    if (orderId) {
      fetchAmounts();
      fetchSubtotal();
    }
  }, [orderId, cart]);

  const handleDiscountChange = async discountCode => {
    try {
      const token = localStorage.getItem("token");
      await axios.post(
        `https://localhost:7007/api/orders/${orderId}/discount`,
        { discountCode },
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );
      await fetchAmounts();
    } catch (error) {
      console.error("Error applying discount:", error);
      setError("Error applying discount");
    }
  };

  const handleTipChange = async tipAmount => {
    try {
      const token = localStorage.getItem("token");
      await axios.put(
        `https://localhost:7007/api/orders/${orderId}/tip`,
        { tipAmount },
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );
      await fetchAmounts();
    } catch (error) {
      console.error("Error applying tip:", error);
      setError("Error applying tip");
    }
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
      <p>Subtotal: {subtotal.toFixed(2)}</p>
      <p>
        Tax ({amounts.taxRate * 1}%): {amounts.taxAmount.toFixed(2)}
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
      </div>
    </div>
  );
}

OrderTotal.propTypes = {
  orderId: PropTypes.string.isRequired,
  cart: PropTypes.array.isRequired
};

export default OrderTotal;
