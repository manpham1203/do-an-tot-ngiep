import {
    SET_DATA,
  } from "./checkoutConstants";
  
  const initialState = {};
  
  export const checkoutReducer = (state = initialState, action) => {
    switch (action.type) {
      case SET_DATA:
        return action.payload;
  
      default:
        return state;
    }
  };
  