import "./App.css";
import OrderPage from "./Components/OrderPage/OrderPage.jsx";
import Menu from "./Components/Menu/Menu.jsx";
import { useState } from "react";

function App() {
  const [orderId, setOrderId] = useState("");
  const [orderHasStarted, setOrderHasStarted] = useState(false);
  const [selectedItem, setSelectedItem] = useState(null);

  const handleClick = id => {
    setOrderId(id);
    setOrderHasStarted(true);
  };

  const handleItemSelected = item => {
    setSelectedItem(item);
  };

  return (
    <div className="logoutContainer">
      <button className="logoutButton">Logout</button>
      <div className="mainContainer">
        <OrderPage sendToParent={handleClick} selectedItem={selectedItem} />
        {orderHasStarted && (
          <Menu id={orderId} onItemSelected={handleItemSelected} />
        )}
      </div>
    </div>
  );
}

export default App;
