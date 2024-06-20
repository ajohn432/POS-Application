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

function OrderDiscount() {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [discount, setDiscount] = React.useState(0);

  const handleChange = e => {
    setDiscount(e.target.value);
  };

  return (
    <div className="discountDiv">
      <Button onClick={handleOpen}>Discounts</Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Text in a modal
          </Typography>
          <Typography
            component={"span"}
            id="modal-modal-description"
            sx={{ mt: 2 }}
          >
            <FormControl>
              <FormLabel id="demo-radio-buttons-group-label">
                Discounts
              </FormLabel>
              <RadioGroup
                aria-labelledby="demo-radio-buttons-group-label"
                name="radio-buttons-group"
                onChange={handleChange}
              >
                <FormControlLabel value="5" control={<Radio />} label="5%" />
                <FormControlLabel value="10" control={<Radio />} label="10%" />
                <FormControlLabel value="15" control={<Radio />} label="15%" />
                <FormControlLabel value="20" control={<Radio />} label="20%" />
                <FormControlLabel value="50" control={<Radio />} label="50%" />
                <Button onClick={handleClose}>Finish</Button>
              </RadioGroup>
            </FormControl>
          </Typography>
        </Box>
      </Modal>
    </div>
  );
}

export default OrderDiscount;
