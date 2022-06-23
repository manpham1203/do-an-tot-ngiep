import api from "../../apis/api";
import {
  PRODUCTS_OF_CATEGORY_LOADING,
  PRODUCTS_OF_CATEGORY_SUCCESS,
  PRODUCTS_OF_CATEGORY_FAIL,
} from "./productsOfCategoryConstants";
export const fetchProductsOfCategory = (payload) => async (dispatch) => {
  dispatch({ type: PRODUCTS_OF_CATEGORY_LOADING });
  try {
    const reponse = await api.get(`/api/CategoryFull/${payload}`);
    dispatch({ type: PRODUCTS_OF_CATEGORY_SUCCESS, payload: reponse.data });
  } catch {
    dispatch({ type: PRODUCTS_OF_CATEGORY_FAIL });
  }
};
