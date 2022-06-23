import {
  SET_OPEN_ADMIN_VIEW_CMT,
  SET_CLOSE_ADMIN_VIEW_CMT,
  SET_CMT_DATA
} from "./adminViewCmtConstants";

const initialState = {
  show: false,
  id: null,
};

export const adminViewCmtReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_VIEW_CMT:
      return {
        ...state,
        show: true,
        id: action.payload.id,
      };
    case SET_CLOSE_ADMIN_VIEW_CMT:
      return {
        ...state,
        id: null,
        show: false,
      };
    default:
      return state;
  }
};
