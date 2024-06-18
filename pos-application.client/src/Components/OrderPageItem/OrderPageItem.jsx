import IngredientQuantity from "../IngredientQuantity/IngredientQuantity";

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
      <p>{item.itemName}</p>
      {item.ingredients.$values.map(item => {
        return (
          <p>
            {item.name}
            <IngredientQuantity quantity={item.quantity} />
          </p>
        );
      })}
    </div>
  );
}

export default OrderPageItem;