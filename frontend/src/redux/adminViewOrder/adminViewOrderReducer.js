import { SET_OPEN_ADMIN_VIEW_ORDER, SET_CLOSE_ADMIN_VIEW_ORDER } from "./adminViewOrderConstants";

const initialState = {
  show: false,
  id: null,
};

export const adminViewOrderReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_VIEW_ORDER:
      return {
        ...state,
        show: true,
        id: action.payload.id,
      };
    case SET_CLOSE_ADMIN_VIEW_ORDER:
      return {
        ...state,
        id: null,
        show:false,
      };
    default:
      return state;
  }
};
