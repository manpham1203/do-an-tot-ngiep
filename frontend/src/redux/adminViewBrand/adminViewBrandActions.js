import { SET_OPEN_ADMIN_VIEW_BRAND, SET_CLOSE_ADMIN_VIEW_BRAND } from "./adminViewBrandConstants";

export const setOpenadminViewBrand = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_BRAND,
    payload: payload,
  };
};
export const setCloseadminViewBrand = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_BRAND,
    payload: payload,
  };
};
