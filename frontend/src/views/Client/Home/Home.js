import React, { useEffect, useReducer, useState } from "react";
import Carousel from "../../../components/Carousel/Carousel";
import Heading from "../../../components/Heading/Heading";
import ListProductCard from "../../../components/ProductSlideShow/ListProductCard";
import ListTab from "../../../components/ProductSlideShow/ListTab";
import ProductSlideShow from "../../../components/ProductSlideShow/ProductSlideShow";
import api from "../../../apis/api";
import PostSlideShow from "../../../components/PostSlideShow/PostSlideShow";

const initState = {
  category: {
    loading: false,
    fail: false,
    data: [],
  },
  brand: {
    loading: false,
    fail: false,
    data: [],
  },
};
//action
const CATEGORY_LOADING = "CATEGORY_LOADING";
const CATEGORY_SUCCESS = "CATEGORY_SUCCESS";
const CATEGORY_FAIL = "CATEGORY_FAIL";
const BRAND_LOADING = "BRAND_LOADING";
const BRAND_SUCCESS = "BRAND_SUCCESS";
const BRAND_FAIL = "BRAND_FAIL";
const categoryLoading = () => {
  return {
    type: CATEGORY_LOADING,
  };
};
const categorySuccess = (payload) => {
  return {
    type: CATEGORY_SUCCESS,
    payload: payload,
  };
};
const categoryFail = () => {
  return {
    type: CATEGORY_FAIL,
  };
};
const brandLoading = () => {
  return {
    type: BRAND_LOADING,
  };
};
const brandSuccess = (payload) => {
  return {
    type: BRAND_SUCCESS,
    payload: payload,
  };
};
const brandFail = () => {
  return {
    type: BRAND_FAIL,
  };
};
const reducer = (state, action) => {
  switch (action.type) {
    case CATEGORY_LOADING:
      return {
        ...state,
        category: {
          ...state.category,
          loading: true,
          fail: false,
        },
      };
    case CATEGORY_SUCCESS:
      return {
        ...state,
        category: {
          ...state.category,
          loading: false,
          fail: false,
          data: action.payload,
        },
      };
    case CATEGORY_FAIL:
      return {
        ...state,
        category: {
          ...state.category,
          loading: false,
          fail: true,
        },
      };
    case BRAND_LOADING:
      return {
        ...state,
        brand: {
          ...state.brand,
          loading: true,
          fail: false,
        },
      };
    case BRAND_SUCCESS:
      return {
        ...state,
        brand: {
          ...state.brand,
          loading: false,
          fail: false,
          data: action.payload,
        },
      };
    case BRAND_FAIL:
      return {
        ...state,
        brand: {
          ...state.brand,
          loading: false,
          fail: true,
        },
      };
    default:
      return state;
  }
};
function Home() {
  const [state, dispatch] = useReducer(reducer, initState);
  const fetchProductsOfBrand = async () => {
    dispatch(brandLoading());
    await api({
      method: "GET",
      url: `/Brand/AllBrandWithProductCard`,
      data: null,
    })
      .then((res) => {
        dispatch(brandSuccess(res.data));
      })
      .catch(() => dispatch(brandFail()));
  };
  const fetchProductsOfCategory = async () => {
    dispatch(categoryLoading());
    await api({
      method: "GET",
      url: `/Category/AllCategoryWithProductCard`,
      data: null,
    })
      .then((res) => {
        dispatch(categorySuccess(res.data));
      })
      .catch(() => dispatch(categoryFail()));
  };
  useEffect(() => {
    fetchProductsOfCategory();
    fetchProductsOfBrand();
  }, []);
  const [showBrand, setShowBrand] = useState();
  const [showCategory, setShowCategory] = useState();

  useEffect(() => {
    setShowCategory(state.category?.data[0]?.id);
  }, [state.category.data]);
  useEffect(() => {
    setShowBrand(state.brand?.data[0]?.id);
  }, [state.brand.data]);
  document.title = "Web";

  return (
    <div className="w-[100%]">
      <Carousel></Carousel>
      <ProductSlideShow>
        <Heading title="Thương Hiệu" />
        <div className="flex flex-row justify-center gap-x-[20px] font-primary text-[20px]">
          {state.brand.data.map((item) => {
            return (
              <ListTab
                key={item.id}
                name={item.name}
                id={item.id}
                show={showBrand}
                setShow={setShowBrand}
              />
            );
          })}
        </div>
        {state.brand.data.map((item) => {
          return (
            <ListProductCard
              key={item.id}
              id={item.id}
              products={item.productCardVMs}
              show={showBrand}
            />
          );
        })}
      </ProductSlideShow>
      <ProductSlideShow>
        <Heading title="Danh Mục" />
        <div className="flex flex-row justify-center gap-x-[20px] font-primary font-bold text-[20px]">
          {state.category.data.map((item) => {
            return (
              <ListTab
                key={item.id}
                name={item.name}
                id={item.id}
                show={showCategory}
                setShow={setShowCategory}
              />
            );
          })}
        </div>
        {state.category.data.map((item) => {
          return (
            <ListProductCard
              key={item.id}
              id={item.id}
              products={item.productCardVMs}
              show={showCategory}
            />
          );
        })}
      </ProductSlideShow>
      <PostSlideShow />
    </div>
  );
}

export default Home;
