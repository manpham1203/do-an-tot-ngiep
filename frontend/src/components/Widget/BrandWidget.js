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
      url: `/brand/allbrandname`,
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
    <div className="mb-[20px] text-second dark:text-third">
      <h2 className="text-[22px]">Thương hiệu</h2>
      <ul className="font-normal ml-[10px]">
        {state.data?.map((item) => {
          return (
            <li key={item.id}>
              &#8210;{" "}
              <Link to={`/san-pham?thuong-hieu=${item.slug}`} className="hover:underline underline-offset-4">
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
