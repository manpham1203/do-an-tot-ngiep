import { SET_OPEN_ADMIN_VIEW_USER, SET_CLOSE_ADMIN_VIEW_USER } from "./adminViewUserConstants";

export const setOpenAdminViewUser = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_USER,
    payload: payload,
  };
};
export const setCloseAdminViewUser = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_USER,
    payload: payload,
  };
};
