import api from "../../apis/api";
import {
  CATEGORY_LOADING,
  CATEGORY_SUCCESS,
  CATEGORY_FAIL,
} from "./categoryConstants";

// export const fetchProducts = () => {
//   return async function(dispatch){
//     const reponse= await api.get("/api/Product");
//     dispatch({type:FETCH_ALLPRODUCT, payload:reponse.data})
//   }
// };
export const fetchCategory = () => async (dispatch) => {
  dispatch({ type: CATEGORY_LOADING });
  try {
    const reponse = await api.get("/api/CategoryFull");
    dispatch({ type: CATEGORY_SUCCESS, payload: reponse.data });
  } catch {
    dispatch({ type: CATEGORY_FAIL });
  }
};
