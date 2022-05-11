import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../../components/Pagination/Pagination";
import Row from "./Row";
import ReactDOM from "react-dom";
import { useNavigate } from "react-router-dom";
import Select, { StylesConfig } from "react-select";
import Table from "../../../components/Table/Table";
import Thead from "../../../components/Table/Thead";
import Th from "../../../components/Table/Th";
import Tbody from "../../../components/Table/Tbody";
import Tr from "../../../components/Table/Tr";

const initState = {
  loading: false,
  fail: false,
  data: {
    orders: [],
    totalPage: null,
    totalResult: null,
  },
};
//action
const LOADING = "LOADING";
const SUCCESS = "SUCCESS";
const FAIL = "FAIL";
const loading = () => {
  return {
    type: LOADING,
  };
};
const success = (payload) => {
  return {
    type: SUCCESS,
    payload: payload,
  };
};
const fail = () => {
  return {
    type: FAIL,
  };
};
const reducer = (state, action) => {
  switch (action.type) {
    case LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case SUCCESS:
      return {
        ...state,
        loading: false,
        fail: false,
        data: {
          ...state.data,
          orders: action.payload.orderVMs,
          totalPage: action.payload.totalPage,
          totalResult: action.payload.totalResult,
        },
      };
    case FAIL:
      return {
        ...state,
        loading: false,
        fail: true,
      };
    default:
      return state;
  }
};
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
function OrderTable(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const [query, setQuery] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [stateOrder, setStateOrder] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      dispatch(loading());
      await api({
        method: "GET",
        url: `/Order/AdminGetByState`,
        params: {
          currentPage: currentPage,
          limit: 10,
          id: query,
          state: stateOrder?.value,
        },
      })
        .then((res) => {
          if (res.status === 200) {
            dispatch(success(res.data));
          }
        })
        .catch(() => dispatch(fail()));
    };
    fetchData();
  }, [query, stateOrder, currentPage]);
  console.log(currentPage);
  return (
    <>
      <div className="bg-white rounded-[8px] p-[20px] shadow-admin">
        <div className="p-[10px] gap-x-[25px] flex">
          <div className="flex flex-row justify-between w-full items-center">
            <div className="w-full flex flex-row justify-between gap-x-[25px]">
              <div className="flex-col">
                <label
                  htmlFor="search"
                  className="block mb-2 text-sm font-medium text-gray-900 "
                >
                  Tìm kiếm
                </label>
                <input
                  id="search"
                  value={query}
                  onChange={(e) => setQuery(e.target.value)}
                  className="bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
                />
              </div>

              <div className="flex-col">
                <label
                  htmlFor="search"
                  className="block mb-2 text-sm font-medium text-gray-900 "
                >
                  Tình Trạng đơn hàng
                </label>
                <Select
                  className=" cursor-pointer "
                  classNamePrefix="select"
                  // defaultValue={orderOptions[0]}
                  isClearable={true}
                  isSearchable={false}
                  name="orderStatus"
                  // value={null}
                  onChange={(e) => setStateOrder(e)}
                  options={orderOptions}
                  styles={colourStyles}
                  placeholder="Tình Trạng Đơn Hàng"
                />
              </div>
            </div>
          </div>
        </div>
        <div className="w-full">
          <Table className="w-full">
            <Thead>
              <Tr>
                <Th className="w-[60px]">
                  <div className="w-full flex justify-center">
                    <input
                      className="w-5 h-5 border-gray-200 rounded"
                      type="checkbox"
                      id="row_all"
                      disabled
                    />
                  </div>
                </Th>
                <Th>Mã đơn hàng</Th>
                <Th className="w-[250px]">Trạng thái đơn hàng</Th>
                <Th className="w-[200px]">Hành động</Th>
              </Tr>
            </Thead>
            <Tbody>
              {state?.data?.orders?.map((item) => {
                return <Row key={item.id} id={item.id} />;
              })}
            </Tbody>
          </Table>
          <div className="bg-white px-4 py-3 flex items-center justify-between sm:px-6">
            <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
              <div>
                <p className="text-sm text-gray-700">
                  Hiển thị{" "}
                  <span className="font-medium">
                    {" "}
                    {state.data?.orders?.length}{" "}
                  </span>
                  Trong
                  <span className="font-medium">
                    {" "}
                    {state.data?.totalResult}{" "}
                  </span>
                  kết quả
                </p>
              </div>
              <div>
                <Pagination
                  setCurrentPage={setCurrentPage}
                  totalPage={state?.data?.totalPage}
                  itemsPerPage={state?.data?.orders?.length}
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default OrderTable;
