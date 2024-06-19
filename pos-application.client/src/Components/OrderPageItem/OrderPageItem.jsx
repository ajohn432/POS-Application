import PropTypes from "prop-types";
import Quantity from "../Quantity/Quantity";
import "./OrderPageItem.css";
import { useState } from "react";
import axios from "axios";

function OrderPageItem({ item, onAddToCart, orderId }) {
  const [itemQuantity, setItemQuantity] = useState(item.quantity || 1);
  const [ingredientQuantities, setIngredientQuantities] = useState(
    item.ingredients.$values.map(ingredient => ingredient.quantity || 1)
  );

  const handleItemQuantityChange = newQuantity => {
    setItemQuantity(newQuantity);
  };

  const handleIngredientQuantityChange = (index, newQuantity) => {
    setIngredientQuantities(prevQuantities => {
      const updatedQuantities = [...prevQuantities];
      updatedQuantities[index] = newQuantity;
      return updatedQuantities;
    });
  };

  const calculateTotalPrice = () => {
    const itemTotal = item.basePrice * itemQuantity;
    const ingredientsTotal = item.ingredients.$values.reduce(
      (total, ingredient, index) =>
        total + ingredient.price * ingredientQuantities[index],
      0
    );
    return itemTotal + ingredientsTotal;
  };

  const handleAddToCart = async () => {
    const itemToAdd = {
      itemId: item.itemId,
      quantity: itemQuantity,
    };

    try {
      const token = localStorage.getItem("token");
      console.log('Order ID before API call:', orderId, 'Type:', typeof orderId);
      const response = await axios.post(
        `https://localhost:7007/api/orders/${orderId}/items`,
        itemToAdd,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      console.log('adding item to order:', response.data);
      const cartItem = {
        ...response.data,
        itemName: item.itemName, 
        ingredients: item.ingredients.$values.map((ingredient, index) => ({
          ...ingredient,
          quantity: ingredientQuantities[index],
        })),
      };
      onAddToCart(cartItem);
    } catch (error) {
      console.error("Error adding item to order:", error);
    }
  };

  return (
    <div className="orderItem">
      <ul className="orderItemValues">
        <li className="itemName">{item.itemName}</li>
        <li className="itemQuantity">
          <Quantity
            quantity={itemQuantity}
            onQuantityChange={handleItemQuantityChange}
          />
        </li>
        <li className="itemTotalPrice">{calculateTotalPrice()}</li>
      </ul>
      <ul className="orderIngredients">
        {item.ingredients.$values.map((ingredient, index) => (
          <li key={ingredient.$id}>
            <Quantity
              quantity={ingredientQuantities[index]}
              onQuantityChange={newQuantity =>
                handleIngredientQuantityChange(index, newQuantity)
              }
            />
            {ingredient.name} (${ingredient.price})
          </li>
        ))}
      </ul>
      <button onClick={handleAddToCart}>Add to Cart</button>
    </div>
  );
}

OrderPageItem.propTypes = {
  item: PropTypes.shape({
    itemId: PropTypes.string.isRequired,
    itemName: PropTypes.string.isRequired,
    basePrice: PropTypes.number.isRequired,
    quantity: PropTypes.number,
    ingredients: PropTypes.shape({
      $values: PropTypes.arrayOf(
        PropTypes.shape({
          $id: PropTypes.string.isRequired,
          ingredientId: PropTypes.string.isRequired,
          name: PropTypes.string.isRequired,
          price: PropTypes.number.isRequired,
          quantity: PropTypes.number,
        })
      ).isRequired,
    }).isRequired,
  }).isRequired,
  onAddToCart: PropTypes.func.isRequired,
  orderId: PropTypes.string.isRequired,
};

export default OrderPageItem;