import { SET_OPEN_ADMIN_VIEW_PRODUCT, SET_CLOSE_ADMIN_VIEW_PRODUCT } from "./adminViewProductConstants";

const initialState = {
  show: false,
  id: null,
};

export const adminViewProductReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_VIEW_PRODUCT:
      return {
        ...state,
        show: true,
        id: action.payload.id,
      };
    case SET_CLOSE_ADMIN_VIEW_PRODUCT:
      return {
        ...state,
        id: null,
        show:false,
      };
    default:
      return state;
  }
};
