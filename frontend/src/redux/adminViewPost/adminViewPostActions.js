import { SET_OPEN_ADMIN_VIEW_POST, SET_CLOSE_ADMIN_VIEW_POST } from "./adminViewPostConstants";

export const setOpenAdminViewPost = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_POST,
    payload: payload,
  };
};
export const setCloseAdminViewPost = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_POST,
    payload: payload,
  };
};
