import PropTypes from 'prop-types';
import './MenuItem.css';

function MenuItem({ name, price }) {
  return (
    <div className="menuItemCard">
      <h1> {name}</h1>
      <h2>Price: {price}</h2>
    </div>
  );
}

MenuItem.propTypes = {
  name: PropTypes.string.isRequired,
  price: PropTypes.number.isRequired
};

export default MenuItem;