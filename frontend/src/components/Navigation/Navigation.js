import React, { useEffect, useReducer } from "react";
import { NavLink } from "react-router-dom";
import api from "../../apis/api";
import { FaHeart, FaTimes } from "react-icons/fa";
import { TiArrowSortedDown } from "react-icons/ti";
import { menuData } from "../menuData";

//init state
const initState = {
  brand: {
    loading: false,
    fail: false,
    data: [],
  },
  category: {
    loading: false,
    fail: false,
    data: [],
  },
};
//action
const BRAND_LOADING = "BRAND_LOADING";
const BRAND_SUCCESS = "BRAND_SUCCESS";
const BRAND_FAIL = "BRAND_FAIL";
const CATEGORY_LOADING = "CATEGORY_LOADING";
const CATEGORY_SUCCESS = "CATEGORY_SUCCESS";
const CATEGORY_FAIL = "CATEGORY_FAIL";
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
//reducer
const reducer = (state, action) => {
  switch (action.type) {
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
    default:
      return state;
  }
};
function Navigation(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const { brand, category } = state;

  const fetchBrand = async () => {
    dispatch(brandLoading());
    await api({
      method: "GET",
      url: `/Brand/allbrandname`,
      data: null,
    })
      .then((res) => dispatch(brandSuccess(res.data)))
      .catch(() => dispatch(brandFail()));
  };
  const fetchCategory = async () => {
    dispatch(categoryLoading());
    await api({
      method: "GET",
      url: `/Category/allcategoryname`,
      data: null,
    })
      .then((res) => dispatch(categorySuccess(res.data)))
      .catch(() => dispatch(categoryFail()));
  };

  useEffect(() => {
    fetchBrand();
    fetchCategory();
  }, []);
  return (
    <ul className={`lg:flex flex-row items-center justify-center hidden `}>
      {menuData.map((item, index) => {
        return (
          <li className="group relative flex flex-row" key={item.id}>
            <NavLink
              to={item.slug}
              className={`nav-link flex-row items-center p-[10px] ${
                props.navHome === false
                  ? "text-black"
                  : props.navHome && props.scroll
                  ? "text-black"
                  : "text-white"
              }`}
              style={({ isActive }) =>
                isActive ? { textDecoration: "underline" } : undefined
              }
            >
              {item.title}
              {item.id === 6 || item.id === 7 ? (
                <TiArrowSortedDown className="inline-block" />
              ) : (
                ""
              )}
            </NavLink>
            {item.id === 6 ? (
              <ul
                className="w-[160px] absolute top-[110px] bg-white text-black rounded-md p-[10px] invisible opacity-0 
                      group-hover:opacity-100 group-hover:visible group-hover:top-[56px] 
                      transition-all duration-[0.3s] shadow-md"
              >
                {brand.loading ? (
                  <>loading</>
                ) : brand.fail === true ? (
                  <>error</>
                ) : (
                  brand.data.map((b) => {
                    return (
                      <li key={b.id} className="hover:bg-gray-300">
                        <NavLink to={`/thuong-hieu/${b.id}`} className="block">
                          {b.name}
                        </NavLink>{" "}
                      </li>
                    );
                  })
                )}
              </ul>
            ) : item.id === 7 ? (
              <ul
                className="w-[160px] absolute top-[110px] bg-white text-black rounded-md p-[10px] invisible opacity-0 
                      group-hover:opacity-100 group-hover:visible group-hover:top-[56px] 
                      transition-all duration-[0.3s] shadow-md"
              >
                {category.loading ? (
                  <>loading</>
                ) : category.fail === true ? (
                  <>error</>
                ) : (
                  category.data.map((c) => {
                    return (
                      <li key={c.id} className="hover:bg-gray-300">
                        <NavLink to={`/danh-muc/${c.id}`} className="block">
                          {c.name}
                        </NavLink>
                      </li>
                    );
                  })
                )}
              </ul>
            ) : (
              ""
            )}
          </li>
        );
      })}
    </ul>
  );
}

export default Navigation;
