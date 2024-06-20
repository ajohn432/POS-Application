import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormControl from "@mui/material/FormControl";
import FormLabel from "@mui/material/FormLabel";
import TextField from "@mui/material/TextField";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 1000,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4
};

function PayNow() {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <div className="discountDiv">
      <Button onClick={handleOpen}>Pay</Button>
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
          <Typography
            component={"span"}
            id="modal-modal-description"
            sx={{ mt: 2 }}
          >
            <span>Total: 39.00 </span>
            <FormControl>
              <TextField
                id="outlined-basic"
                label="Tip"
                variant="outlined"
                defaultValue="0"
              />
              <Button>Add Tip</Button>
            </FormControl>
            <span></span>
            <FormControl>
              <TextField
                id="outlined-basic"
                label="Name on Card"
                variant="outlined"
                defaultValue="0"
              />
              <TextField
                id="outlined-basic"
                label="Credit Card Number"
                variant="outlined"
                defaultValue="0"
              />
              <TextField
                id="outlined-basic"
                label="CVV"
                variant="outlined"
                defau
                ltValue="0"
              />
              <TextField
                id="outlined-basic"
                label="Expiration Date"
                variant="outlined"
                defaultValue="0"
              />
              <Button>Pay Now</Button>
            </FormControl>
          </Typography>
        </Box>
      </Modal>
    </div>
  );
}

export default PayNow;
