import "./OrderIdentifier.css";

function OrderIdentifier({ name, id }) {
  return (
    <div className="orderIdentiferDiv">
      <p className="orderNames">Order Name: {name}</p>
      <p className="orderNames">Order ID: {id}</p>
      <hr className="orderIdentifierBar"></hr>
    </div>
  );
}

export default OrderIdentifier;
