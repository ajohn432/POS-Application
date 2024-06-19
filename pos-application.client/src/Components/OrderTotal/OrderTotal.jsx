import PropTypes from "prop-types";

function OrderTotal({ cart }) {
  const calculateSubtotal = () => {
    return cart.reduce((total, item) => {
      const itemTotal = item.basePrice * item.quantity;
      const ingredientsTotal = item.ingredients.reduce(
        (ingredientTotal, ingredient) => 
          ingredientTotal + ingredient.price * ingredient.quantity,
        0
      );
      return total + itemTotal + ingredientsTotal;
    }, 0);
  };

  return (
    <div className="orderTotalDiv">
      <p>Subtotal: {calculateSubtotal()}</p>
      <p>Discounts: </p>
      <p>Tax: </p>
      <p>Total: </p>
    </div>
  );
}

OrderTotal.propTypes = {
  cart: PropTypes.arrayOf(
    PropTypes.shape({
      itemName: PropTypes.string.isRequired,
      basePrice: PropTypes.number.isRequired,
      quantity: PropTypes.number.isRequired,
      ingredients: PropTypes.arrayOf(
        PropTypes.shape({
          name: PropTypes.string.isRequired,
          price: PropTypes.number.isRequired,
          quantity: PropTypes.number.isRequired,
        })
      ).isRequired,
    })
  ).isRequired,
};

export default OrderTotal;