import { CURSOR_DEFAULT, CURSOR_WAIT } from "./cursorConstants";
const initialState = {
  wait: false,
};
export const cursorReducer = (state = initialState, action) => {
  switch (action.type) {
    case CURSOR_DEFAULT:
      return {
        ...state,
        wait: false,
      };
    case CURSOR_WAIT:
      return {
        ...state,
        wait: true,
      };
    default:
      return state;
  }
};
