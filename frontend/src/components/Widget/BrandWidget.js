import React, { useEffect, useReducer, memo } from "react";
import { Link } from "react-router-dom";
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
function BrandWidget(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const fetch = async () => {
    dispatch(Loading());
    await api({
      method: "GET",
      url: `/api/brand`,
      data: null,
    })
      .then((res) => {
        dispatch(Success(res.data));
      })
      .catch(() => dispatch(Fail()));
  };
  useEffect(() => {
    fetch();
  }, []);
  return (
    <div className="mb-[20px]">
      <div
        className="font-semibold border-b-[3px] relative border-[#1D1B26] overflow-hidden 
          before:block before:absolute before:bg-[#1D1B26] before:h-[100px] before:w-[100px] before:rotate-45 before:z-[-1] before:left-[60px]"
      >
        <h2 className="bg-[#1D1B26] p-[5px] inline-block text-[white]">
          THƯƠNG HIỆU
        </h2>
      </div>
      <ul className="font-medium ml-[20px] mt-[10px]">
        {state.data?.map((item) => {
          return (
            <li key={item.id}>
              &#8210;{" "}
              <Link to={`/thuong-hieu/${item.id}`} className="hover:underline">
                {item.name}
              </Link>
            </li>
          );
        })}
      </ul>
    </div>
  );
}

export default memo(BrandWidget);
