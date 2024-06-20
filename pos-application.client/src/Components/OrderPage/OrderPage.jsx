import PropTypes from "prop-types";
import OrderIdentifier from "../OrderIdentifier/OrderIdentifier";
import OrderPageItem from "../OrderPageItem/OrderPageItem";
import OrderTotal from "../OrderTotal/OrderTotal.jsx";
import StartOrderForm from "../StartOrderForm/StartOrderForm.jsx";
import "./OrderPage.css";
import { useState } from "react";
import axios from "axios";

function OrderPage(props) {
  const { sendToParent, selectedItem } = props;
  const [orderId, setOrderId] = useState("");
  const [orderName, setOrderName] = useState("");
  const [cart, setCart] = useState([]);

  const handleClick = (id, name) => {
    setOrderId(id);
    setOrderName(name);
    sendToParent(id);
  };

  const handleAddToCart = item => {
    setCart(prevCart => [...prevCart, item]);
  };

  const handleRemoveFromCart = async itemId => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.delete(
        `https://localhost:7007/api/orders/${orderId}/items/${itemId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );
      console.log("removing item from order:", response.data);
      setCart(prevCart => prevCart.filter(item => item.itemId !== itemId));
    } catch (error) {
      console.error("Error removing item from order:", error);
    }
  };

  return (
    <div className="orderPageDiv">
      <h1 className="orderH1">Order</h1>
      <StartOrderForm sendToParent={handleClick} />
      <OrderIdentifier orderName={orderName} />
      <ul className="orderItemsHeaders">
        <li>Name</li>
        <li>Quantity</li>
        <li>Price</li>
      </ul>
      <h2 className="cartHeader">Cart:</h2>
      {cart.map((cartItem, index) => (
        <div key={index} className="cartItem">
          {cartItem.itemName} (
          {cartItem.ingredients && cartItem.ingredients.length > 0 ? (
            cartItem.ingredients.map((ingredient, idx) => (
              <span key={idx}>
                {ingredient.name} x{ingredient.quantity}
                {idx < cartItem.ingredients.length - 1 ? ", " : ""}
              </span>
            ))
          ) : (
            <span>No ingredients</span>
          )}
          )
          <button onClick={() => handleRemoveFromCart(cartItem.itemId)}>
            Remove
          </button>
        </div>
      ))}
      {selectedItem && (
        <OrderPageItem
          item={selectedItem}
          orderId={orderId}
          onAddToCart={handleAddToCart}
        />
      )}
      <OrderTotal orderId={orderId} cart={cart} />
    </div>
  );
}

OrderPage.propTypes = {
  sendToParent: PropTypes.func.isRequired,
  selectedItem: PropTypes.object
};

export default OrderPage;