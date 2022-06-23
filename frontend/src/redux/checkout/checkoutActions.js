import { SET_DATA } from "./checkoutConstants";
export const setData = (payload) => {
  return {
    type: SET_DATA,
    payload: payload,
  };
};
