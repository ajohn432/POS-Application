import MenuItem from "../MenuItem/MenuItem.jsx";
import React, { useState, useEffect } from "react";
import axios from "axios";
import "./Menu.css";

function Menu() {
  // const token =
  //   "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdHJpbmciLCJqdGkiOiI3MzBmNmJmYy1kNjljLTQyYTYtODBkMy0yNGRhNDA3OTM0M2IiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNYW5hZ2VyIiwiZXhwIjoxNzE4NTU3Nzg2LCJpc3MiOiJKb2V5ISIsImF1ZCI6IlBPUy1Vc2VycyJ9.TqEN0LIPk5qXFcFcE11GD0VHZEMDtUrelJHBuYy6ZN0";

  // console.log("Token: " + token);

  // const fetchData = async () => {
  //   try {
  //     const response = await axios.get(
  //       "https://localhost:7007/api/inventory/items",
  //       {
  //         headers: {
  //           Authorization: `Bearer ${token}`
  //         }
  //       }
  //     );
  //     console.log("Data:", response.data);
  //     // Handle data as needed
  //   } catch (error) {
  //     console.error("Error fetching data:", error);
  //     // Handle error as needed
  //   }
  // };

  // fetchData();

  let menuItemsList = [
    {
      itemName: "Hamburger",
      basePrice: 8.75,
      itemId: 1
    },
    {
      itemName: "Smoothie",
      basePrice: 8.75,
      itemId: 2
    },
    {
      itemName: "Hotdog",
      basePrice: 8.75,
      itemId: 3
    },
    {
      itemName: "Chicken Sandwich",
      basePrice: 8.75,
      itemId: 4
    },
    {
      itemName: "Chicken Wings",
      basePrice: 8.75,
      itemId: 5
    },
    {
      itemName: "Milkshake",
      basePrice: 8.75,
      itemId: 6
    },
    {
      itemName: "Cheeseburger",
      basePrice: 8.75,
      itemId: 7
    },
    {
      itemName: "Steak",
      basePrice: 8.75,
      itemId: 1
    }
  ];

  return (
    <div className="menuItemList">
      {menuItemsList.map(e => {
        return (
          <MenuItem name={e.itemName} price={e.basePrice} itemId={e.itemId} />
        );
      })}
    </div>
  );
}

export default Menu;
