import React, { useEffect, useReducer } from "react";
import { Link, useParams } from "react-router-dom";
import api from "../../../apis/api";
import ProductCard from "../../../components/Product/ProductCard";
import BrandWidget from "../../../components/Widget/BrandWidget";
import CategoryWidget from "../../../components/Widget/CategoryWidget";

const initState = {
  loading: false,
  fail: false,
  data: {},
};
//action
const PRODUCT_LOADING = "PRODUCT_LOADING";
const PRODUCT_SUCCESS = "PRODUCT_SUCCESS";
const PRODUCT_FAIL = "PRODUCT_FAIL";
const productLoading = () => {
  return {
    type: PRODUCT_LOADING,
  };
};
const productSuccess = (payload) => {
  return {
    type: PRODUCT_SUCCESS,
    payload: payload,
  };
};
const productFail = () => {
  return {
    type: PRODUCT_FAIL,
  };
};
const reducer = (state, action) => {
  switch (action.type) {
    case PRODUCT_LOADING:
      return {
        ...state,
          loading: true,
          fail: false,
      };
    case PRODUCT_SUCCESS:
      return {
        ...state,
          loading: false,
          fail: false,
          data: action.payload,
      };
    case PRODUCT_FAIL:
      return {
        ...state,
          loading: false,
          fail: true,
      };
    default:
      return state;
  }
};
function ProductsCategory() {
  const [state, dispatch] = useReducer(reducer, initState);
  const { categoryId } = useParams();

  const fetchProducts = async (id) => {
    dispatch(productLoading());
    await api({
      method: "GET",
      url: `/api/categoryfull/${id}`,
      data: null,
    })
      .then((res) => {
        dispatch(productSuccess(res.data));
      })
      .catch(() => dispatch(productFail()));
  };
  useEffect(() => {
    fetchProducts(categoryId);
  }, [categoryId]);

  return (
    <div className="container mx-auto pt-[100px] flex flex-row gap-x-[20px]">
      <div className="w-[400px]">
        <BrandWidget />
        <CategoryWidget />
        <div>
          <div
            className="font-semibold border-b-[3px] relative border-[#1D1B26] overflow-hidden 
          before:block before:absolute before:bg-[#1D1B26] before:h-[100px] before:w-[100px] before:rotate-45 before:z-[-1] before:left-[74px]"
          >
            <h2 className="bg-[#1D1B26] p-[5px] inline-block text-[white]">
              LỌC SẢN PHẨM
            </h2>
          </div>
        </div>
      </div>
      <div className="w-full grid grid-cols-4 gap-x-[20px] gap-y-[20px]">
        {state.data?.productVMs?.map((item) => {
          return (
            <ProductCard
              key={item.id}
              name={item.name}
              price={item.price}
              priceDiscount={item.priceDiscount}
              id={item.id}
            />
          );
        })}
      </div>
    </div>
  );
}

export default ProductsCategory;
