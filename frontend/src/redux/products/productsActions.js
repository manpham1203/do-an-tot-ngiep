import api from "../../apis/api";
import {
  GET_PRODUCTS_LOADING,
  GET_PRODUCTS_SUCCESS,
  GET_PRODUCTS_FAIL,
  SET_PRODUCTS_EMPTY
} from "./productsConstants";

// export const fetchProducts = () => {
//   return async function(dispatch){
//     const reponse= await api.get("/api/Product");
//     dispatch({type:FETCH_ALLPRODUCT, payload:reponse.data})
//   }
// };
export const fetchProducts = () => async (dispatch) => {
  dispatch({ type: GET_PRODUCTS_LOADING });
  try {
    const reponse = await api.get("/api/ProductFull");
    // setTimeout(() => {
      dispatch({ type: GET_PRODUCTS_SUCCESS, payload: reponse.data });
    // }, 5000);
  } catch {
    dispatch({ type: GET_PRODUCTS_FAIL });
  }
};
export const getProductsLoading = () => {
  return {
    type: GET_PRODUCTS_LOADING,
  };
};
export const setProductsEmpty = () => {
  return {
    type: SET_PRODUCTS_EMPTY,
  };
};
export const getProductsSuccess = (payload) => {
  return {
    type: GET_PRODUCTS_SUCCESS,
    payload: payload,
  };
};
export const getProductsFail = () => {
  return {
    type: GET_PRODUCTS_FAIL,
  };
};