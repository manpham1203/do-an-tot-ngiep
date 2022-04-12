import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { FaRegEdit, FaRegTrashAlt } from "react-icons/fa";
import Row from "./Row";

const initState = {
  loading: false,
  fail: false,
  data: [],
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
function CategoryTable(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const navigate = useNavigate();
  const fetchData = async () => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/Category/allcategorynamedeleted`,
      params: { deleted: false },
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(dispatch(fail()));
  };
  useEffect(() => {
    fetchData();
  }, []);

  const handlePublished = async (id) => {
    await api({
      method: "POST",
      url: `/category/published/${id}`,
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
  const handleEdit = (slug) => {
    navigate(`/admin/chinh-sua-danh-muc/${slug}`);
  };
  const handleTrash = async (id) => {
    await api({
      method: "POST",
      url: `/category/deleted/${id}`,
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

  return (
    <div className="overflow-hidden overflow-x-auto border border-gray-100 rounded-xl">
    
      <table className="min-w-full text-sm divide-y divide-gray-200">
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
              Tên danh mục
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Phát hành
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Hành động
            </th>
          </tr>
        </thead>
        <tbody className="divide-y divide-gray-100">
          {state.data.map((item) => {
            return (
              <Row key={item.id} id={item.id} />
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default CategoryTable;
