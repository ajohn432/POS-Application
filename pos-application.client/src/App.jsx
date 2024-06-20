import "./App.css";
import OrderPage from "./Components/OrderPage/OrderPage.jsx";
import Menu from "./Components/Menu/Menu.jsx";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function App() {
  const [orderId, setOrderId] = useState("");
  const [orderHasStarted, setOrderHasStarted] = useState(false);
  const [selectedItem, setSelectedItem] = useState(null);
  const navigate = useNavigate();

  const handleClick = id => {
    setOrderId(id);
    setOrderHasStarted(true);
  };

  const handleItemSelected = item => {
    setSelectedItem(item);
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  return (
    <div className="logoutContainer">
      <button className="logoutButton" onClick={handleLogout}>Logout</button>
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