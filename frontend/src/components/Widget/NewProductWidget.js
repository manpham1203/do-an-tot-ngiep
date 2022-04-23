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
function NewProductWidget(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const fetchData = async () => {
    dispatch(Loading());
    await api({
      method: "GET",
      url: `/product/newproductwidget`,
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
    <div className="mb-[20px]">
      <h2 className="text-[25px]">Sản phẩm mới</h2>
      <div className="font-normal flex flex-col gap-y-[20px]">
        {state.data?.map((item) => {
          return (
            <div key={item.id} className="flex flex-row gap-x-[10px]">
              <div className="w-[80px]">
                <img
                  src={`${item.imgSrc}`}
                  alt=""
                  className="w-full h-full object-cover object-center shadow-md rounded-md"
                />
              </div>
              <div className="">
                <Link to={`/san-pham/${item.slug}`}>{item.name}</Link>
       
                  {item.priceDiscount === null ? (
                    <div>
                      {new Intl.NumberFormat("vi-VN", {
                        style: "currency",
                        currency: "VND",
                      }).format(item.price)}
                    </div>
                  ) : (
                    <div className="flex flex-row gap-x-[15px]">
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
  );
}

export default NewProductWidget;
