import React, { useEffect, useState } from "react";
import OrderToday from "../../../components/Dashboard/OrderToday/OrderToday";
import CustomerBirthday from "../../../components/Dashboard/CustomerBirthday/CustomerBirthday";
import ProductCmt from "../../../components/Dashboard/ProductCmt/ProductCmt";
import PostCmt from "../../../components/Dashboard/PostCmt/PostCmt";

function Dashboard(props) {
  return (
    <div className=" flex flex-col gap-y-[25px]">
      <OrderToday />
      <ProductCmt />
      <PostCmt />
      <CustomerBirthday />
    </div>
  );
}

export default Dashboard;
