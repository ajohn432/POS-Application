import PropTypes from "prop-types";
import "./Quantity.css";
import { useState, useEffect } from "react";

function Quantity({ quantity, onQuantityChange }) {
  const [quantityCount, setQuantityCount] = useState(quantity || 1);

  useEffect(() => {
    onQuantityChange(quantityCount);
  }, [quantityCount]);

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

Quantity.propTypes = {
  quantity: PropTypes.number.isRequired,
  onQuantityChange: PropTypes.func.isRequired,
};

export default Quantity;