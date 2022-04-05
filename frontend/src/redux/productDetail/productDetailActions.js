import api from "../../apis/api";
import {
  GET_PRODUCTETAIL_LOADING,
  GET_PRODUCTETAIL_SUCCESS,
  GET_PRODUCTETAIL_FAIL,
} from "./productDetailConstants";

export const fetchProductDetail = (id) => async (dispatch) => {
  dispatch({ type: GET_PRODUCTETAIL_LOADING });
  try {
    const reponse = await api.get(`/api/ProductFull/${id}`);
    // setTimeout(() => {
      dispatch({ type: GET_PRODUCTETAIL_SUCCESS, payload: reponse.data });
    // }, 5000);
  } catch {
    dispatch({ type: GET_PRODUCTETAIL_FAIL });
  }
};
export const getProductDetailLoading = () => {
  return {
    type: GET_PRODUCTETAIL_LOADING,
  };
};
export const getProductDetailSuccess = (data) => {
  return {
    type: GET_PRODUCTETAIL_SUCCESS,
    payload: data,
  };
};
export const getProductDetailFail = () => {
  return {
    type: GET_PRODUCTETAIL_FAIL,
  };
};
