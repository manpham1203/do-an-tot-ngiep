import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import Pagination from "../../../components/Pagination/Pagination";

import { FaRegEdit, FaTrashRestoreAlt, FaTimes } from "react-icons/fa";
import RowTrash from "./RowTrash";
import Table from "../../../components/Table/Table";
import Thead from "../../../components/Table/Thead";
import Th from "../../../components/Table/Th";
import Tbody from "../../../components/Table/Tbody";
import Tr from "../../../components/Table/Tr";

const initState = {
  loading: false,
  fail: false,
  data: {
    categories: [],
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
function CategoryTrash(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const [currentPage, setCurrentPage] = useState(1);
  const [query, setQuery] = useState("");
  const fetchData = async () => {
    const data = {
      currentPage: currentPage,
      search: query,
      limit: 10,
    };
    dispatch(loading());
    await api({
      method: "POST",
      url: `/Category/allcategorynameadmindeleted`,
      params: { deleted: true },
      data: data,
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(dispatch(fail()));
  };
  useEffect(() => {
    fetchData();
  }, [currentPage, query]);

  const handleDelete = async (id) => {
    await api({
      method: "Delete",
      url: `/category/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Xo?? th??nh c??ng`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(id);
        } else {
          toast.error(`Xo?? th???t b???i`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Xo?? th???t b???i`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  return (
    <div className="p-[20px] bg-white shadow-admin rounded-[8px]">
      <div className="p-[10px] gap-x-[25px] flex">
        <div className="flex flex-col">
          <label
            htmlFor="search"
            className="block mb-2 text-sm font-medium text-gray-900 "
          >
            T??m ki???m
          </label>
          <input
            id="search"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            className="bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
          />
        </div>
      </div>
      <div className="w-full">
        <Table className="w-full">
          <Thead>
            <Tr className="">
              <Th className="w-[50px]">
                <div className="flex justify-center">
                  <input
                    className="w-5 h-5 border-gray-200 rounded"
                    type="checkbox"
                    id="row_1"
                    disabled
                  />
                </div>
              </Th>
              <Th>T??n th????ng hi???u</Th>
              <Th>Ph??t h??nh</Th>
              <Th>H??nh ?????ng</Th>
            </Tr>
          </Thead>
          <Tbody className="divide-y divide-gray-600 bg-white">
            {state.data.categories.map((item) => {
              return (
                <RowTrash
                  key={item.id}
                  id={item.id}
                  handleDelete={handleDelete}
                />
              );
            })}
          </Tbody>
        </Table>
        <div className="bg-white px-4 py-3 flex items-center justify-between sm:px-6">
          <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
            <div>
              <p className="text-sm text-gray-700">
                Hi???n th???{" "}
                <span className="font-medium">
                  {" "}
                  {state.data?.brands?.length}{" "}
                </span>
                trong
                <span className="font-medium"> {state.data?.totalResult} </span>
                k???t qu???
              </p>
            </div>
            <div>
              {state.loading ? (
                "loading"
              ) : (
                <Pagination
                  forcePage={currentPage}
                  setCurrentPage={setCurrentPage}
                  totalPage={state?.data?.totalPage}
                  itemsPerPage={state.data?.categories?.length}
                />
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CategoryTrash;
