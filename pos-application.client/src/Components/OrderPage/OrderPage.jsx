import OrderIdentifier from "../OrderIdentifier/OrderIdentifier";
import OrderPageItem from "../OrderPageItem/OrderPageItem";
import OrderTotal from "../OrderTotal/OrderTotal.jsx";
import StartOrderForm from "../StartOrderForm/StartOrderForm.jsx";
import "./OrderPage.css";

function OrderPage() {
  let orderId = 1;
  let orderName = "Erika";

  return (
    <div className="orderPageDiv">
      <h1 className="orderH1">Order</h1>
      <StartOrderForm />
      <OrderIdentifier orderName={orderName} />
      <OrderPageItem />
      <OrderTotal />
    </div>
  );
}

export default OrderPage;
