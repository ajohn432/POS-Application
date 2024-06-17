import "./MenuItem.css";

function MenuItem({ name, price, itemId }) {
  function addMenuItemToOrder() {
    //Make POST request to add this menu item to order via the itemId
    //Can add other parameters if needed
    console.log(`Making POST request to add ${name} to order.`);
  }

  return (
    <div className="menuItemCard">
      <h1>Name: {name}</h1>
      <h2>Price: {price}</h2>
    </div>
  );
}

export default MenuItem;
