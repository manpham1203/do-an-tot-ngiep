import React, { useState, memo } from "react";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { useDispatch, useSelector } from "react-redux";
import imgthumb from "../../assets/imgthumb.jpg";
import {
  decrementQTY,
  incrementQTY,
  adjustQty,
  removeFromCart
} from "../../redux/cart/cartActions";

function CartItem(props) {
  const dispatch = useDispatch();
  const handleNumber = (e) => {
    dispatch(adjustQty({ cartId: props.cartItem.id, qty: e.target.value }));
  };
  return (
    <div
      className="flex items-center hover:bg-gray-100 -mx-8 px-6 py-5"
      key="{props.id}"
    >
      <div className="flex w-2/5">
        <div className="w-20">
          <img className="h-24" src={imgthumb} alt="" />
        </div>
        <div className="flex flex-col justify-between ml-4 flex-grow">
          <span className="font-bold text-sm">{props.cartItem.name}</span>
          <span className="text-red-500 text-xs">
            {props.cartItem.brandVM.name}
          </span>
          <h2 className="font-semibold hover:text-red-500 text-gray-500 text-xs" onClick={()=>dispatch(removeFromCart(props.cartItem.id))}>
            Remove
          </h2>
        </div>
      </div>
      <div className="flex justify-center w-1/5 items-center">
        <div
          onClick={() => dispatch(decrementQTY(props.cartItem.id))}
          className="cursor-pointer"
        >
          <BsDashLg />
        </div>

        <input
          className="mx-2 border text-center w-8"
          type="text"
          value={props.qty}
          onChange={(e) => handleNumber(e)}
        />
        <div
          onClick={() => dispatch(incrementQTY(props.cartItem.id))}
          className="cursor-pointer"
        >
          <BsPlusLg />
        </div>
      </div>
      <span className="text-center w-1/5 font-semibold text-sm">
        {props.cartItem.price}
      </span>
      <span className="text-center w-1/5 font-semibold text-sm">
        {props.cartItem.price * props.qty}
      </span>
    </div>
  );
}

export default CartItem;