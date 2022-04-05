import { LOGIN, LOGOUT } from "./userConstants";

const initState = { id: null };

export const userReducer = (state = initState, action) => {
  switch (action.type) {
    case LOGIN:
      return {
        ...state,
        id: action.payload,
      };
      case LOGOUT:
      return {
        ...state,
        id: null,
      };
    default:
      return state;
  }
};
