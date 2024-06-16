function OrderIdentifier({ orderId = "", orderName = "" }) {
  return (
    <div>
      <h1 className="orderH1">Order</h1>
      <p className="orderId">Order ID: {orderId}</p>
      <p className="orderName">Order Name: {orderName}</p>
    </div>
  );
}

export default OrderIdentifier;
