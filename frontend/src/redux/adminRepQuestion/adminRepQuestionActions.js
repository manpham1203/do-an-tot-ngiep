import { SET_OPEN_ADMIN_REP_QUESTION, SET_CLOSE_ADMIN_REP_QUESTION } from "./adminRepQuestionConstants";

export const setOpenadminRepQuestion = (payload) => {
  return {
    type: SET_OPEN_ADMIN_REP_QUESTION,
    payload: payload,
  };
};
export const setCloseadminRepQuestion = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_REP_QUESTION,
    payload: payload,
  };
};
