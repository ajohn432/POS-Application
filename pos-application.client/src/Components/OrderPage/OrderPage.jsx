import PropTypes from "prop-types";
import OrderIdentifier from "../OrderIdentifier/OrderIdentifier";
import OrderPageItem from "../OrderPageItem/OrderPageItem";
import OrderTotal from "../OrderTotal/OrderTotal.jsx";
import StartOrderForm from "../StartOrderForm/StartOrderForm.jsx";
import "./OrderPage.css";
import { useState } from "react";

function OrderPage(props) {
<<<<<<< HEAD
  const { sendToParent } = props;
=======
  const { sendToParent, selectedItem } = props;
>>>>>>> POS-Auth
  const [orderId, setOrderId] = useState("");
  const [orderName, setOrderName] = useState("");
  const [orderHasStarted, setOrderHasStarted] = useState();

  const handleClick = (id, name) => {
    setOrderId(id);
    setOrderName(name);
    sendToParent(orderId);
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
<<<<<<< HEAD
      <OrderPageItem />
=======
      {selectedItem && <OrderPageItem item={selectedItem} />}
>>>>>>> POS-Auth
      <OrderTotal />
    </div>
  );
}

OrderPage.propTypes = {
  sendToParent: PropTypes.func.isRequired,
  selectedItem: PropTypes.object,
};

export default OrderPage;