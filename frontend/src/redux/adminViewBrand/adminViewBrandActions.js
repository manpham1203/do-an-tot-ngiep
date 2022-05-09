import { SET_OPEN_ADMIN_VIEW_PRODUCT, SET_CLOSE_ADMIN_VIEW_PRODUCT } from "./adminViewBrandConstants";

export const setOpenadminViewBrand = (payload) => {
  return {
    type: SET_OPEN_ADMIN_VIEW_PRODUCT,
    payload: payload,
  };
};
export const setCloseadminViewBrand = (payload) => {
  return {
    type: SET_CLOSE_ADMIN_VIEW_PRODUCT,
    payload: payload,
  };
};
