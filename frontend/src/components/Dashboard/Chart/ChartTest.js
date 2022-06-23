import React, { useEffect, useState } from "react";
import { Colors } from "./dataColor";
import api from "../../../apis/api";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  PieChart,
  Pie,
  Cell,
  Brush
} from "recharts";

function ChartTest(props) {
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
  console.log(data, "bar");
  return (
    <div className="">
      <BarChart
        width={500}
        height={300}
        data={data}
        margin={{
          top: 5,
          right: 30,
          left: 20,
          bottom: 5,
        }}
        barSize={20}
      >
        <XAxis dataKey="name" />
        <YAxis />
        <Tooltip />
        <Legend
          layout="vertical"
          verticalAlign="top"
          align="right"
          
          payload={data.map((item, index) => ({
            id: item.id,
            type: "square",
            value: `${item.name} (${item.productQty})`,
            color: Colors[index],
          }))}
        />
        <Brush dataKey="name" height={30} stroke="#8884d8" />
        <CartesianGrid strokeDasharray="3 3" />
        <Bar dataKey="productQty" fill="#8884d8" background={{ fill: "#eee" }}>
          {data.map((item, index) => {
            return <Cell key={`cell-${index}`} fill={Colors[index]} />;
          })}
        </Bar>
      </BarChart>

      {/* <PieChart width={400} height={400}>
        <Pie
          dataKey="value"
          isAnimationActive={false}
          data={data}
          cx="50%"
          cy="50%"
          outerRadius={80}
          fill="#8884d8"
          label
        />
        <Tooltip />
        <Legend dataKey="label" />
      </PieChart> */}
    </div>
  );
}

export default ChartTest;
