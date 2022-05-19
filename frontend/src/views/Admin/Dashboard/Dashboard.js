import React, { useEffect, useState } from "react";
import OrderToday from "../../../components/Dashboard/OrderToday/OrderToday";
import CustomerBirthday from "../../../components/Dashboard/CustomerBirthday/CustomerBirthday";
import ProductCmt from "../../../components/Dashboard/ProductCmt/ProductCmt";
import PostCmt from "../../../components/Dashboard/PostCmt/PostCmt";
import ChartTest from "../../../components/Dashboard/Chart/ChartTest";
import CategoryChart from "../../../components/Dashboard/Chart/CategoryChart";
import BrandChart from "../../../components/Dashboard/Chart/BrandChart";
import OrderChart from "../../../components/Dashboard/Chart/OrderChart";
import ContactToday from "../../../components/Dashboard/ContactToday/ContactToday";

function Dashboard(props) {
  return (
    <div className=" flex flex-col gap-y-[25px]">
      {/* <ChartTest /> */}
      <OrderChart />
      <OrderToday />
      <CustomerBirthday />
      <ContactToday />
      <ProductCmt />
      <PostCmt />
      <BrandChart />
      <CategoryChart />
    </div>
  );
}

export default Dashboard;
