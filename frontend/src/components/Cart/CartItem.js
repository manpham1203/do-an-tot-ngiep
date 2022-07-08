import React, { useState, memo, useEffect } from "react";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { useDispatch, useSelector } from "react-redux";
import autoMergeLevel2 from "redux-persist/lib/stateReconciler/autoMergeLevel2";
import imgthumb from "../../assets/imgthumb.jpg";
import {
  decrementQTY,
  incrementQTY,
  adjustQty,
  removeFromCart,
} from "../../redux/cart/cartActions";

function CartItem(props) {
  const dispatch = useDispatch();
  const { cart } = useSelector((state) => state);

  const handleNumber = (e) => {
    const re = /^[0-9\b]+$/;
    if (e.target.value === "" || re.test(parseInt(e.target.value))) {
      dispatch(adjustQty({ cartId: props.cartItem.id, qty: e.target.value }));
    }
  };
  const handleBlurNumber = (e) => {
    if (e.target.value === "") {
      dispatch(adjustQty({ cartId: props.cartItem.id, qty: 1 }));
    }
    if (parseInt(e.target.value) < 1) {
      dispatch(adjustQty({ cartId: props.cartItem.id, qty: 1 }));
    }
    if (parseInt(e.target.value) >= props.cartItem.quantityInStock) {
      dispatch(adjustQty({ cartId: props.cartItem.id, qty: props.cartItem.quantityInStock }));
    }
  };

  console.log(props.qty)
  const tang = (a) => {
    if (props.cartItem.quantityInStock > props.qty) {
      dispatch(incrementQTY(a))

    }
  }
  const giam = (a) => {
    if(props.qty>0){

      dispatch(decrementQTY(a))
    }
    if(props.qty==1){
      dispatch(removeFromCart(props.cartItem.id));
    }
    
  }
 

  return (
    <div className="flex flex-col md:flex-row items-center hover:bg-gray-100 dark:hover:bg-second mb-[30px]">
      <div className="flex w-full md:w-2/5 mb-[10px] md:mb-0">
        <div className="w-[100px] h-[100px] flex-none p-[10px]">
          <img
            className="w-full h-full object-cover object-center"
            src={props.cartItem.imageSrc}
            alt=""
          />
        </div>
        <div className="flex flex-col justify-between ml-4 flex-grow">
          <span className="font-bold text-sm short-desc-postcard2">
            {props.cartItem.name}
          </span>
          <span className=" text-xs">
            {props.cartItem.brandNameVM.name}
          </span>
          <h2
            className="font-semibold hover:text-red-500 text-gray-500 dark:text-gray-200 dark:hover:text-red-500 text-xs cursor-pointer"
            onClick={() => dispatch(removeFromCart(props.cartItem.id))}
          >
            Xoá khỏi giỏ hàng
          </h2>
        </div>
      </div>

      <span className="text-center w-full md:w-1/5 font-semibold text-sm mb-[10px] md:mb-0">
        {new Intl.NumberFormat("vi-VN", {
          style: "currency",
          currency: "VND",
        }).format(props.cartItem.currentPrice)}
      </span>
      <div className="flex justify-center w-full md:w-1/5 items-center mb-[10px] md:mb-0">
        <div
          onClick={() => giam(props.cartItem.id)}
          className="cursor-pointer"
        >
          <BsDashLg />
        </div>

        <input
          className="number_cart-item mx-2 border text-center w-[60px] text-second"
          type="number"
          max="10"
          min="1"
          value={props.qty}
          onChange={(e) => handleNumber(e)}
          onBlur={(e) => handleBlurNumber(e)}
        />
        <div
          onClick={() => tang(props.cartItem.id)}
          className="cursor-pointer"
        >
          <BsPlusLg />
        </div>
      </div>
      <span className="text-center w-full md:w-1/5 font-semibold text-sm mb-[10px] md:mb-0">
        {new Intl.NumberFormat("vi-VN", {
          style: "currency",
          currency: "VND",
        }).format(props.cartItem.currentPrice * props.qty)}
      </span>
    </div>
  );
}

export default CartItem;
