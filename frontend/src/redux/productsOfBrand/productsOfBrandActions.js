import api from "../../apis/api";
import {
  PRODUCTS_OF_BRAND_LOADING,
  PRODUCTS_OF_BRAND_SUCCESS,
  PRODUCTS_OF_BRAND_FAIL,
} from "./productsOfBrandConstants";
export const fetchProductsOfBrand = (payload) => async (dispatch) => {
  dispatch({ type: PRODUCTS_OF_BRAND_LOADING });
  try {
    const reponse = await api.get(`/api/BrandFull/${payload}`);
    dispatch({ type: PRODUCTS_OF_BRAND_SUCCESS, payload: reponse.data });
  } catch {
    dispatch({ type: PRODUCTS_OF_BRAND_FAIL });
  }
};
