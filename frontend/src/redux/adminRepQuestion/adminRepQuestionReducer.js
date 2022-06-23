import { SET_OPEN_ADMIN_REP_QUESTION, SET_CLOSE_ADMIN_REP_QUESTION } from "./adminRepQuestionConstants";

const initialState = {
  show: false,
  data: null,
};

export const adminRepQuestionReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN_ADMIN_REP_QUESTION:
      return {
        ...state,
        show: true,
        data: action.payload,
      };
    case SET_CLOSE_ADMIN_REP_QUESTION:
      return {
        ...state,
        data: null,
        show: false,
      };
    default:
      return state;
  }
};
