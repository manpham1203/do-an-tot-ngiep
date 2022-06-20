import {
  CATEGORY_LOADING,
  CATEGORY_SUCCESS,
  CATEGORY_FAIL,
} from "./categoryConstants";

const initialState = {
  loading: false,
  data: [],
  fail: false,
};
export const categoryReducer = (state = initialState, action) => {
  switch (action.type) {
    case CATEGORY_LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case CATEGORY_SUCCESS:
      return {
        ...state,
        loading: false,
        data: action.payload,
        fail: false,
      };
    case CATEGORY_FAIL:
      return {
        ...state,
        dataBrands: [],
        data: false,
        fail: true,
      };
    default:
      return state;
  }
};
