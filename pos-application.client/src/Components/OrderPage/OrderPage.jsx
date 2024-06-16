import OrderIdentifier from "../OrderIdentifier/OrderIdentifier";
import OrderPageItem from "../OrderPageItem/OrderPageItem";

function OrderPage() {
  let orderId = 1;
  let orderName = "Erika";

  return (
    <div>
      <OrderIdentifier />
      <OrderPageItem />

      <h1 className="totalH1">Total</h1>
      <p>total amount</p>
      <button>Pay Now</button>
    </div>
  );
}

export default OrderPage;
