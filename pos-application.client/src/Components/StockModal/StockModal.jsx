import  { useEffect, useState } from "react";
import PropTypes from "prop-types";
import axios from "axios";
import Modal from "@mui/material/Modal";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";

const modalStyle = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

function StockModal({ open, onClose }) {
  const [items, setItems] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchItems = async () => {
      try {
        const token = localStorage.getItem("token");
        const response = await axios.get("https://localhost:7007/api/inventory/items", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setItems(response.data.$values);
      } catch (error) {
        setError("Error fetching items");
      }
    };

    if (open) {
      fetchItems();
    }
  }, [open]);

  const handleStockChange = async (itemId, inStock) => {
    try {
      const token = localStorage.getItem("token");
      const url = `https://localhost:7007/api/inventory/item/${itemId}/${inStock ? "in-stock" : "out-of-stock"}`;
      await axios.put(url, {}, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setItems((prevItems) =>
        prevItems.map((item) =>
          item.itemId === itemId ? { ...item, isInStock: inStock } : item
        )
      );
    } catch (error) {
      setError("Error changing stock status");
    }
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box sx={modalStyle}>
        <h2>Manage Stock</h2>
        {error && <p className="error">{error}</p>}
        <div className="stock-list">
          {items.map((item) => (
            <div key={item.itemId} className="stock-item">
              <span>{item.itemName}</span>
              <Button
                onClick={() => handleStockChange(item.itemId, true)}
                disabled={item.isInStock}
              >
                In Stock
              </Button>
              <Button
                onClick={() => handleStockChange(item.itemId, false)}
                disabled={!item.isInStock}
              >
                Out of Stock
              </Button>
            </div>
          ))}
        </div>
      </Box>
    </Modal>
  );
}

StockModal.propTypes = {
  open: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
};

export default StockModal;