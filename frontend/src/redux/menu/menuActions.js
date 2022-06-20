import { MENU } from "./menuConstans";

export const menu = (payload) => {
  return {
    type: MENU,
    payload: payload,
  };
};