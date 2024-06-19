import "./App.css";
import OrderPage from "./Components/OrderPage/OrderPage.jsx";
import Menu from "./Components/Menu/Menu.jsx";
import { useState } from "react";

function App() {
  const [orderId, setOrderId] = useState("");
  const [orderHasStarted, setOrderHasStarted] = useState(false);

  const handleClick = id => {
    setOrderId(id);
    setOrderHasStarted(true);
  };

  return (
    <div className="mainContainer">
      <OrderPage sendToParent={handleClick} />
      {orderHasStarted && <Menu id={orderId} />}
    </div>
  );
}

export default App;
