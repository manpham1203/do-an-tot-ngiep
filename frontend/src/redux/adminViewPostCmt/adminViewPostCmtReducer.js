import {
  SET_OPEN_ADMIN_VIEW_POST_CMT,
  SET_CLOSE_ADMIN_VIEW_POST_CMT,
} from "./adminViewPostCmtConstants";

const initialState = {
  show: false,
  id: null,
};

export const adminViewPostCmtReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_VIEW_POST_CMT:
      return {
        ...state,
        show: true,
        id: action.payload.id,
      };
    case SET_CLOSE_ADMIN_VIEW_POST_CMT:
      return {
        ...state,
        id: null,
        show: false,
      };
    default:
      return state;
  }
};
