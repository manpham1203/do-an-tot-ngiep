import React, { useEffect, useReducer } from "react";
import product from "../../assets/product.jpg";
import api from "../../apis/api";
import { Link } from "react-router-dom";

const initState = {
  loading: false,
  fail: false,
  data: [],
};
const LOADING = "LOADING";
const SUCCESS = "SUCCESS";
const FAIL = "FAIL";
const Loading = () => {
  return {
    type: LOADING,
  };
};
const Success = (payload) => {
  return {
    type: SUCCESS,
    payload: payload,
  };
};
const Fail = () => {
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
function OnSaleWidget(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const fetchData = async () => {
    dispatch(Loading());
    await api({
      method: "GET",
      url: `/product/OnSaleWidget`,
      data: null,
    })
      .then((res) => {
        dispatch(Success(res.data));
      })
      .catch(() => dispatch(Fail()));
  };
  useEffect(() => {
    fetchData();
  }, []);

  return (
    state.data.length>0?
    <div className="mb-[20px]">
      <h2 className="text-[22px]">Sản phẩm giảm giá</h2>
      <div className="font-normal flex flex-col gap-y-[20px]">
        {state.data?.map((item) => {
          return (
            <div key={item.id} className="flex flex-row gap-x-[10px]">
              <div className="w-[80px] flex-none">
                <img
                  src={`${item.imgSrc}`}
                  alt=""
                  className="w-full h-full object-cover object-center shadow-md rounded-md"
                />
              </div>
              <div className="">
                <Link to={`/san-pham/${item.slug}`} className="productCard2Name">{item.name}</Link>
       
                  {item.priceDiscount === null ? (
                    <div>
                      {new Intl.NumberFormat("vi-VN", {
                        style: "currency",
                        currency: "VND",
                      }).format(item.price)}
                    </div>
                  ) : (
                    <div className="flex flex-col gap-y-[2px]">
                      <span className="font-normal">
                        {new Intl.NumberFormat("vi-VN", {
                          style: "currency",
                          currency: "VND",
                        }).format(item.priceDiscount)}
                      </span>
                      <span className="line-through text-gray-500">
                        {new Intl.NumberFormat("vi-VN", {
                          style: "currency",
                          currency: "VND",
                        }).format(item.price)}
                      </span>
                    </div>
                  )}
              </div>
            </div>
          );
        })}
      </div>
    </div>
    :""
  );
}

export default OnSaleWidget;
