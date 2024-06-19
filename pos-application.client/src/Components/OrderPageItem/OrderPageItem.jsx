<<<<<<< HEAD
import Quantity from "../Quantity/Quantity";
import "./OrderPageItem.css";

function OrderPageItem() {
  const item = {
    $id: "2",
    itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9",
    itemName: "Burger",
    quantity: 1,
    basePrice: 8,
    isInStock: true,
    ingredients: {
      $id: "3",
      $values: [
        {
          $id: "4",
          ingredientId: "081138ad-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Beef Patty",
          price: 3,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "5",
          ingredientId: "08113bed-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Lettuce",
          price: 0.5,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "6",
          ingredientId: "08113d75-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Tomato",
          price: 0.75,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "7",
          ingredientId: "08113e55-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Cheese",
          price: 1,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "8",
          ingredientId: "08113eff-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Onions",
          price: 0.5,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "9",
          ingredientId: "08113f88-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Pickles",
          price: 0.25,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "10",
          ingredientId: "08113ffb-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Ketchup",
          price: 0.2,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "11",
          ingredientId: "08114068-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Mustard",
          price: 0.2,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        },
        {
          $id: "12",
          ingredientId: "081140de-2b3b-11ef-8549-0ac6d44bbeb9",
          name: "Burger Bun",
          price: 1.5,
          quantity: 1,
          itemId: "0804eae0-2b3b-11ef-8549-0ac6d44bbeb9"
        }
      ]
    }
  };

  return (
    <div className="orderItem">
      <ul className="orderItemValues">
        <li className="itemName">{item.itemName}</li>
        <li className="itemQuantity">
          <Quantity quantity={item.quantity} />
        </li>
        <li className="itemTotalPrice">{item.basePrice * item.quantity}</li>
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
=======
import PropTypes from "prop-types";
import Quantity from "../Quantity/Quantity";
import "./OrderPageItem.css";
>>>>>>> POS-Auth

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