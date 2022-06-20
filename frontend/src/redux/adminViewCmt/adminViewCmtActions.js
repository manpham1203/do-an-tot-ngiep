import { SET_OPEN_ADMIN_VIEW_CMT, SET_CLOSE_ADMIN_VIEW_CMT } from "./adminViewCmtConstants";

export const setOpenadminViewCmt = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_CMT,
    payload: payload,
  };
};

export const setCloseadminViewCmt = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_CMT,
    payload: payload,
  };
};
