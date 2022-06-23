import { SET_OPEN_ADMIN_VIEW_USER, SET_CLOSE_ADMIN_VIEW_USER } from "./adminViewUserConstants";

const initialState = {
  show: false,
  id: null,
  data:null
};

export const adminViewUserReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_VIEW_USER:
      return {
        ...state,
        show: true,
        id: action.payload.id,
        data:action.payload.data
      };
    case SET_CLOSE_ADMIN_VIEW_USER:
      return {
        ...state,
        id: null,
        show:false,
        data:null
      };
    default:
      return state;
  }
};
