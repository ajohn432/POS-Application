import "./MenuItem.css";

function MenuItem({ name = "Hamburger", price = 8.95, itemId = 0 }) {
  function addMenuItemToOrder() {
    //Make POST request to add this menu item to order via the itemId
    //Can add other parameters if needed
    console.log(`Making POST request to add ${name} to order.`);
  }

  return (
    <div onClick={addMenuItemToOrder} className="menuItemCard">
      <h1>{name}</h1>
      <h2>{price}</h2>
    </div>
  );
}

export default MenuItem;
