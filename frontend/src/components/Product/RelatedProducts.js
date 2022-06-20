import React, { useEffect, useReducer } from "react";
import ListProductCard from "../ProductSlideShow/ListProductCard";
import api from "../../apis/api";

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
function RelatedProducts(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const fetchData = async (id) => {
    dispatch(Loading());
    await api({
      method: "GET",
      url: `/brand/BrandWithProductCard`,
      params: { id: id },
      data: null,
    })
      .then((res) => {
        dispatch(Success(res.data));
      })
      .catch(() => dispatch(Fail()));
  };
  useEffect(() => {
    fetchData(props.brandId);
  }, [props.brandId]);
  console.log(state);
  return (
    <div>
      <h2 className="text-[20px]  md:text-[25px] ">Sản phẩm liên quan</h2>
      <div className="pt-[20px] pb-[20px]">
          {state.data?.productCardVMs?.length > 0 && (
        <ListProductCard products={state.data.productCardVMs} />
      )}
      </div>
      
    </div>
  );
}

export default RelatedProducts;
