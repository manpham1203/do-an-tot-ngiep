import {
  GET_PRODUCTETAIL_LOADING,
  GET_PRODUCTETAIL_SUCCESS,
  GET_PRODUCTETAIL_FAIL,
} from "./productDetailConstants";

const initialState = {
  loading: false,
  dataProduct: {},
  fail: false,
};

export const productDetailReducer = (state = initialState, action) => {
  switch (action.type) {
    case GET_PRODUCTETAIL_LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };

    case GET_PRODUCTETAIL_SUCCESS:
      return {
        ...state,
        loading: false,
        dataProduct: action.payload,
        fail: false,
      };

    case GET_PRODUCTETAIL_FAIL:
      return {
        ...state,
        dataProduct: {},
        loading: false,
        fail: true,
      };

    default:
      return state;
  }
};
