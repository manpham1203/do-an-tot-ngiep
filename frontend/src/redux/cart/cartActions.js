import api from "../../apis/api";
import {
  ADD_TO_CART,
  REMOVER_FROM_CART,
  ADJUST_QTY,
  LOAD_CURRENT_ITEM,
  GET_CART_LOCAL_STORAGE,
  INCREMENT_QTY,
  DECREMENT_QTY,
  PRODUCTS_OF_CART_LOADING,
  PRODUCTS_OF_CART_SUCCESS,
  PRODUCTS_OF_CART_FAIL,
  DATA_CART_SUCCESS,
  DATA_CART_LOADING,
  DATA_CART_FAIL,
  SET_DATA_CART_EMPTY,
  SET_CART_EMPTY,
  SET_CART
} from "./cartConstant";
export const fetchProductsOfCart = () => async (dispatch) => {
  dispatch({ type: PRODUCTS_OF_CART_LOADING });
  try {
    const reponse = await api.get("/api/ProductFull");
    // setTimeout(() => {
    dispatch({ type: PRODUCTS_OF_CART_SUCCESS, payload: reponse.data });
    // }, 5000);
  } catch {
    dispatch({ type: PRODUCTS_OF_CART_FAIL });
  }
};
export const fetchDataCart = (payload) => (dispatch) => {
  dispatch({ type: DATA_CART_LOADING });
  if (payload.length >= 0) {
    dispatch({ type: SET_DATA_CART_EMPTY });

    dispatch({ type: DATA_CART_SUCCESS, payload: payload });
  } else {
    dispatch({ type: DATA_CART_FAIL });
  }
};
export const setCartEmpty = (payload) => {
  return {
    type: SET_CART_EMPTY,
    payload: payload,
  };
};
export const setDataCartEmpty = (payload) => {
  return {
    type: SET_DATA_CART_EMPTY,
    payload: payload,
  };
};
export const dataCartSuccess = (payload) => {
  return {
    type: DATA_CART_SUCCESS,
    payload: payload,
  };
};
export const dataCartLoading = (payload) => {
  return {
    type: DATA_CART_LOADING,
    payload: payload,
  };
};
export const dataCartFail = (payload) => {
  return {
    type: DATA_CART_FAIL,
    payload: payload,
  };
};
export const addToCart = (payload) => {
  return {
    type: ADD_TO_CART,
    payload: payload,
  };
};
export const setCart = (payload) => {
  return {
    type: SET_CART,
    payload: payload,
  };
};
export const removeFromCart = (payload) => {
  return {
    type: REMOVER_FROM_CART,
    payload: payload,
  };
};
export const adjustQty = (payload) => {
  return {
    type: ADJUST_QTY,
    payload: payload,
  };
};
export const decrementQTY = (payload) => {
  return {
    type: DECREMENT_QTY,
    payload: payload,
  };
};
export const incrementQTY = (payload) => {
  return {
    type: INCREMENT_QTY,
    payload: payload,
  };
};
export const getCartLocalStorage = (payload) => {
  return {
    type: GET_CART_LOCAL_STORAGE,
    payload: payload,
  };
};
export const loadCurrentItem = (payload) => {
  return {
    type: LOAD_CURRENT_ITEM,
    payload: payload,
  };
};
