import "./Quantity.css";
import { useState } from "react";

function Quantity({ quantity = 1 }) {
  const [quantityCount, setQuantityCount] = useState(quantity);

  const decreaseCount = () => {
    if (quantityCount > 0) {
      setQuantityCount(quantityCount - 1);
    }
  };

  const increaseCount = () => {
    setQuantityCount(quantityCount + 1);
  };

  return (
    <span className="buttonSpan">
      <button onClick={decreaseCount} className="decreaseQuantityBtn">
        -
      </button>{" "}
      {quantityCount}{" "}
      <button onClick={increaseCount} className="increaseQuantityBtn">
        +
      </button>
    </span>
  );
}

export default Quantity;