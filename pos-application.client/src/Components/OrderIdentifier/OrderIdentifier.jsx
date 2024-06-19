function OrderIdentifier({  orderName = "" }) {
  return (
    <div>
      <p className="orderName">Order Name: {orderName}</p>
    </div>
  );
}

export default OrderIdentifier;
