import React, { useEffect, useState } from "react";
import { Colors } from "./dataColor";
import api from "../../../apis/api";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import Select from "react-select";
import {
  Brush,
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";

const colourStyles = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: data?.color,
    left: "0%",
    lineHeight: "1.3rem",
    marginLeft: "0rem",
  }),
  control: () => ({
    display: "flex",
    border: "1px solid #202121",
    height: "30px",
    width: "120px",
    borderRadius: "0px",
    background: "#fcfcfc",
    fontSize: "14px",
  }),
  option: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    fontSize: "14px",
    textAlign: "left",
    background: "white",
    borderBottom: "0.1rem solid #103D56",
    ":last-of-type": {
      borderBottom: "none",
    },
    ":hover": {
      background: "#f7f7f7",
      cursor: "pointer",
    },
    color: data?.color,
  }),
  menu: (styles) => ({
    ...styles,
    borderRadius: 0,
    width: "120px",
    border: "1px solid #202121",
    paddingTop: 0,
  }),
  input: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: data?.color,
  }),
  singleValue: (styles, { data }) => {
    return {
      ...styles,
      color: data?.color,
    };
  },
};
function OrderChart(props) {
  const [data, setData] = useState([]);
  const [dataCompare, setDataCompare] = useState([]);
  const date = new Date();
  const [listYear, setListYear] = useState([]);
  const [yearSelected, setYearSelected] = useState({
    label: `${date.getFullYear()}`,
    value: date.getFullYear(),
  });
  const [compare, setCompare] = useState([]);
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/Order/orderchart`,
      params: { year: yearSelected.value },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
        }
      })
      .catch(() => console.log("fail"));
  };
  const fetchDataCompare = async () => {
    await api({
      method: "POST",
      url: `/Order/CompareOrderChart`,
      data: compare,
    })
      .then((res) => {
        if (res.status === 200) {
          setDataCompare(res.data);
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, [yearSelected]);
  useEffect(() => {
    fetchDataCompare();
  }, [compare]);
  useEffect(() => {
    const newArr = [];
    for (var i = date.getFullYear(); i >= 2020; i--) {
      var obj = {
        label: `${i}`,
        value: i,
      };
      newArr.push(obj);
    }
    setListYear(newArr);
    setCompare([...compare, newArr[0].value]);
  }, []);

  const [show, setShow] = useState(true);
  const handleCompareChange = (event) => {
    var temp = parseInt(event.target.value);
    let newArray = [...compare, temp];
    if (compare.includes(temp)) {
      newArray = newArray.filter((x) => x !== temp);
    }
    setCompare(newArray);
  };
  const [tab, setTab] = useState(0);
  return (
    <div className="w-full bg-white shadow-admin rounded-[8px]">
      <div
        className={`p-[20px] ${
          show && "shadow-admin"
        }  rounded-[8px] flex flex-row justify-between items-center cursor-pointer`}
        onClick={() => setShow(!show)}
      >
        <h2 className="text-[20px]">Biểu đồ doanh thu</h2>{" "}
        {show ? <BsDashLg /> : <BsPlusLg />}
      </div>
      <div className={`${!show && "hidden"} p-[20px] w-full`}>
        <div className="w-full flex flex-row justify-center mb-[20px]">
          <h2
            onClick={() => setTab(0)}
            className={`text-[25px] w-[300px] text-center border-second  ${
              tab === 0 ? "border-b-[3px]": "border-b-[1px]"
            } cursor-pointer`}
          >
            Doanh thu theo năm
          </h2>
          <h2
            onClick={() => setTab(1)}
            className={`text-[25px] w-[300px] text-center border-second   ${
              tab === 1 ? "border-b-[3px]": "border-b-[1px]"
            } cursor-pointer`}
          >
            So sánh
          </h2>
        </div>
        {tab === 0 && (
          <div className="w-full">
            <div className="flex flex-row gap-x-[20px] items-center mb-[20px]">
              <span>Doanh thu theo năm: </span>
              <Select
                className=" cursor-pointer "
                classNamePrefix="select"
                // defaultValue={orderOptions[0]}
                isClearable={false}
                isSearchable={false}
                name="orderStatus"
                value={yearSelected}
                onChange={(e) => setYearSelected(e)}
                options={listYear}
                getOptionLabel={(e) => e.label}
                getOptionValue={(e) => e.value}
                styles={colourStyles}
                placeholder="Lọc đánh giá"
              />
            </div>
            <ResponsiveContainer width="100%" height={400}>
              <LineChart
                data={data}
                margin={{
                  top: 5,
                  right: 50,
                  left: 20,
                  bottom: 5,
                }}
              >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="month" />
                <YAxis
                  tickFormatter={(value) =>
                    new Intl.NumberFormat("en", {
                      notation: "compact",
                      compactDisplay: "short",
                    }).format(value)
                  }
                />
                <Tooltip />
                <Line
                  type="monotone"
                  dataKey="value"
                  stroke="royalblue"
                  activeDot={{ r: 8 }}
                />
                <Brush dataKey="month" stroke="royalblue" travellerWidth={20} />
              </LineChart>
            </ResponsiveContainer>
          </div>
        )}
        {tab === 1 && (
          <div className="w-full">
            <div className="flex flex-row gap-x-[25px] mb-[25px]">
              {listYear.map((item, index) => {
                return (
                  <label
                    key={index}
                    className="inline-flex items-center cursor-pointer"
                  >
                    <input
                      type="checkbox"
                      value={item.value}
                      id={item.value}
                      className="form-checkbox hidden "
                      onChange={handleCompareChange}
                      checked={compare.some((x) => x === item.value)}
                    />
                    <div className="checkbox-box bg-white box-content w-[18px] h-[18px] p-[1px] border border-second flex items-center justify-center mr-[10px] rounded-[3px]"></div>
                    <span className="block text-sm font-medium text-gray-900 dark:text-gray-300">
                      {item.label}
                    </span>
                  </label>
                );
              })}
            </div>
            <ResponsiveContainer width="100%" height={400}>
              <LineChart
                margin={{
                  top: 5,
                  right: 50,
                  left: 20,
                  bottom: 5,
                }}
              >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis
                  dataKey="month"
                  type="category"
                  allowDuplicatedCategory={false}
                />
                <YAxis
                  tickFormatter={(value) =>
                    new Intl.NumberFormat("en", {
                      notation: "compact",
                      compactDisplay: "short",
                    }).format(value)
                  }
                  dataKey="value"
                />
                <Tooltip />
                <Legend />
                {dataCompare.map((item, index) => (
                  <Line
                    type="monotone"
                    dataKey="value"
                    data={item?.data}
                    name={item?.year}
                    key={item?.id}
                    stroke={Colors[index]}
                  />
                ))}
                {/* <Brush dataKey="month" stroke="royalblue" travellerWidth={20} /> */}
              </LineChart>
            </ResponsiveContainer>
          </div>
        )}
      </div>
    </div>
  );
}

export default OrderChart;
