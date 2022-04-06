import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { FaRegEdit, FaTrashRestoreAlt, FaTimes } from "react-icons/fa";

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
function CategoryTrash(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const fetchData = async () => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/category/GetAllcategoryDeleted`,
      params: { deleted: true },
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(dispatch(fail()));
  };
  useEffect(() => {
    fetchData();
  }, []);
  const navigate = useNavigate();
  
  const handleEdit = (slug) => {
    navigate(`/admin/chinh-sua-thuong-hieu/${slug}`);
  };
  const handleTrash = async (id) => {
    await api({
      method: "POST",
      url: `/category/deleted/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Khôi phục thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(id);
        } else {
          toast.error(`Khôi phục thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Khôi phục thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const handleDelete = async (id) => {
    await api({
      method: "Delete",
      url: `/category/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Xoá thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(id);
        } else {
          toast.error(`Xoá thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Xoá thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const handlePulished = async (id) => {
    await api({
      method: "POST",
      url: `/category/pulished/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          fetchData(id);
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
        <tbody className="divide-y divide-gray-100">
          {state.data.map((item) => {
            return (
              <tr key={item.id}>
                <td className="sticky left-0 px-4 py-2 ">
                  <input
                    className="w-5 h-5 border-gray-200 rounded"
                    type="checkbox"
                    id="row_1"
                  />
                </td>
                <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
                  {item.name}
                </td>
                <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
                  <div
                    className={`w-[50px] h-[25px]  flex items-center rounded-full relative
                  ${item.pulished ? "bg-blue-600 " : "bg-gray-300"}
                  transition-all duration-200 cursor-pointer
                  `}
                    onClick={() => handlePulished(item.id)}
                  >
                    <div
                      className={`w-[18px] h-[18px] bg-white rounded-full  absolute
                    ${item.pulished ? "ml-[28px]" : "ml-[4px]"}
                    transition-all duration-200
                    `}
                    ></div>
                  </div>
                </td>
                <td className="px-4 py-2 text-gray-700 whitespace-nowrap flex flex-row text-[25px] gap-x-[20px]">
                  <FaRegEdit
                    onClick={() => handleEdit(item.slug)}
                    className="cursor-pointer"
                  />
                  <FaTrashRestoreAlt
                    onClick={() => handleTrash(item.id)}
                    className="cursor-pointer"
                  />
                  <button
                    onClick={() => handleDelete(item.id)}
                    type="button"
                    className="bg-danger text-white p-[2px] rounded-md"
                  >
                    <FaTimes />
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default CategoryTrash;