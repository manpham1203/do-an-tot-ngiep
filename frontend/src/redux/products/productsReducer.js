import {
  FETCH_PRODUCTS,
  GET_PRODUCTS_LOADING,
  GET_PRODUCTS_SUCCESS,
  GET_PRODUCTS_FAIL,
  SET_PRODUCTS_EMPTY
} from "./productsConstants";

const initialState = {
  loading: false,
  dataProducts: [],
  fail: false,
};

export const productsReducer = (state = initialState, action) => {

  switch (action.type) {
    case SET_PRODUCTS_EMPTY:
      return {
        ...state,
        loading: false,
        dataProducts: [],
        fail: false,
      };
    case FETCH_PRODUCTS:
      return {
        ...state,
        loading: false,
        dataProducts: action.payload,
        fail: false,
      };
    case GET_PRODUCTS_LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case GET_PRODUCTS_SUCCESS:
      return {
        ...state,
        loading: false,
        dataProducts:[...state.dataProducts, ...action.payload],
        fail: false,
      };
    case GET_PRODUCTS_FAIL:
      return {
        ...state,
        dataProducts: [],
        loading: false,
        fail: true,
      };
    default:
      return state;
  }
};
