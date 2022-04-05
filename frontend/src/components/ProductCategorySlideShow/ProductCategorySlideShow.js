import React, { useEffect, useReducer, useState } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation } from "swiper";
import "swiper/css/navigation";
import "swiper/css";
import ProductCard from "../Product/ProductCard";
import api from "../../apis/api";

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
function ProductCategorySlideShow(props) {
  const [show, setShow] = useState();
  const [state, dispatch] = useReducer(reducer, initState);

  const fetchProductsOfBrands = async () => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/api/categoryfull`,
      data: null,
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(dispatch(fail()));
  };
  useEffect(() => {
    fetchProductsOfBrands();
  }, []);

  useEffect(() => {
    setShow(state.data[0]?.id);
  }, [state.data]);

  return (
    <div className="ProductSlideShow container mx-auto pt-[80px] ">
      <h1 className="font-primary font-bold text-[30px] text-center">
        DANH MỤC
      </h1>
      <div className="flex flex-row justify-center gap-x-[20px] font-primary font-bold text-[20px]">
        {state.data.map((item) => {
          return item.productVMs.length>0?(
            <button
              key={item.id}
              onClick={() => setShow(item.id)}
              className={`${show === item.id ? "underline" : ""}`}
            >
              {item.name}
            </button>
          ):null;
        })}
      </div>

      {state.data.map((item) => {
        return (
          <Swiper
            key={item.id}
            slidesPerView={1}
            spaceBetween={30}
            navigation={true}
            modules={[Navigation]}
            breakpoints={{
              640: {
                slidesPerView: 2,
                spaceBetween: 20,
              },
              768: {
                slidesPerView: 3,
                spaceBetween: 30,
              },
              1024: {
                slidesPerView: 4,
                spaceBetween: 30,
              },
              1280: {
                slidesPerView: 4,
                spaceBetween: 30,
              },
            }}
            className={`mySwipe ${show !== item.id ? "hidden" : null}  p-[20px]`}
          >
            {item.productVMs.map((prod) => {
              return (
                <SwiperSlide key={prod.id}>
                  <ProductCard
                    id={prod.id}
                    name={prod.name}
                    brandName={item.name}
                    price={prod.price}
                    priceDiscount={prod.priceDiscount}
                  />
                </SwiperSlide>
              );
            })}
          </Swiper>
        );
      })}
    </div>
  );
}

export default ProductCategorySlideShow;
