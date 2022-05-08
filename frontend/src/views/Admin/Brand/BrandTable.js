import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import Row from "./Row";
import Pagination from "../../../components/Pagination/Pagination";

import { FaRegEdit, FaRegTrashAlt } from "react-icons/fa";

const initState = {
  loading: false,
  fail: false,
  data: {
    brands: [],
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
        data: action.payload,
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
function BrandTable(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const navigate = useNavigate();
  const [currentPage, setCurrentPage] = useState(1);
  const [limit, setLimit] = useState(10);
  const [query, setQuery] = useState("");

  const fetchData = async () => {
    if (limit === "") return;
    const data = {
      currentPage: currentPage,
      search: query,
    };
    dispatch(loading());
    await api({
      method: "POST",
      url: `/Brand/allbrandnameadmindeleted`,
      params: { deleted: false },
      data: data,
    })
      .then((res) => {
        dispatch(success(res.data));
        setLimit(res.data.brands.length);
      })
      .catch(() => dispatch(fail()));
  };
  const fetchData1 = async () => {
    if (limit === "") return;
    const data = {
      currentPage: currentPage,
      limit: limit,
      search: query,
    };
    dispatch(loading());
    await api({
      method: "POST",
      url: `/Brand/allbrandnameadmindeleted`,
      params: { deleted: false },
      data: data,
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(() => dispatch(fail()));
  };
  useEffect(() => {
    fetchData();
  }, []);
  useEffect(() => {
    fetchData();
  }, [query]);
  useEffect(() => {
    fetchData1();
  }, [limit, currentPage]);

  const handlePublished = async (id) => {
    await api({
      method: "POST",
      url: `/brand/published/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          fetchData();
        } else {
          toast.error(`Thao tác thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thao tác thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const handleTrash = async (id) => {
    await api({
      method: "POST",
      url: `/brand/deleted/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Chuyển vào thùng rác thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData();
        } else {
          toast.error(`Thao tác thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thao tác thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const handleLimit = (value) => {
    const re = /^[0-9\b]+$/;
    if (value === "" || re.test(value)) {
      if (value > state.data.totalResult) {
        setLimit(state.data.totalResult);
      } else {
        setLimit(value);
      }
    }
  };
  const blurLimit = (e) => {
    if (e.target.value === "") {
      setLimit(state.data?.brands.length);
    }
  };

  return (
    <div className="p-[20px] bg-white shadow-admin rounded-[8px]">
      <div className="p-[10px] gap-x-[25px] flex">
        <div className="flex flex-col">
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
              value={limit}
              onChange={(e) => handleLimit(e.target.value)}
              onBlur={(e) => blurLimit(e)}
              className="w-[50px] bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
            />
            <span>/{state.data?.totalResult}</span>
          </div>
        </div>
      </div>
      <div className="overflow-hidden overflow-x-auto border border-gray-600 rounded-[8px] ">
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
                Tên thương hiệu
              </th>
              <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
                Phát hành
              </th>
              <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
                Hành động
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-600 bg-white">
            {state.data.brands.map((item) => {
              return <Row key={item.id} id={item.id} handleTrash={handleTrash} />;
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
                  {state.data?.brands?.length}{" "}
                </span>
                trong
                <span className="font-medium"> {state.data?.totalResult} </span>
                kết quả
              </p>
            </div>
            <div>
              <Pagination
                setCurrentPage={setCurrentPage}
                totalPage={state.data?.totalPage}
                itemsPerPage={state.data?.brands.length}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default BrandTable;
