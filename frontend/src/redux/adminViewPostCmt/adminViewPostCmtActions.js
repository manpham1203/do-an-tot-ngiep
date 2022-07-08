import { SET_OPEN_ADMIN_VIEW_POST_CMT, SET_CLOSE_ADMIN_VIEW_POST_CMT } from "./adminViewPostCmtConstants";

export const setOpenadminViewPostCmt = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_POST_CMT,
    payload: payload,
  };
};

export const setCloseadminViewPostCmt = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_POST_CMT,
    payload: payload,
  };
};
