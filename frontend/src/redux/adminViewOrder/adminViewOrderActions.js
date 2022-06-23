import { SET_OPEN_ADMIN_VIEW_ORDER, SET_CLOSE_ADMIN_VIEW_ORDER } from "./adminViewOrderConstants";

export const setOpenAdminViewOrder = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_ORDER,
    payload: payload,
  };
};
export const setCloseAdminViewOrder = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_ORDER,
    payload: payload,
  };
};
