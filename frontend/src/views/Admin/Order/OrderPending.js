import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../../components/Pagination/Pagination";
import Row from "./Row";
import ReactDOM from "react-dom";
import { useNavigate } from "react-router-dom";

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
function OrderPending(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const [limit, setLimit] = useState(10);
  const [query, setQuery] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const fetchData = async () => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/Order/AdminGetByState`,
      params: { currentPage: currentPage, limit: limit, id: query, state: 1 },
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(() => dispatch(fail()));
  };
  useEffect(() => {
    fetchData();
  }, [currentPage, limit, query]);
  const handleLimit = (value) => {
    const re = /^[0-9\b]+$/;
    if (value === "" || re.test(value)) {
      if (value > state.data.totalResult) {
        setLimit(state.data.totalResult);
        return;
      }
      setLimit(value);
    }
  };
  const blurLimit = (e) => {
    if (e.target.value === "") {
      setLimit(5);
    }
  };
  const [count, setCount] = useState(0);
  const handleClick = () => {
    setCount(count + 1);
  };
  useEffect(() => {
    fetchData();
  }, [count]);
  return (
    <>
      <div className="bg-white rounded-[8px] p-[20px] shadow-admin">
        <div className="p-[10px] gap-x-[25px] flex">
          <div className="flex flex-row justify-between w-full items-center">
            <div className="flex flex-row gap-x-[25px]">
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

              <div className="flex flex-col">
                <label
                  htmlFor="limit"
                  className="block mb-2 text-sm font-medium text-gray-900 "
                >
                  Số dòng
                </label>
                <div className="flex flex-row items-center">
                  <input
                    id="limit"
                    type="text"
                    // value={state.data.orders.length}
                    value={limit}
                    onChange={(e) => handleLimit(e.target.value)}
                    onBlur={(e) => blurLimit(e)}
                    className="w-[50px] bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
                  />
                  <span>/{state.data?.totalResult}</span>
                </div>
              </div>
            </div>
            <button
              onClick={handleClick}
              className="bg-second text-third rounded-md h-[40px] px-[20px]"
            >
              Làm mới
            </button>
          </div>
        </div>
        <div className=" border border-gray-600 rounded-[8px]">
          <table className="min-w-full text-sm divide-y divide-gray-600">
            <thead>
              <tr className="bg-white">
                <th className="sticky left-0 px-4 py-2 text-left bg-white">
                  <input
                    className="w-5 h-5 border-gray-200 rounded"
                    type="checkbox"
                    id="row_all"
                  />
                </th>
                <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
                  Mã đơn hàng
                </th>
                <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
                  Trạng thái đơn hàng
                </th>
                <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
                  Hành động
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-600">
              {state?.data?.orders?.map((item) => {
                return <Row key={item.id} id={item.id} fetchData={fetchData} />;
              })}
            </tbody>
          </table>
          <div className="bg-white px-4 py-3 flex items-center justify-between border-t border-gray-600 sm:px-6">
            <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
              <div>
                <p className="text-sm text-gray-700">
                  Hiển thị{" "}
                  <span className="font-medium">
                    {" "}
                    {state.data?.orders?.length}{" "}
                  </span>
                  trong
                  <span className="font-medium">
                    {" "}
                    {state.data?.totalResult}{" "}
                  </span>
                  kết quả
                </p>
              </div>
              <div>
                {currentPage > 0 && (
                  <Pagination
                    setCurrentPage={setCurrentPage}
                    totalPage={state.data?.totalPage}
                    itemsPerPage={state.data?.orders?.length}
                  />
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default OrderPending;
