import axios from "axios";
import "./Menu.css";
import { useEffect, useState } from "react";
import MenuItem from "../MenuItem/MenuItem";
import PropTypes from "prop-types";
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";

function Menu({ onItemSelected }) {
  const token = localStorage.getItem("token");
  const [data, setData] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [snackbarOpen, setSnackbarOpen] = useState(false);

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

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  const handleItemClick = item => {
    if (item.isInStock) {
      onItemSelected(item);
    } else {
      setSnackbarOpen(true);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  const itemsToDisplay = data.slice(0, 12);

  return (
    <div className="menuDiv">
      <h1 className="menuHeader">Menu</h1>
      <hr className="menuBar"></hr>
      <div className="menu">
        {itemsToDisplay.length === 0 && <div>No items available</div>}
        {itemsToDisplay.map(item => {
          return (
            <MenuItem
              onClick={() => handleItemClick(item)}
              key={item.itemId}
              name={item.itemName}
              price={item.basePrice}
            />
          );
        })}
      </div>
      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleSnackbarClose}
      >
        <Alert onClose={handleSnackbarClose} severity="error">
          This item is out of stock!
        </Alert>
      </Snackbar>
    </div>
  );
}

Menu.propTypes = {
  onItemSelected: PropTypes.func.isRequired
};

export default Menu;