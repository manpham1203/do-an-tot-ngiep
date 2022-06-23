import { SET_OPEN_ADMIN_VIEW_POST, SET_CLOSE_ADMIN_VIEW_POST } from "./adminViewPostConstants";

const initialState = {
  show: false,
  id: null,
};

export const adminViewPostReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_VIEW_POST:
      return {
        ...state,
        show: true,
        id: action.payload.id,
      };
    case SET_CLOSE_ADMIN_VIEW_POST:
      return {
        ...state,
        id: null,
        show:false,
      };
    default:
      return state;
  }
};
