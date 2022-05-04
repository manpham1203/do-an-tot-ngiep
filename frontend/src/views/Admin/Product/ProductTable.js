import React, { useEffect, useReducer, useState } from "react";
import api from "../../../apis/api";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";
import { toast } from "react-toastify";
import { useForm, Controller } from "react-hook-form";
import { FaRegEdit, FaRegTrashAlt } from "react-icons/fa";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import RowTable from "./Row";
import MultiRangeSlider from "../../../components/MultiRangeSlider/MultiRangeSlider";
import Pagination from "../../../components/Pagination/Pagination";
import Slider from "rc-slider";
import "rc-slider/assets/index.css";
import useDebounce from "../../../hooks/useDebounce";

const initState = {
  loading: false,
  fail: false,
  data: {
    products: [],
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
          products: action.payload.products,
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
function ProductTable(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const [activeFilter, setActiveFilter] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [limit, setLimit] = useState(10);
  const [query, setQuery] = useState("");
  const [brand, setBrand] = useState();
  const [category, setCategory] = useState();
  const [priceRange, setPriceRange] = useState([]);
  const [debouncePrice, setDebouncePrice] = useState([]);
  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncePrice(priceRange);
    }, 1000);
    return () => {
      clearTimeout(timer);
    };
  }, [priceRange]);
  const [priceRangeData, setPriceRangeData] = useState({
    maxPrice: 0,
    minPrice: 0,
  });
  const [filterTab, setFilterTab] = useState({
    category: false,
    brand: false,
    priceRange: false,
  });
  const [loadingP, setLoadingP] = useState(false);
  const [arrBrand, setArrBrand] = useState([]);
  const [arrCategory, setArrCategory] = useState([]);
  const fetchData = async () => {
    console.log("vo day");
    const data = {
      currentPage: currentPage,
      limit: limit,
      search: query,
      brandSlugs: arrBrand,
      categorySlugs: arrCategory,
      priceFrom: debouncePrice[0],
      priceTo: debouncePrice[1],
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
        // setLimit(res.data.products.length);
      })
      .catch(() => dispatch(fail()));
  };
  const fetchData1 = async () => {
    const data = {
      currentPage: currentPage,
      limit: limit,
      search: query,
      brandSlugs: arrBrand,
      categorySlugs: arrCategory,
      from: debouncePrice[0],
      to: debouncePrice[1],
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
  const fetchDataCategory = async () => {
    await api({
      method: "GET",
      url: `/category/allcategorynamedeleted`,
      params: { deleted: false },
    })
      .then((res) => {
        setCategory(res.data);
      })
      .catch(() => console.log("category fail"));
  };
  const fetchDataPriceRange = async () => {
    setLoadingP(true);
    await api({
      method: "GET",
      url: `/Product/pricerange`,
    })
      .then((res) => {
        setPriceRangeData({
          ...priceRangeData,
          maxPrice: res.data.maxPrice,
          minPrice: res.data.minPrice,
        });
        setPriceRange([res.data.minPrice, res.data.maxPrice]);
        setLoadingP(false);
      })
      .catch(() => console.log("brand fail"));
  };

  useEffect(() => {
    fetchData1();
  }, [limit, currentPage]);
  useEffect(() => {
    fetchData();
  }, [query, arrBrand, arrCategory, debouncePrice]);

  useEffect(() => {
    fetchDataBrand();
    fetchDataCategory();
    fetchDataPriceRange();
  }, []);
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
  const handleTrash = async (id) => {
    await api({
      method: "POST",
      url: `/product/deleted/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Chuyển vào thùng rác thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
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
  // useEffect(() => {
  //   if (state.data?.totalResult < limit) {
  //     setCurrentPage(1);
  //   }
  // }, [limit, state.data?.totalResult]);

  useEffect(() => {
    if (state.data.totalResult <= limit) {
      setCurrentPage(1);
    }
  }, [limit, state.data.totalResult]);

  const handleBrandChange = (event) => {
    let newArray = [...arrBrand, event.target.id];
    if (arrBrand.includes(event.target.id)) {
      newArray = newArray.filter((x) => x !== event.target.id);
    }
    setArrBrand(newArray);
  };
  const handleCategoryChange = (event) => {
    let newArray = [...arrCategory, event.target.id];
    if (arrCategory.includes(event.target.id)) {
      newArray = newArray.filter((x) => x !== event.target.id);
    }
    setArrCategory(newArray);
  };
  const [productSelect, setProductSelect] = useState([]);
  const [checkAll, setCheckAll] = useState(false);
  const handleProductSelect = (event) => {
    setCheckAll(false);
    let newArray = [...productSelect, event.target.id];
    if (productSelect.includes(event.target.id)) {
      newArray = newArray.filter((x) => x !== event.target.id);
    }
    if (newArray.length === state.data.products.length) {
      setCheckAll(true);
    }
    setProductSelect(newArray);
  };
  const handleSelectAll = (e) => {
    if (e.target.checked) {
      setCheckAll(true);
      let newArr = [];
      for (var i = 0; i < state.data.products.length; i++) {
        newArr.push(state.data.products[i].id);
      }
      setProductSelect(newArr);
    } else {
      setCheckAll(false);

      setProductSelect([]);
    }
  };
  console.log(productSelect);
  const handlePublishedTrueList = async () => {
    await api({
      method: "PUT",
      url: `/Product/PublishedTrueList`,
      data: productSelect,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Cập nhật thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          setShowTable(false);
          fetchData();
          setProductSelect([]);
          setCheckAll(false);
          setShowTable(true);
        } else {
          toast.error(`Cập nhật thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() =>
        toast.error(`Cập nhật thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        })
      );
  };
  const handlePublishedFalseList = async () => {
    await api({
      method: "PUT",
      url: `/Product/PublishedFalseList`,
      data: productSelect,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Cập nhật thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          setShowTable(false);
          fetchData();
          setProductSelect([]);
          setCheckAll(false);
          setShowTable(true);
        } else {
          toast.error(`Cập nhật thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() =>
        toast.error(`Cập nhật thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        })
      );
  };
  const handleDeletedTrueList = async () => {
    await api({
      method: "PUT",
      url: `/Product/DeletedTrueList`,
      data: productSelect,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Cập nhật thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          setShowTable(false);
          fetchData();
          setProductSelect([]);
          setCheckAll(false);
          setShowTable(true);
        } else {
          toast.error(`Cập nhật thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() =>
        toast.error(`Cập nhật thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        })
      );
  };
  const [showTable, setShowTable] = useState(true);
  console.log(state);
  return (
    <>
      <div className="py-[10px] shadow-admin rounded-[8px] flex flex-row items-center justify-end gap-x-[20px] px-[20px] mb-[20px]">
        <button
          onClick={handlePublishedTrueList}
          className="bg-blue-600 rounded-[8px] h-[40px] px-[20px] text-third"
        >
          Phát hành
        </button>
        <button
          onClick={handlePublishedFalseList}
          className="bg-gray-600 rounded-[8px] h-[40px] px-[20px] text-third"
        >
          Dừng phát hành
        </button>
        <button
          onClick={handleDeletedTrueList}
          className="bg-yellow-400 rounded-[8px] h-[40px] px-[20px] text-third"
        >
          Chuyển vào thùng rác
        </button>
      </div>
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
            <div className="w-full rounded-[8px] shadow-admin h-fit">
              <div
                onClick={() =>
                  setFilterTab({ ...filterTab, brand: !filterTab.brand })
                }
                className="h-[50px] shadow-admin rounded-[8px] flex flex-row items-center justify-between px-[20px] cursor-pointer"
              >
                <h2 className="block font-medium text-[16px]  leading-[50px]">
                  Thương hiệu
                </h2>
                {filterTab.brand ? <BsDashLg /> : <BsPlusLg />}
              </div>
              <div className={`${filterTab.brand ? null : "hidden"}`}>
                {brand?.map((item, index) => {
                  return (
                    <div
                      key={item.id}
                      className={`${index === 0 ? "mt-[15px]" : "mt-[8px]"} ${
                        index === brand.length - 1 ? "mb-[15px]" : null
                      } px-[20px]`}
                    >
                      <label className="inline-flex items-center cursor-pointer">
                        <input
                          type="checkbox"
                          value={item.id}
                          id={item.id}
                          className="form-checkbox hidden "
                          onChange={handleBrandChange}
                          checked={arrBrand.some((x) => x === item.id)}
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
            <div className="w-full rounded-[8px] shadow-admin h-fit">
              <div
                onClick={() =>
                  setFilterTab({ ...filterTab, category: !filterTab.category })
                }
                className="h-[50px] shadow-admin rounded-[8px] flex flex-row items-center justify-between px-[20px] cursor-pointer"
              >
                <h2 className="block font-medium text-[16px]  leading-[50px]">
                  Danh mục
                </h2>
                {filterTab.category ? <BsDashLg /> : <BsPlusLg />}
              </div>
              <div className={`${filterTab.category ? null : "hidden"}`}>
                {category?.map((item, index) => {
                  return (
                    <div
                      key={item.id}
                      className={`${index === 0 ? "mt-[15px]" : "mt-[8px]"} ${
                        index === category.length - 1 ? "mb-[15px]" : null
                      } px-[20px]`}
                    >
                      <label className="inline-flex items-center cursor-pointer">
                        <input
                          type="checkbox"
                          value={item.id}
                          id={item.id}
                          className="form-checkbox hidden "
                          onChange={handleCategoryChange}
                          checked={arrCategory.some((x) => x === item.id)}
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
            <div className="w-full rounded-[8px] shadow-admin h-fit">
              <div
                onClick={() =>
                  setFilterTab({
                    ...filterTab,
                    priceRange: !filterTab.priceRange,
                  })
                }
                className="h-[50px] shadow-admin rounded-[8px] flex flex-row items-center justify-between px-[20px] cursor-pointer"
              >
                <h2 className="block font-medium text-[16px]  leading-[50px]">
                  Khoảng giá
                </h2>
                {filterTab.priceRange ? <BsDashLg /> : <BsPlusLg />}
              </div>
              <div
                className={`mt-[15px] mb-[15px] px-[20px] ${
                  filterTab.priceRange ? null : "hidden"
                }`}
              >
                {loadingP ? (
                  ""
                ) : (
                  <>
                    <div className="flex flex-row mb-[25px]">
                      <div className="w-full">
                        Từ:{" "}
                        {new Intl.NumberFormat("vi-VN", {
                          style: "currency",
                          currency: "VND",
                        }).format(priceRange[0])}
                      </div>
                      <div className="mx-auto border-r border-gray-500 w-[1px]"></div>
                      <div className="w-full text-right">
                        Đến:{" "}
                        {new Intl.NumberFormat("vi-VN", {
                          style: "currency",
                          currency: "VND",
                        }).format(priceRange[1])}{" "}
                      </div>
                    </div>
                    <Slider
                      range
                      min={priceRangeData?.minPrice}
                      max={priceRangeData?.maxPrice}
                      value={priceRange}
                      onChange={setPriceRange}
                      step={100000}
                      className="filter"
                    />
                  </>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="bg-white rounded-[8px] p-[20px] shadow-admin">
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
                // value={state.data.products.length}
                value={limit}
                onChange={(e) => handleLimit(e.target.value)}
                onBlur={(e) => blurLimit(e)}
                className="w-[50px] bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
              />
              <span>/{state.data?.totalResult}</span>
            </div>
          </div>
        </div>
        <div className="overflow-hidden overflow-x-auto border border-gray-600 rounded-[8px]">
          {showTable && (
            <table className="min-w-full text-sm divide-y divide-gray-600">
              <thead>
                <tr className="bg-white">
                  <th className="sticky left-0 px-4 py-2 text-left bg-white">
                    <input
                      className="w-5 h-5 border-gray-200 rounded"
                      type="checkbox"
                      id="row_all"
                      name="selectAll"
                      onChange={handleSelectAll}
                      checked={checkAll}
                    />
                  </th>
                  <th className="px-4 py-2 font-medium text-left text-gray-900 ">
                    Hình
                  </th>
                  <th className="px-4 py-2 font-medium text-left text-gray-900 ">
                    Tên sản phẩm
                  </th>
                  <th className="px-4 py-2 font-medium text-left text-gray-900 ">
                    Phát hành
                  </th>
                  <th className="px-4 py-2 font-medium text-left text-gray-900 ">
                    Hành động
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-600">
                {state.data.products.map((item) => {
                  return (
                    <RowTable
                      key={item.id}
                      id={item.id}
                      handleTrash={handleTrash}
                      handleProductSelect={handleProductSelect}
                      productSelect={productSelect}
                    />
                  );
                })}
              </tbody>
            </table>
          )}

          <div className="bg-white px-4 py-3 flex items-center justify-between border-t border-gray-600 sm:px-6">
            <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
              <div>
                <p className="text-sm text-gray-700">
                  Hiển thị{" "}
                  <span className="font-medium">
                    {" "}
                    {state.data?.products?.length}{" "}
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
                    itemsPerPage={state.data?.products.length}
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

export default ProductTable;
