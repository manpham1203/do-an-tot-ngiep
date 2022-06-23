import api from "../../apis/api";
import {
  GET_BRANDS_LOADING,
  GET_BRANDS_SUCCESS,
  GET_BRANDS_FAIL,
  SET_BRANDS_EMPTY,
} from "./brandsConstants";

// export const fetchProducts = () => {
//   return async function(dispatch){
//     const reponse= await api.get("/api/Product");
//     dispatch({type:FETCH_ALLPRODUCT, payload:reponse.data})
//   }
// };
export const fetchBrands = () => async (dispatch) => {
  dispatch({ type: GET_BRANDS_LOADING });
  try {
    const reponse = await api.get("/api/BrandFull");
    dispatch({ type: GET_BRANDS_SUCCESS, payload: reponse.data });
  } catch {
    dispatch({ type: GET_BRANDS_FAIL });
  }
};

export const setBrandsEmpty = () => {
  return {
    type: SET_BRANDS_EMPTY,
  };
};
export const getBrandsLoading = () => {
  return {
    type: GET_BRANDS_LOADING,
  };
};
export const getBrandSuccess = (data) => {
  return {
    type: GET_BRANDS_SUCCESS,
    payload: data,
  };
};
export const getBrandFail = () => {
  return {
    type: GET_BRANDS_FAIL,
  };
};
