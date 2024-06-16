import { useEffect, useState } from "react";
import "./App.css";
import OrderPage from "./Components/OrderPage/OrderPage.jsx";
import Menu from "./Components/Menu/Menu.jsx";

function App() {
  return (
    <div className="mainContainer">
      <OrderPage />
      <Menu />
    </div>
  );
}

export default App;
