import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";
import { toast } from "react-toastify";
import { useForm, Controller } from "react-hook-form";
import { FaRegEdit, FaRegTrashAlt } from "react-icons/fa";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import Row from "./Row";

const initState = {
  loading: false,
  fail: false,
  data: {
    products: [],
    totalPage: null,
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
          products: action.payload.products,
          totalPage: action.payload.totalPage,
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
function ProductTable(props) {
  const {
    handleSubmit,
    reset,
    watch,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
    register,
  } = useForm({
    mode: "onChange",
    defaultValues:{brand:[]}
  });
  const [state, dispatch] = useReducer(reducer, initState);
  const [activeFilter, setActiveFilter] = useState(false);
  const [currentPage, setCurrentPage]=useState(1);
  const [limit, setLimit]=useState(5);
  const [query, setQuery] = useState("");
  const [brand, setBrand] = useState();
  const watchBrand = watch("brand");
  console.log(watchBrand)
  const fetchData = async () => {
    const data = {
      currentPage: currentPage,
      limit: limit,
      search: query,
      brandSlug: watchBrand,
    };
    dispatch(loading());
    await api({
      method: "POST",
      url: `/Product/AllProductNameadmin`,
      params: {
        deleted: false,
      },
      data: data,
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(() => dispatch(fail()));
  };

  const fetchDataBrand = async () => {
    await api({
      method: "GET",
      url: `/Brand/allbrandnamedeleted`,
      params: { deleted: false },
    })
      .then((res) => {
        setBrand(res.data);
      })
      .catch(() => console.log("brand fail"));
  };

  useEffect(() => {
    fetchData();
    fetchDataBrand();
  }, [currentPage, limit, query, watchBrand]);


  return (
    <>
      <div className="bg-white mb-[20px] shadow-admin rounded-[8px] overflow-hidden">
        <div
          className="h-[50px] shadow-admin rounded-[8px] flex flex-row items-center justify-between px-[20px] cursor-pointer"
          onClick={() => setActiveFilter(!activeFilter)}
        >
          <h2 className="block font-medium text-[20px]  leading-[50px]">
            Bộ lọc
          </h2>
          {activeFilter ? <BsDashLg /> : <BsPlusLg />}
        </div>
        <div className={`${activeFilter ? null : "hidden"} mt-[20px]`}>
          <div className="flex flex-row w-full gap-x-[20px] px-[20px] mb-[20px]">
            <div className="w-full rounded-[8px] shadow-admin">
              <div className="h-[50px] shadow-admin rounded-[8px] flex flex-row items-center justify-between px-[20px] cursor-pointer">
                <h2 className="block font-medium text-[16px]  leading-[50px]">
                  Brand
                </h2>
                {activeFilter ? <BsDashLg /> : <BsPlusLg />}
              </div>
              <div>
                {brand?.map((item, index) => {
                  return (
                    <div key={item.id}
                      className={`${index === 0 ? "mt-[15px]" : "mt-[8px]"} ${
                        index === brand.length - 1 ? "mb-[15px]" : null
                      } px-[20px]`}
                    >
                      <label className="inline-flex items-center cursor-pointer">
                        <input
                          type="checkbox"
                          {...register("brand")}
                          value={item.slug}
                          className="form-checkbox hidden"
                        />
                        <div className="checkbox-box bg-white box-content w-[18px] h-[18px] p-[1px] border border-blue-500 flex items-center justify-center mr-[10px] rounded-[3px]"></div>
                        <span className="block text-sm font-medium text-gray-900 dark:text-gray-300">
                          {item.name}
                        </span>
                      </label>
                    </div>
                  );
                })}
              </div>
            </div>
            <div className="w-full rounded-full shadow-admin">category</div>
            <div className="w-full rounded-full shadow-admin">price</div>
          </div>
        </div>
      </div>
      <div className="bg-white rounded-[8px] p-[20px] shadow-admin">
        <div className="p-[10px] flex">
          <input
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            className="bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
          />
          <input
            type="text"
            value={limit}
            onChange={(e) => setLimit( e.target.value)}
          />
        </div>
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
                  Hình
                </th>
                <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
                  Tên sản phẩm
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
              {state.data.products.map((item) => {
                return <Row key={item.id} id={item.id} />;
              })}
            </tbody>
          </table>
          <div className="bg-white px-4 py-3 flex items-center justify-between border-t border-gray-200 sm:px-6">
            <div className="flex-1 flex justify-between sm:hidden">
              <a
                href="/"
                className="relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50"
              >
                Previous
              </a>
              <a
                href="/"
                className="ml-3 relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50"
              >
                Next
              </a>
            </div>
            <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
              <div>
                <p className="text-sm text-gray-700">
                  Showing <span className="font-medium">1</span> to{" "}
                  <span className="font-medium">10</span> of{" "}
                  <span className="font-medium">97</span> results
                </p>
              </div>
              <div>
                <nav
                  className="relative z-0 inline-flex rounded-md shadow-sm -space-x-px"
                  aria-label="Pagination"
                >
                  <a
                    href="/"
                    className="relative inline-flex items-center px-2 py-2 rounded-l-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50"
                  >
                    <span className="sr-only">Previous</span>
                  </a>
                  {/* Current: "z-10 bg-indigo-50 border-indigo-500 text-indigo-600", Default: "bg-white border-gray-300 text-gray-500 hover:bg-gray-50" */}

                  {[...Array(state.data.totalPage)].map((item, index) => {
                    return (
                      <span
                        onClick={() =>
                          setCurrentPage(index + 1)
                        }
                        key={index}
                        className="bg-white border-gray-300 text-gray-500 hover:bg-gray-50 relative inline-flex items-center px-4 py-2 border text-sm font-medium"
                      >
                        {index + 1}
                      </span>
                    );
                  })}
                  {/* <a
                href="/"
                aria-current="page"
                className="z-10 bg-indigo-50 border-indigo-500 text-indigo-600 relative inline-flex items-center px-4 py-2 border text-sm font-medium"
              >
                1
              </a> */}

                  <span className="relative inline-flex items-center px-4 py-2 border border-gray-300 bg-white text-sm font-medium text-gray-700">
                    ...
                  </span>
                  <a
                    href="/"
                    className="relative inline-flex items-center px-2 py-2 rounded-r-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50"
                  >
                    <span className="sr-only">Next</span>
                  </a>
                </nav>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default ProductTable;
