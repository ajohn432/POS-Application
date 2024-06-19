import "./App.css";
import OrderPage from "./Components/OrderPage/OrderPage.jsx";
import Menu from "./Components/Menu/Menu.jsx";
import { useState } from "react";

function App() {
  const [orderId, setOrderId] = useState("");
  const [orderHasStarted, setOrderHasStarted] = useState(false);
<<<<<<< HEAD
=======
  const [selectedItem, setSelectedItem] = useState(null);
>>>>>>> POS-Auth

  const handleClick = id => {
    setOrderId(id);
    setOrderHasStarted(true);
  };

<<<<<<< HEAD
  return (
    <div className="mainContainer">
      <OrderPage sendToParent={handleClick} />
      {orderHasStarted && <Menu id={orderId} />}
=======
  const handleItemSelected = item => {
    setSelectedItem(item);
  };

  return (
    <div className="mainContainer">
      <OrderPage sendToParent={handleClick} selectedItem={selectedItem} />
      {orderHasStarted && <Menu id={orderId} onItemSelected={handleItemSelected} />}
>>>>>>> POS-Auth
    </div>
  );
}

export default App;