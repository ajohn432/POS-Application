import "./StartOrderForm.css";

function StartOrderForm() {
  return (
    <div className="startOrderFormDiv">
      <form className="startOrderForm">
        <label>Enter Order Name:</label>
        <input type="text"></input>
        <button>Submit</button>
      </form>
    </div>
  );
}

export default StartOrderForm;
