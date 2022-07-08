import {
  ADD_TO_CART,
  REMOVER_FROM_CART,
  ADJUST_QTY,
  INCREMENT_QTY,
  DECREMENT_QTY,
  SET_CART_EMPTY,
  SET_CART,
} from "./cartConstant";

const initialState = [];

export const cartReducer = (state = initialState, action) => {
  switch (action.type) {
    case ADD_TO_CART:
      const check = state.find((x) => x.cartId === action.payload.cartId);
      if (check) {
        return state.map((x) =>
          x.cartId === check.cartId
            ? { ...x, qty: x.qty + action.payload.qty }
            : x
        );
      }
      return [
        ...state,
        {
          cartId: action.payload.cartId,
          qty: action.payload.qty,
        },
      ];
    case SET_CART:
      return action.payload;

    case REMOVER_FROM_CART:
      return state.filter((item) => item.cartId !== action.payload);
    case ADJUST_QTY:
      return state.map((item) =>
        item.cartId === action.payload.cartId
          ? {
              ...item,
              qty: action.payload.qty,
            }
          : item
      );
    case DECREMENT_QTY:
      return state.map((item) => {
        return item.cartId === action.payload
          ? { ...item, qty: item.qty <= 1 ? 1 : item.qty - 1 }
          : item;
      });

    case INCREMENT_QTY:
      return state.map((item) =>
        item.cartId === action.payload ? { ...item, qty: item.qty + 1 } : item
      );
    case SET_CART_EMPTY:
      return [];

    default:
      return state;
  }
};
