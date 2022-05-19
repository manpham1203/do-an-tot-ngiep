import React, { useEffect, useState } from "react";
import { Colors } from "./dataColor";
import api from "../../../apis/api";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import {
  BarChart,
  Bar,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  Cell,
  ResponsiveContainer,
  XAxis,
} from "recharts";

function CategoryChart(props) {
  const [data, setData] = useState([]);
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/category/categorychart`,
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, []);
  const [tab, setTab] = useState(false);
  return (
    <div className="w-full bg-white shadow-admin rounded-[8px]">
      <div className={`p-[20px] ${tab&&"shadow-admin"}  rounded-[8px] flex flex-row justify-between items-center cursor-pointer`} onClick={()=>setTab(!tab)}>
        <h2 className="text-[20px]">Sản phẩm theo danh mục</h2>{" "}
        {tab ? <BsDashLg /> : <BsPlusLg />}
      </div>
      <div className={`${!tab&&"hidden"} p-[20px]`}>
        <ResponsiveContainer width="100%" height={400}>
        <BarChart
          layout="horizontal"
          data={data}
          barSize={10}
        >
          <XAxis type="category" dataKey="name" hide />
          <YAxis type="number" />
          <Tooltip />
          <Legend
            layout="vertical"
            verticalAlign="middle"
            align="right"
            payload={data.map((item, index) => ({
              id: item.id,
              type: "square",
              value: `${item.name} (${item.productQty})`,
              color: Colors[index],
            }))}
          />
          <CartesianGrid strokeDasharray="3 3" />
          <Bar
            
            dataKey="productQty"
            fill="#8884d8"
            background={{ fill: "#eee" }}
          >
            {data.map((item, index) => {
              return <Cell key={`cell-${index}`} fill={Colors[index]} />;
            })}
          </Bar>
        </BarChart>
      </ResponsiveContainer>
      </div>
      
    </div>
  );
}

export default CategoryChart;
