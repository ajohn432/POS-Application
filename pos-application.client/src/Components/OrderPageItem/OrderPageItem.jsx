import PropTypes from "prop-types";
import Quantity from "../Quantity/Quantity";
import "./OrderPageItem.css";

function OrderPageItem({ item }) {
  return (
    <div className="orderItem">
      <ul className="orderItemValues">
        <li className="itemName">{item.itemName}</li>
        <li className="itemQuantity">
          <Quantity quantity={item.quantity || 1} />
        </li>
        <li className="itemTotalPrice">{item.basePrice}</li>
      </ul>
      <ul className="orderIngredients">
        {item.ingredients.$values.map(ingredient => {
          return (
            <li key={ingredient.$id}>
              <Quantity quantity={ingredient.quantity} />
              {ingredient.name}
            </li>
          );
        })}
      </ul>
    </div>
  );
}

OrderPageItem.propTypes = {
  item: PropTypes.shape({
    itemName: PropTypes.string.isRequired,
    basePrice: PropTypes.number.isRequired,
    ingredients: PropTypes.shape({
      $values: PropTypes.arrayOf(
        PropTypes.shape({
          $id: PropTypes.string.isRequired,
          name: PropTypes.string.isRequired,
          price: PropTypes.number.isRequired,
          quantity: PropTypes.number.isRequired,
        })
      ).isRequired,
    }).isRequired,
  }).isRequired,
};

export default OrderPageItem;