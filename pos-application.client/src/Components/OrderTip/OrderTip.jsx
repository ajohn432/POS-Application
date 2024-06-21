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

function OrderTip({ onTipChange }) {
  const [open, setOpen] = React.useState(false);
  const [tipAmount, setTipAmount] = React.useState(0);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const handleApplyTip = () => {
    onTipChange(tipAmount);
    handleClose();
  };

  return (
    <span className="tipDiv">
      <Button onClick={handleOpen}>Tip</Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Enter Tip Amount
          </Typography>
          <Typography
            component={"span"}
            id="modal-modal-description"
            sx={{ mt: 2 }}
          >
            <TextField
              label="Tip Amount"
              variant="outlined"
              type="number"
              value={tipAmount}
              onChange={e => setTipAmount(Number(e.target.value))}
              fullWidth
            />
            <Button onClick={handleApplyTip} sx={{ mt: 2 }}>
              Apply Tip
            </Button>
          </Typography>
        </Box>
      </Modal>
    </span>
  );
}

OrderTip.propTypes = {
  onTipChange: PropTypes.func.isRequired
};

export default OrderTip;
