import {
  GET_BRANDS_LOADING,
  GET_BRANDS_SUCCESS,
  GET_BRANDS_FAIL,
  SET_BRANDS_EMPTY,
} from "./brandsConstants";

const initialState = {
  loading: false,
  dataBrands: [],
  fail: false,
};

export const brandsReducer = (state = initialState, action) => {
  switch (action.type) {
    // case FETCH_BRAND:
    //   return {
    //     ...state,
    //     loading: false,
    //     dataBrand: action.payload,
    //     error: false,
    //   };
    case SET_BRANDS_EMPTY:
      return {
        ...state,
        loading: false,
        fail: false,
        dataBrands:[]
      };
    case GET_BRANDS_LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case GET_BRANDS_SUCCESS:
      return {
        ...state,
        loading: false,
        dataBrands: action.payload,
        fail: false,
      };
    case GET_BRANDS_FAIL:
      return {
        ...state,
        dataBrands: [],
        loading: false,
        fail: true,
      };
    default:
      return state;
  }
};
