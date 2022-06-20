import {
  PRODUCTS_OF_CATEGORY_LOADING,
  PRODUCTS_OF_CATEGORY_SUCCESS,
  PRODUCTS_OF_CATEGORY_FAIL,
} from "./productsOfCategoryConstants";

const initialState = {
  loading: false,
  data: {},
  fail: false,
};
export const productsOfCategoryReducer = (state = initialState, action) => {
  switch (action.type) {
    case PRODUCTS_OF_CATEGORY_LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
        data: {},
      };
    case PRODUCTS_OF_CATEGORY_SUCCESS:
      return {
        ...state,
        loading: false,
        fail: false,
        data: { ...action.payload },
      };
    case PRODUCTS_OF_CATEGORY_FAIL:
      return {
        ...state,
        loading: false,
        fail: true,
        data: {},
      };
    default:
      return state;
  }
};
