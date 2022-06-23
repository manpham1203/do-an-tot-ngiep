import {
  PRODUCTS_OF_BRAND_LOADING,
  PRODUCTS_OF_BRAND_SUCCESS,
  PRODUCTS_OF_BRAND_FAIL,
} from "./productsOfBrandConstants";

const initialState = {
  loading: false,
  data: {},
  fail: false,
};
export const productsOfBrandReducer = (state = initialState, action) => {
  switch (action.type) {
    case PRODUCTS_OF_BRAND_LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
        data: {},
      };
      case PRODUCTS_OF_BRAND_SUCCESS:
      return {
        ...state,
        loading: false,
        fail: false,
        data: {...action.payload},
      };
      case PRODUCTS_OF_BRAND_FAIL:
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
