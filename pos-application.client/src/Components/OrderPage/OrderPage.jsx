import OrderIdentifier from "../OrderIdentifier/OrderIdentifier";
import OrderPageItem from "../OrderPageItem/OrderPageItem";
import OrderTotal from "../OrderTotal/OrderTotal.jsx";
import StartOrderForm from "../StartOrderForm/StartOrderForm.jsx";
import "./OrderPage.css";
import { useState } from "react";

function OrderPage() {
  const [orderId, setOrderId] = useState("");
  const [orderName, setOrderName] = useState("");

  const handleClick = (id, name) => {
    setOrderId(id);
    setOrderName(name);
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
      <OrderPageItem />
      <OrderTotal />
    </div>
  );
}

export default OrderPage;
