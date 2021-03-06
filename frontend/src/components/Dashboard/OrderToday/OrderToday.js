import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../Pagination/Pagination";
import Row from "./Row";
import Select from "react-select";
import Table from "../../Table/Table";
import Thead from "../../Table/Thead";
import Tr from "../../Table/Tr";
import Th from "../../Table/Th";
import Tbody from "../../Table/Tbody";
import { BsPlusLg, BsDashLg } from "react-icons/bs";

const orderOptions = [
  { value: 1, label: "Chờ xác nhận", color: "#2F4B60" },
  { value: 2, label: "Đang chuẩn bị hàng", color: "#2DCCBF" },
  { value: 3, label: "Đang vận chuyển", color: "#1A9487" },
  { value: 4, label: "Đã nhận hàng", color: "#9EBC4B" },
  { value: 0, label: "Đã huỷ", color: "#ED553B" },
];

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
    width: "250px",
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
    width: "250px",
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
function ProductCmt(props) {
  const [data, setData] = useState({ totalPage: 0, totalResult: 0, data: [] });
  const [loading, setLoading] = useState(true);
  const [limit, setLimit] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);
  const [orderStatus, setOrderStatus] = useState(null);
  const fetchData = async () => {
    setLoading(true);
    await api({
      method: "GET",
      url: `/Order/OrderToday`,
      params: {
        currentPage: currentPage,
        limit: limit,
        state: orderStatus?.value,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          setData({
            ...data,
            totalPage: res.data.totalPage,
            totalResult: res.data.totalResult,
            data: res.data.orderVMs,
          });
        }
        setLoading(false);
      })
      .catch(() => {
        setLoading(true);
      });
  };
  useEffect(() => {
    fetchData();
  }, [currentPage, limit, orderStatus]);
  const [show, setShow] = useState(true);
  return (
    <div className="w-full bg-white shadow-admin rounded-[8px]">
      <div
        className={`p-[20px] flex flex-row justify-between items-center cursor-pointer rounded-[8px] ${
          show && "shadow-admin"
        }`}
        onClick={() => setShow(!show)}
      >
        <h2 className="text-[20px]">
          Đơn hàng hôm nay{" "}
          <span className="text-red-500">({data.totalResult})</span>
        </h2>
        {show ? <BsDashLg /> : <BsPlusLg />}
      </div>
      <div className={`w-full p-[20px] ${!show && "hidden"}`}>
        <div className="flex flex-row gap-x-[20px] items-center mb-[20px]">
          <span>Lọc Đơn Hàng: </span>
          <Select
            className=" cursor-pointer "
            classNamePrefix="select"
            // defaultValue={orderOptions[0]}
            isClearable={true}
            isSearchable={false}
            name="orderStatus"
            value={orderStatus}
            onChange={(e) => setOrderStatus(e)}
            options={orderOptions}
            styles={colourStyles}
            placeholder="Tình trạng đơn hàng"
          />
        </div>
        <Table className="border-collapse w-full ">
          <Thead>
            <Tr>
              <Th className="">Mã Đơn hàng</Th>
              <Th className="w-[300px] ">Tình trạng</Th>
              <Th>Hành động</Th>
            </Tr>
          </Thead>
          <Tbody>
            {data?.data?.map((item) => {
              return <Row key={item.id} id={item.id} />;
            })}
          </Tbody>
        </Table>
        <div className="mt-[20px]">
          {loading ? (
            "loading"
          ) : (
            <Pagination
              forcePage={currentPage}
              setCurrentPage={setCurrentPage}
              totalPage={data?.totalPage}
              itemsPerPage={data?.orders?.length}
            />
          )}
        </div>
      </div>
    </div>
  );
}

export default ProductCmt;
