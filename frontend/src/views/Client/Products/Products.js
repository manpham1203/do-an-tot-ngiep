import React, { useEffect, useReducer, useState, useRef } from "react";
import { useForm } from "react-hook-form";
import { useLocation, useSearchParams } from "react-router-dom";
import api from "../../../apis/api";
import { IoIosArrowUp, IoIosArrowDown } from "react-icons/io";
import ProductCard from "../../../components/Product/ProductCard";
import Pagination from "../../../components/Pagination/Pagination";
import Slider from "rc-slider";
import "rc-slider/assets/index.css";
import { FaTimesCircle, FaSquareFull } from "react-icons/fa";
import Select from "react-select";
import ProductCard2 from "../../../components/Product/ProductCard2";
import ProductRow from "../../../components/Skeleton/ProductRow";

const initState = {
  loading: false,
  fail: false,
  data: {
    totalPage: 0,
    totalResult: 0,
    products: [],
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
          totalPage: action.payload.totalPage,
          totalResult: action.payload.totalResult,
          products: action.payload.products,
        },
      };
    case FAIL:
      return {
        ...state,
        loading: true,
        fail: true,
      };
    default:
      return state;
  }
};
const colourStyles = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles) => ({
    ...styles,
    color: '"#202121"',
    borderLeft: "0.1rem solid #1EB7F3",
    left: "0%",
    lineHeight: "1.3rem",
    paddingLeft: "0.5rem",
    marginLeft: "0rem",
  }),
  control: () => ({
    marginLeft: "1rem",
    display: "flex",
    border: "1px solid #202121",
    height: "30px",
    width: "160px",
    borderRadius: "0px",
    background: "#fcfcfc",
    fontSize: "14px",
  }),
  option: (styles) => ({
    ...styles,
    fontSize: "14px",
    textAlign: "left",
    color: "#103D56",
    background: "white",
    borderBottom: "0.1rem solid #103D56",
    ":last-of-type": {
      borderBottom: "none",
    },
    ":hover": {
      background: "#f7f7f7",
      cursor: "pointer",
    },
  }),
  menu: (styles) => ({
    ...styles,
    borderRadius: 0,
    marginLeft: "1rem",
    width: "160px",
    border: "1px solid #202121",
    paddingTop: 0,
  }),
};
const colourStyles2 = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles) => ({
    ...styles,
    color: "black",
    left: "0%",
    lineHeight: "1.3rem",
    paddingLeft: "0.5rem",
    marginLeft: "0rem",
  }),
  control: () => ({
    display: "flex",
    border: "1px solid #202121",
    height: "40px",
    width: "100%",
    borderRadius: "0px",
    background: "#fcfcfc",
    fontSize: "14px",
  }),
  option: (styles) => ({
    ...styles,
    fontSize: "14px",
    textAlign: "left",
    color: "#103D56",
    height: "40px",
    background: "white",
    borderBottom: "0.1rem solid #103D56",
    ":last-of-type": {
      borderBottom: "none",
    },
    ":hover": {
      background: "#f7f7f7",
      cursor: "pointer",
    },
  }),
  menu: (styles) => ({
    ...styles,
    borderRadius: 0,
    width: "100%",
    border: "1px solid #202121",
    paddingTop: 0,
  }),
};
function Products(props) {
  document.title = "S???n ph???m";
  const [state, dispatchProduct] = useReducer(reducer, initState);

  const [searchParams, setSearchPrams] = useSearchParams();
  const brandSlugs = searchParams.getAll("thuong-hieu");
  const categorySlugs = searchParams.getAll("danh-muc");
  const price = searchParams.get("gia");
  const [brand, setBrand] = useState([]);
  const [category, setCategory] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [tabFilter, setTabFilter] = useState({
    brand: true,
    category: true,
    priceRange: true,
  });
  const [priceRange, setPriceRange] = useState(null);
  const [query, setQuery] = useState("");
  const [arrBrand, setArrBrand] = useState([]);
  const [arrCategory, setArrCategory] = useState([]);

  const [priceRangeData, setPriceRangeData] = useState({
    maxPrice: 0,
    minPrice: 0,
  });
  const location = useLocation();
  const orderOptions = [
    { value: "day_desc", label: "M???i nh???t" },
    { value: "day_asc", label: "C?? nh???t" },
    { value: "price_desc", label: "Gi?? cao nh???t" },
    { value: "price_asc", label: "Gi?? th???p nh???t" },
  ];
  const priceOptions = [
    { value: "duoi-5-trieu", label: "D?????i 5 tri???u" },
    { value: "tu-5-10-trieu", label: "T??? 5 - 10 tri???u" },
    { value: "tu-10-15-trieu", label: "T??? 10 - 15 tri???u" },
    { value: "tu-15-20-trieu", label: "T??? 15 - 20 tri???u" },
    { value: "tren-20", label: "Tr??n 20 tri???u" },
  ];
  const [orderBy, setOrderBy] = useState(orderOptions[0]);
  const fetchData = async () => {
    var data = {
      brandSlugs: brandSlugs,
      categorySlugs: categorySlugs,
      limit: 16,
      currentPage: currentPage,
      priceRange: price,
      search: query,
      orderBy: orderBy.value,
    };
    dispatchProduct(loading());
    await api({
      method: "POST",
      url: `/Product/productfilter`,
      data: data,
    })
      .then((res) => {
        if (res.status === 200) {
          dispatchProduct(success(res.data));
        }
      })
      .catch(() => dispatchProduct(fail()));
  };
  const fetchDataPriceRange = async () => {
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
      })
      .catch(() => console.log("brand fail"));
  };
  const fetchDataBrand = async () => {
    await api({
      method: "GET",
      url: `/Brand/allbrandname`,
    })
      .then((res) => {
        setBrand(res.data);
      })
      .catch(() => console.log("brand fail"));
  };
  const fetchDataCategory = async () => {
    await api({
      method: "GET",
      url: `/category/AllCategoryName`,
    })
      .then((res) => {
        setCategory(res.data);
      })
      .catch(() => console.log("category fail"));
  };

  useEffect(() => {
    setArrBrand(brandSlugs);
    setArrCategory(categorySlugs);
    // if (price != null) {
    //   for (var i = 0; i < priceOptions.length; i++) {
    //     if (price === priceOptions[i].value) {
    //       setPriceRange(priceOptions[i]);
    //     }
    //   }
    // }
    fetchData();
  }, [location]);
  useEffect(() => {
    fetchData();
  }, [currentPage]);
  useEffect(() => {
    setCurrentPage(1);
    fetchData();
  }, [arrBrand, arrCategory, query, orderBy]);
  useEffect(() => {
    fetchDataBrand();
    fetchDataCategory();
    fetchDataPriceRange();
  }, []);

  const [grid, setGrid] = useState(2);

  const handleBrandChange = (event) => {
    let newArray = [...arrBrand, event.target.id];
    if (arrBrand.includes(event.target.id)) {
      newArray = newArray.filter((x) => x !== event.target.id);
    }
    setArrBrand(newArray);
    if (price === null) {
      setSearchPrams({
        "thuong-hieu": newArray,
        "danh-muc": arrCategory,
      });
    } else {
      setSearchPrams({
        "thuong-hieu": newArray,
        "danh-muc": arrCategory,
        gia: priceRange.value,
      });
    }
  };
  const handleCategoryChange = (event) => {
    let newArray = [...arrCategory, event.target.id];
    if (arrCategory.includes(event.target.id)) {
      newArray = newArray.filter((x) => x !== event.target.id);
    }
    setArrCategory(newArray);
    if (price === null) {
      setSearchPrams({
        "thuong-hieu": arrBrand,
        "danh-muc": newArray,
      });
    } else {
      setSearchPrams({
        "thuong-hieu": arrBrand,
        "danh-muc": newArray,
        gia: priceRange.value,
      });
    }
  };

  const handlePriceChange = (e) => {
    setPriceRange(e);
    console.log(e);
    if (e === null) {
      if (arrBrand.length !== 0 || arrCategory.length !== 0) {
        setSearchPrams({
          "thuong-hieu": arrBrand,
          "danh-muc": arrCategory,
        });
      } else {
        if (e === null) {
          setSearchPrams({});
        }
      }
    } else {
      setSearchPrams({
        "thuong-hieu": arrBrand,
        "danh-muc": arrCategory,
        gia: e.value,
      });
    }
  };
  useEffect(() => {
    setArrBrand(brandSlugs);
    setArrCategory(categorySlugs);
    if (price != null) {
      for (var i = 0; i < priceOptions.length; i++) {
        if (price === priceOptions[i].value) {
          setPriceRange(priceOptions[i]);
        }
      }
    }
  }, []);
  const handleUncheckedBrand = () => {
    setArrBrand([]);
    if (price === null) {
      setSearchPrams({
        "danh-muc": arrCategory,
      });
    } else {
      setSearchPrams({
        "danh-muc": arrCategory,
        gia: priceRange.value,
      });
    }
  };
  const handleUncheckedCategory = () => {
    setArrBrand([]);
    if (price === null) {
      setSearchPrams({
        "thuong-hieu": arrBrand,
      });
    } else {
      setSearchPrams({
        "thuong-hieu": arrBrand,
        gia: priceRange.value,
      });
    }
  };
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return (
    <div className="container px-[10px] sm:px-[20px] mx-auto flex flex-col text-second dark:text-third">
      <div className="h-[60px] hidden lg:flex lg:items-center gap-x-[20px]">
        <div className="w-[280px] flex-none">L???c s???n ph???m</div>
        <div className=" w-full flex justify-between items-center">
          <div className="text-gray-500 dark:text-gray-200">
            T??m th???y{" "}
            <span className="text-second dark:text-third">
              {state.data.totalResult}
            </span>{" "}
            k???t qu???
          </div>
          <div className="flex flex-row gap-x-[20px] items-center">
            <div className="flex flex-row items-center">
              <div>S???p x???p theo:</div>
              <div>
                <Select
                  className="min-w-[150px] cursor-pointer"
                  classNamePrefix="select"
                  // defaultValue={orderOptions[0]}
                  isClearable={false}
                  isSearchable={false}
                  name="orderBy"
                  value={orderBy}
                  onChange={(e) => setOrderBy(e)}
                  options={orderOptions}
                  styles={colourStyles}
                />
              </div>
            </div>
            <div className="flex flex-row items center gap-x-[20px]">
              <div
                onClick={() => setGrid(0)}
                className="cursor-pointer flex flex-col gap-y-[2px] text-[5px]"
              >
                <div className=" flex flex-row gap-x-[2px]">
                  <FaSquareFull />
                  <div className="flex flex-row">
                    <FaSquareFull />
                    <FaSquareFull />
                  </div>
                </div>
                <div className=" flex flex-row gap-x-[2px]">
                  <FaSquareFull />
                  <div className="flex flex-row">
                    <FaSquareFull />
                    <FaSquareFull />
                  </div>
                </div>
              </div>
              <div
                onClick={() => setGrid(1)}
                className="cursor-pointer flex flex-col gap-y-[2px] text-[5px]"
              >
                <div className=" flex flex-row gap-x-[2px]">
                  <FaSquareFull />
                  <FaSquareFull />
                  <FaSquareFull />
                </div>
                <div className=" flex flex-row gap-x-[2px]">
                  <FaSquareFull />
                  <FaSquareFull />
                  <FaSquareFull />
                </div>
              </div>
              <div
                onClick={() => setGrid(2)}
                className="cursor-pointer flex flex-col gap-y-[2px] text-[5px]"
              >
                <div className=" flex flex-row gap-x-[2px]">
                  <FaSquareFull />
                  <FaSquareFull />
                  <FaSquareFull />
                  <FaSquareFull />
                </div>
                <div className=" flex flex-row gap-x-[2px]">
                  <FaSquareFull />
                  <FaSquareFull />
                  <FaSquareFull />
                  <FaSquareFull />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="flex flex-col lg:flex-row gap-x-[20px]">
        <div className="flex-none w-full lg:w-[280px]">
          <div className="flex flex-col">
            <div className="relative mb-[20px]">
              <div className="w-full relative">
                <input
                  placeholder=" "
                  value={query}
                  onChange={(e) => setQuery(e.target.value)}
                  className="form-input border border-second dark:border-third text-second dark:text-third font-normal rounded-[4px] w-[100%] h-[50px] px-[20px] transition-all duration-[0.25s]  outline-none bg-third dark:bg-darkMode"
                />
                <label className="form-label absolute left-[20px] top-[50%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-second dark:text-third bg-third dark:bg-darkMode">
                  T??m ki???m
                </label>
                <span
                  className="absolute right-[8px] top-[50%] translate-y-[-50%] cursor-pointer text-second dark:text-third"
                  onClick={() => setQuery("")}
                >
                  <FaTimesCircle />
                </span>
              </div>
            </div>
          </div>

          <div className="flex-[1] border-t border-gray-200 ">
            <div
              className="cursor-pointer w-full flex flex-row justify-between h-[50px] items-center"
              onClick={() =>
                setTabFilter({ ...tabFilter, brand: !tabFilter.brand })
              }
            >
              <span>Th????ng Hi???u</span>
              {tabFilter.brand ? <IoIosArrowDown /> : <IoIosArrowUp />}
            </div>
            <ul className={`${tabFilter.brand === false && "hidden"}`}>
              {brand.length > 0 &&
                brand.map((item) => {
                  return (
                    <li key={item.id} className="mb-[10px]">
                      <label className="inline-flex items-center cursor-pointer">
                        <input
                          type="checkbox"
                          value={item.slug}
                          id={item.slug}
                          className="form-checkbox hidden "
                          onChange={handleBrandChange}
                          checked={arrBrand.some((x) => x === item.slug)}
                        />
                        <div className="checkbox-box bg-white box-content w-[18px] h-[18px] p-[1px] border border-second dark:border-third flex items-center justify-center mr-[10px] rounded-[3px]"></div>
                        <span className="block text-sm font-medium text-gray-900 dark:text-gray-300">
                          {item.name}
                        </span>
                      </label>
                    </li>
                  );
                })}
              {arrBrand?.length > 0 && (
                <li className=" mb-[25px] text-right">
                  <span
                    className="font-light hover:underline underline-offset-4 cursor-pointer"
                    onClick={handleUncheckedBrand}
                  >
                    B??? c??c l???a ch???n
                  </span>
                </li>
              )}
            </ul>
          </div>
          <div className="w-full border-t border-gray-200">
            <div
              className="cursor-pointer w-full flex flex-row justify-between h-[50px] items-center"
              onClick={() =>
                setTabFilter({ ...tabFilter, category: !tabFilter.category })
              }
            >
              <span>Danh m???c</span>
              {tabFilter.category ? <IoIosArrowDown /> : <IoIosArrowUp />}
            </div>
            <ul className={`${tabFilter.category === false && "hidden"}`}>
              {category.length > 0 &&
                category.map((item) => {
                  return (
                    <li key={item.id} className="mb-[10px]">
                      <label className="inline-flex items-center cursor-pointer">
                        <input
                          type="checkbox"
                          value={item.slug}
                          id={item.slug}
                          className="form-checkbox hidden "
                          onChange={handleCategoryChange}
                          checked={arrCategory.some((x) => x === item.slug)}
                        />
                        <div className="checkbox-box bg-white box-content w-[18px] h-[18px] p-[1px] border border-second dark:border-third flex items-center justify-center mr-[10px] rounded-[3px]"></div>
                        <span className="block text-sm font-medium text-gray-900 dark:text-gray-300">
                          {item.name}
                        </span>
                      </label>
                    </li>
                  );
                })}
              {arrCategory?.length > 0 && (
                <li className=" mb-[25px] text-right">
                  <span
                    className="font-light hover:underline underline-offset-4 cursor-pointer"
                    onClick={handleUncheckedCategory}
                  >
                    B??? c??c l???a ch???n
                  </span>
                </li>
              )}
            </ul>
          </div>
          <div className="w-full border-t border-gray-200 ">
            <div
              className="cursor-pointer w-full flex flex-row justify-between h-[50px] items-center "
              onClick={() =>
                setTabFilter({
                  ...tabFilter,
                  priceRange: !tabFilter.priceRange,
                })
              }
            >
              <span>Gi??</span>
              {tabFilter.priceRange ? <IoIosArrowDown /> : <IoIosArrowUp />}
            </div>
            {/* <div
              className={`${
                tabFilter.priceRange === false && "hidden"
              } mb-[25px]`}
            >
              {loadingP ? (
                ""
              ) : (
                <>
                  
                </>
              )}
            </div> */}
            <div
              className={`${
                tabFilter.priceRange === false && "hidden"
              } mb-[25px] text-second dark:text-third`}
            >
              <Select
                className="min-w-[150px] cursor-pointer text-second dark:text-third"
                classNamePrefix="select"
                // defaultValue={orderOptions[0]}
                isClearable={true}
                isSearchable={false}
                name="priceRange"
                value={priceRange}
                onChange={handlePriceChange}
                options={priceOptions}
                styles={colourStyles2}
                placeholder="Ch???n kho???ng gi??"
              />
            </div>
          </div>
          <div className="border-y border-gray-200 lg:hidden  py-[10px] mb-[20px]">
            <div className="flex flex-row sm:flex-row items-center">
              <div>S???p x???p theo:</div>
              <div>
                <Select
                  className="min-w-[150px] cursor-pointer"
                  classNamePrefix="select"
                  // defaultValue={orderOptions[0]}
                  isClearable={false}
                  isSearchable={false}
                  name="orderBy"
                  value={orderBy}
                  onChange={(e) => setOrderBy(e)}
                  options={orderOptions}
                  styles={colourStyles}
                />
              </div>
            </div>
          </div>
        </div>
        <div className="w-full">
          {state.loading ? (
            <ProductRow />
          ) : (
            <div>
              {grid === 0 && (
                <div className="grid grid-cols-2 gap-y-[25px] gap-x-[25px]">
                  {state.data?.products &&
                    state.data.products.map((item) => {
                      return (
                        <ProductCard2
                          key={item.id}
                          id={item.id}
                          name={item.name}
                          slug={item.slug}
                          brandName={item.brandNameVM.name}
                          brandSlug={item.brandNameVM.slug}
                          price={item.price}
                          priceDiscount={item.priceDiscount}
                          image={item.imageSrc}
                          star={item.star}
                        />
                      );
                    })}
                </div>
              )}
              {grid !== 0 && (
                <div
                  className={`hidden lg:grid ${grid === 2 && "grid-cols-4"} ${
                    grid === 1 && "grid-cols-3"
                  }  gap-x-[25px] gap-y-[25px] w-full`}
                >
                  {state.data?.products &&
                    state.data.products.map((item) => {
                      return (
                        <ProductCard
                          key={item.id}
                          id={item.id}
                          name={item.name}
                          slug={item.slug}
                          brandName={item.brandNameVM.name}
                          brandSlug={item.brandNameVM.slug}
                          price={item.price}
                          priceDiscount={item.priceDiscount}
                          image={item.imageSrc}
                          star={item.star}
                        />
                      );
                    })}
                </div>
              )}
              <div
                className={`lg:hidden grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-x-[25px] gap-y-[25px] w-full`}
              >
                {state.data?.products &&
                  state.data.products.map((item) => {
                    return (
                      <ProductCard
                        key={item.id}
                        id={item.id}
                        name={item.name}
                        slug={item.slug}
                        brandName={item.brandNameVM.name}
                        brandSlug={item.brandNameVM.slug}
                        price={item.price}
                        priceDiscount={item.priceDiscount}
                        image={item.imageSrc}
                        star={item.star}
                      />
                    );
                  })}
              </div>
            </div>
          )}

          <div className="flex justify-center mt-[30px] ">
            {state.loading ? (
              ""
            ) : (
              <Pagination
                forcePage={currentPage}
                setCurrentPage={setCurrentPage}
                totalPage={state?.data?.totalPage}
                itemsPerPage={state.data?.products?.length}
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Products;
