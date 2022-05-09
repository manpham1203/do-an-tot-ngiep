import { SET_OPEN_ADMIN_VIEW_BRAND, SET_CLOSE_ADMIN_VIEW_BRAND } from "./adminViewBrandConstants";

const initialState = {
  show: false,
  id: null,
};

export const adminViewBrandReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_VIEW_BRAND:
      return {
        ...state,
        show: true,
        id: action.payload.id,
      };
    case SET_CLOSE_ADMIN_VIEW_BRAND:
      return {
        ...state,
        id: null,
        show:false,
      };
    default:
      return state;
  }
};
