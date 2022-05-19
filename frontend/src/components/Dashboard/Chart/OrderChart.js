import React, { useEffect, useState } from "react";
import { Colors } from "./dataColor";
import api from "../../../apis/api";
import { BsPlusLg, BsDashLg } from "react-icons/bs";

import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";

const data1 = [
  {
    id: 1,
    name: "Tháng 1",
    value: null,
  },
  {
    id: 2,
    name: "Tháng 2",
    value: null,
  },
  {
    id: 3,
    name: "Tháng 3",
    value: 0,
  },
  {
    id: 4,
    name: "Tháng 4",
    value: 5,
  },
  {
    id: 5,
    name: "Tháng 5",
    value: 4,
  },
  {
    id: 5,
    name: "Tháng 5",
    value: null,
  },
];
function OrderChart(props) {
  const [data, setData] = useState([]);
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/Order/orderchart`,
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
  console.log("ê", data);
  const [tab, setTab] = useState(false);
  return (
    <div className="h-[500px]">
      <ResponsiveContainer width="100%" height="100%">
        <LineChart
          data={data1}
          margin={{
            top: 5,
            right: 30,
            left: 20,
            bottom: 5,
          }}
        >
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="name" />
          <YAxis
            tickFormatter={(value) =>
              new Intl.NumberFormat("en", {
                notation: "compact",
                compactDisplay: "short",
              }).format(value)
            }
          />
          <Tooltip />
          <Legend />
          <Line
            type="monotone"
            dataKey="value"
            stroke="#8884d8"
            activeDot={{ r: 8 }}
          />
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
}

export default OrderChart;
