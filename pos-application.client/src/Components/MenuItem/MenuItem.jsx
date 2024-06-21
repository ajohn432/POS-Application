import PropTypes from "prop-types";
import "./MenuItem.css";

function MenuItem({ name, price, onClick }) {
  return (
    <div className="menuItemCard" onClick={onClick}>
      <h1>{name}</h1>
      <h2>Price: ${price.toFixed(2)}</h2>
    </div>
  );
}

MenuItem.propTypes = {
  name: PropTypes.string.isRequired,
  price: PropTypes.number.isRequired,
  onClick: PropTypes.func.isRequired
};

export default MenuItem;
