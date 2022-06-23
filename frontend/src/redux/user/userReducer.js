import { LOGIN, LOGOUT } from "./userConstants";

const initState = { id: null, role:0 };

export const userReducer = (state = initState, action) => {
  switch (action.type) {
    case LOGIN:
      return {
        ...state,
        id: action.payload.id,
        role:action.payload.role
      };
      case LOGOUT:
      return {
        ...state,
        id: null,
        role:0
      };
    default:
      return state;
  }
};
