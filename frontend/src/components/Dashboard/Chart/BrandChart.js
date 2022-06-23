import React, { useEffect, useState } from "react";
import { Colors } from "./dataColor";
import api from "../../../apis/api";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import {
  Tooltip,
  Legend,
  PieChart,
  Pie,
  Cell,
  ResponsiveContainer,
} from "recharts";

function BrandChart(props) {
  const [data, setData] = useState([]);
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/brand/brandchart`,
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
  const [show, setShow] = useState(false);
  return (
    <div className="w-full  bg-white shadow-admin rounded-[8px]">
      <div
        className={`p-[20px] flex flex-row justify-between items-center cursor-pointer rounded-[8px] ${
          show && "shadow-admin"
        }`}
        onClick={() => setShow(!show)}
      >
        <h2 className="text-[20px]">Sản phẩm theo thương hiệu</h2>{" "}
        {show ? <BsDashLg /> : <BsPlusLg />}
      </div>
      <div className={`${!show && "hidden"} p-[20px]`}>
        <ResponsiveContainer width={500} height={300}>
          <PieChart layout="horizontal">
            <Pie
              dataKey="productQty"
              isAnimationActive={false}
              data={data}
              cx="50%"
              cy="50%"
              outerRadius={80}
              fill="#8884d8"
              label
            >
              {data.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={Colors[index]} />
              ))}
            </Pie>
            <Tooltip />
            <Legend
              layout="vertical"
              verticalAlign="middle"
              align="right"
              payload={data.map((item, index) => ({
                id: item.id,
                type: "circle",
                value: `${item.name} (${item.productQty})`,
                color: Colors[index],
              }))}
            />
          </PieChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
}

export default BrandChart;
