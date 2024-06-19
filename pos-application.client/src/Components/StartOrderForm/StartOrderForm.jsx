<<<<<<< HEAD
=======
import PropTypes from "prop-types";
>>>>>>> POS-Auth
import "./StartOrderForm.css";
import axios from "axios";
import { useState } from "react";

function StartOrderForm(props) {
  const token = localStorage.getItem("token");
  const [orderName, setOrderName] = useState("");
  const { sendToParent } = props;

  const updateOrderName = e => {
    setOrderName(e.target.value);
  };

  const postOrderName = async e => {
    e.preventDefault();
    const url = "https://localhost:7007/api/orders";
    const data1 = {
      customerName: orderName
    };

    try {
      const response = await axios.post(url, data1, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });
      sendToParent(response.data.orderId.orderId, orderName);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="startOrderFormDiv">
      <label>Enter Order Name:</label>
      <input type="text" value={orderName} onChange={updateOrderName}></input>
      <button onClick={postOrderName}>Submit</button>
    </div>
  );
}

<<<<<<< HEAD
export default StartOrderForm;
=======
StartOrderForm.propTypes = {
  sendToParent: PropTypes.func.isRequired,
};

export default StartOrderForm;
>>>>>>> POS-Auth
