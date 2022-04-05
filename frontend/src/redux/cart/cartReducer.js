import {
  ADD_TO_CART,
  REMOVER_FROM_CART,
  ADJUST_QTY,
  INCREMENT_QTY,
  DECREMENT_QTY,
} from "./cartConstant";

const initialState = [];

export const cartReducer = (state = initialState, action) => {
  switch (action.type) {
    case ADD_TO_CART:
      return [
        ...state,
        {
          cartId: action.payload.cartId,
          qty: action.payload.qty,
        },
      ];

    case REMOVER_FROM_CART:
      return state.filter((item) => item.cartId !== action.payload);
    case ADJUST_QTY:
      return state.map((item) =>
        item.cartId === action.payload.cartId
          ? { ...item, qty: parseInt(action.payload.qty) }
          : item
      );
    case DECREMENT_QTY:
      return state.map((item) => {
        return item.cartId === action.payload
          ? { ...item, qty: item.qty - 1 }
          : item;
      });

    case INCREMENT_QTY:
      return state.map((item) => {
        return item.cartId === action.payload
          ? { ...item, qty: item.qty + 1 }
          : item;
      });

    default:
      return state;
  }
};
