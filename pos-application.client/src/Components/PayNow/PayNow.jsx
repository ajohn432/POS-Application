import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import TextField from "@mui/material/TextField";
import PropTypes from "prop-types";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const style = {
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

function PayNow({ orderId = "", total = 0 }) {
  const [open, setOpen] = React.useState(false);
  const [creditCardNumber, setCreditCardNumber] = React.useState("");
  const [paymentResponse, setPaymentResponse] = React.useState(null);
  const [cancelResponse, setCancelResponse] = React.useState(null);
  const navigate = useNavigate();

  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setOpen(false);
    setPaymentResponse(null);
    setCancelResponse(null);
  };

  const handlePayNow = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.put(
        `https://localhost:7007/api/orders/${orderId}/pay`,
        { creditCardNumber },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setPaymentResponse(response.data);
      setTimeout(() => {
        navigate(0);
      }, 2000);
    } catch (error) {
      console.error("Error during payment:", error);
      setPaymentResponse("Payment failed. Please try again.");
    }
  };

  const handleCancelOrder = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.put(
        `https://localhost:7007/api/orders/${orderId}/cancel`,
        {},
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setCancelResponse(response.data);
      setTimeout(() => {
        navigate(0); 
      }, 2000);
    } catch (error) {
      console.error("Error during cancelation:", error);
      setCancelResponse("Cancelation failed. Please try again.");
    }
  };

  return (
    <div className="payNowDiv">
      <Button onClick={handleOpen}>Pay Now</Button>
      <Button onClick={handleCancelOrder} sx={{ mt: 2 }}>
        Cancel Order
      </Button>
      {cancelResponse && (
        <Typography sx={{ mt: 2 }}>{cancelResponse}</Typography>
      )}
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Payment
          </Typography>
          <Typography component={"span"} id="modal-modal-description" sx={{ mt: 2 }}>
            <p>Total: {total.toFixed(2)}</p>
            <TextField
              label="Credit Card Number"
              variant="outlined"
              value={creditCardNumber}
              onChange={(e) => setCreditCardNumber(e.target.value)}
              fullWidth
            />
            <Button onClick={handlePayNow} sx={{ mt: 2 }}>
              Pay Now
            </Button>
            {paymentResponse && (
              <Typography sx={{ mt: 2 }}>{paymentResponse}</Typography>
            )}
          </Typography>
        </Box>
      </Modal>
    </div>
  );
}

PayNow.propTypes = {
  orderId: PropTypes.string.isRequired,
  total: PropTypes.number.isRequired,
};

export default PayNow;