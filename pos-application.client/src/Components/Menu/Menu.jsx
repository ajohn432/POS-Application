import PropTypes from "prop-types";
import axios from "axios";
import "./Menu.css";
import { useEffect, useState } from "react";
import MenuItem from "../MenuItem/MenuItem";

function Menu({ id, onItemSelected }) {
  const token = localStorage.getItem("token");
  const [data, setData] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7007/api/inventory/items",
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );

        const itemsArray = response.data.$values || [];

        // Kept getting an item that was titled "String"
        const validItems = itemsArray.filter(item => {
          return (
            typeof item.itemName === "string" &&
            item.itemName !== "string" &&
            typeof item.basePrice === "number"
          );
        });

        setData(validItems);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching data:", error);
        setError("Error fetching data");
        setLoading(false);
      }
    };

    fetchData();
  }, [token]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  const itemsToDisplay = data.slice(0, 8);

  const addToOrder = (item) => {
    onItemSelected(item);
  };

  return (
    <div>
      <h1 className="menuHeader">Menu</h1>
      <div className="menu">
        {itemsToDisplay.length === 0 && <div>No items available</div>}
        {itemsToDisplay.map(item => {
          return (
            <MenuItem
              onClick={() => addToOrder(item)}
              key={item.itemId}
              name={item.itemName}
              price={item.basePrice}
            />
          );
        })}
      </div>
    </div>
  );
}

Menu.propTypes = {
  id: PropTypes.string.isRequired,
  onItemSelected: PropTypes.func.isRequired,
};

export default Menu;
