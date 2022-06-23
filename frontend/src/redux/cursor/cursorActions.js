import { CURSOR_DEFAULT, CURSOR_WAIT } from "./cursorConstants";

export const cursorDefault = (payload) => {
    return {
      type: CURSOR_DEFAULT,
      payload: payload,
    };
  };
export const cursorWait = (payload) => {
  return {
    type: CURSOR_WAIT,
    payload: payload,
  };
};