import { SET_OPEN, SET_CLOSE } from "./quickViewConstants";

const initialState = {
  show: false,
  id: null,
};

export const quickViewReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_OPEN:
      return {
        ...state,
        show: true,
        id: action.payload.id,
      };
    case SET_CLOSE:
      return {
        ...state,
        id: null,
        show:false,
      };
    default:
      return state;
  }
};
