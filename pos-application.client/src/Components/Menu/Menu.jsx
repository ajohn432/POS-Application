import MenuItem from "../MenuItem/MenuItem.jsx";
import axios from "axios";
import "./Menu.css";
import React, { useEffect, useState } from "react";

function Menu() {
  const token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdHJpbmciLCJqdGkiOiI3MzBmNmJmYy1kNjljLTQyYTYtODBkMy0yNGRhNDA3OTM0M2IiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNYW5hZ2VyIiwiZXhwIjoxNzE4NTU3Nzg2LCJpc3MiOiJKb2V5ISIsImF1ZCI6IlBPUy1Vc2VycyJ9.TqEN0LIPk5qXFcFcE11GD0VHZEMDtUrelJHBuYy6ZN0";

  const [data, setData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7007/api/inventory/items",
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        setData(response.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      {data.$values.map(e => {
        console.log(e.$id);
        return <MenuItem name={e.itemName} price={e.basePrice} />;
      })}
    </div>
  );
}

export default Menu;
