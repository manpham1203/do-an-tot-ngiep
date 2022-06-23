import { SET_OPEN, SET_CLOSE } from "./quickViewConstants";

export const setOpen = (payload) => {
  return {
    type: SET_OPEN,
    payload: payload,
  };
};
export const setClose = (payload) => {
  return {
    type: SET_CLOSE,
    payload: payload,
  };
};
