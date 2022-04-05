import React, { useEffect, useReducer, useState } from "react";
import { FaTimes } from "react-icons/fa";
import { Link } from "react-router-dom";
import { menuData } from "../menuData";
import api from "../../apis/api";

const data = [
  {
    id: 1,
    title: "title 1",
    content: "content 1",
  },
  {
    id: 2,
    title: "title 2",
    content: "content 2",
  },
  {
    id: 3,
    title: "title 3",
    content: "content 3",
  },
];
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
const MobileMenu = (props) => {
  const [state, dispatch] = useReducer(reducer, initState);
  const { brand, category } = state;
  const fetchBrand = async () => {
    dispatch(brandLoading());
    await api({
      method: "GET",
      url: `/api/brand`,
      data: null,
    })
      .then((res) => dispatch(brandSuccess(res.data)))
      .catch(() => dispatch(brandFail()));
  };
  const fetchCategory = async () => {
    dispatch(categoryLoading());
    await api({
      method: "GET",
      url: `/api/Category`,
      data: null,
    })
      .then((res) => dispatch(categorySuccess(res.data)))
      .catch(() => dispatch(categoryFail()));
  };

  useEffect(() => {
    fetchBrand();
    fetchCategory();
  }, []);
  const [selected, setSelected] = useState(null);
  const toggle = (i) => {
    if (selected === i) {
      return setSelected(null);
    }
    setSelected(i);
  };
  return (
    <div
      className={`z-[10] fixed bg-white right-0 top-0 bottom-0 w-[100%] sm:w-[300px] ${
        props.openMenu ? null : "translate-x-full"
      } transition-all duration-[0.3s]
      lg:hidden
      `}
    >
      <div className="flex justify-end p-[20px] ">
        <button
          className="cursor-pointer text-[25px]"
          onClick={() => props.setOpenMenu(false)}
        >
          <FaTimes />
        </button>
      </div>
      <div className="h-fit flex flex-col">
        <ul className="ml-[20px]">
          {menuData.map((item, index) => {
            return (
              <li key={item.id}>
                {item.id === 6 || item.id === 7 ? (
                  <h2 onClick={()=>toggle(index)} className="cursor-pointer">{item.title} {selected===index?"-":"+"}</h2>
                ) : (
                  <Link to={item.slug}>{item.title}</Link>
                )}

                {item.id === 6 ? (
                  <ul className={`${selected===index?null:"hidden"} ml-[20px]`}>
                    {brand.data.map((b) => {
                      return (
                        <li key={b.id}>
                          <Link to={`/thuong-hieu/${item.id}`}>{b.name}</Link>
                        </li>
                      );
                    })}
                  </ul>
                ) : null}
                {item.id === 7 ? (
                  <ul className={`${selected===index?null:"hidden"} ml-[20px]`}>
                    {category.data.map((c) => {
                      return (
                        <li key={c.id}>
                          <Link to={`/danh-muc/${c.id}`}>{c.name}</Link>
                        </li>
                      );
                    })}
                  </ul>
                ) : null}
              </li>
            );
          })}
        </ul>
        
      </div>
    </div>
  );
};

export default MobileMenu;
