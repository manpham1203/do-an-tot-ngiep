import React, { useEffect, useState } from "react";
import OrderToday from "../../../components/Dashboard/OrderToday/OrderToday";
import CustomerBirthday from "../../../components/Dashboard/CustomerBirthday/CustomerBirthday";

function Dashboard(props) {
  return (
    <div className=" flex flex-col gap-y-[25px]">
      <OrderToday />
      <CustomerBirthday />
    </div>
  );
}

export default Dashboard;
