import { SET_OPEN_ADMIN_VIEW_PRODUCT, SET_CLOSE_ADMIN_VIEW_PRODUCT } from "./adminViewProductConstants";

export const setOpenAdminViewProduct = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_PRODUCT,
    payload: payload,
  };
};
export const setCloseAdminViewProduct = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_PRODUCT,
    payload: payload,
  };
};
