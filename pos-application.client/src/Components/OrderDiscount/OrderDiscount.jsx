import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import TextField from "@mui/material/TextField";
import PropTypes from "prop-types";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4
};

function OrderDiscount({ onDiscountChange }) {
  const [open, setOpen] = React.useState(false);
  const [discountCode, setDiscountCode] = React.useState("");

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const handleApplyDiscount = () => {
    onDiscountChange(discountCode);
    handleClose();
  };

  return (
    <span className="discountDiv">
      <Button onClick={handleOpen}>Discounts</Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Enter Discount Code
          </Typography>
          <Typography
            component={"span"}
            id="modal-modal-description"
            sx={{ mt: 2 }}
          >
            <TextField
              label="Discount Code"
              variant="outlined"
              value={discountCode}
              onChange={e => setDiscountCode(e.target.value)}
              fullWidth
            />
            <Button onClick={handleApplyDiscount} sx={{ mt: 2 }}>
              Apply Discount
            </Button>
          </Typography>
        </Box>
      </Modal>
    </span>
  );
}

OrderDiscount.propTypes = {
  onDiscountChange: PropTypes.func.isRequired
};

export default OrderDiscount;
