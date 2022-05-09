import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { BsFillBagFill } from "react-icons/bs";
import { FaRegEye } from "react-icons/fa";
import { useDispatch, useSelector } from "react-redux";
import { addToCart } from "../../redux/cart/cartActions";
import { setOpen } from "../../redux/quickView/quickViewActions";
import { toast } from "react-toastify";
import Heart from "../Wishlist/Heart";
import ShowStarAvg from "../../components/ShowStar/ShowStarAvg";
import imgthumb from '../../assets/imgthumb.jpg'

function ProductCard(props) {
  const navigate = useNavigate();
  const { cart } = useSelector((store) => store);
  const dispatch = useDispatch();
  const addCart = (id, name) => {
    var objCart = {
      cartId: id,
      qty: 1,
    };
    // const check = cart.every((item) => {
    //   return item.cartId !== objCart.cartId;
    // });
    const check = cart.some((x) => x.cartId === objCart.cartId);
    const check2 = cart.find((x) => x.cartId === objCart.cartId);
    if (cart.length <= 8) {
      if (check) {
        if (check2.qty <= 8) {
          dispatch(addToCart(objCart));
          toast.success(`sản phẩm "${name}" thêm vào giỏ hàng thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        } else {
          toast.warn("Sản phẩm đã đạt số lượng tối đa !", {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      } else {
        dispatch(addToCart(objCart));
        toast.success(`sản phẩm "${name}" thêm vào giỏ hàng thành công`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        });
      }
    } else {
      toast.warn("Giỏ hàng đã đầy !", {
        position: toast.POSITION.TOP_RIGHT,
        autoClose: 3000,
      });
    }
  };
  const handleQuickView = () => {
    const obj = {
      show: true,
      id: props.slug,
    };
    dispatch(setOpen(obj));
  };

  return (
    <div className="w-full h-fit relative group  rounded-[8px] overflow-hidden shadow-md hover:shadow-xl">
      <div className="w-full overflow-hidden relative">
        <img
          src={props.image || imgthumb}
          alt="alt"
          className="w-full object-center object-cover"
        />
        <div className=" flex flex-col absolute top-[10px] right-[-44px] gap-[10px] group-hover:right-[10px] transition-all duration-[0.3s]">
          <div
            onClick={() => addCart(props.id, props.name)}
            className="flex justify-center items-center w-[40px] h-[40px] hover:bg-white bg-gray-50 rounded-[500px] cursor-pointer shadow-md "
          >
            <BsFillBagFill />
          </div>
          <div
            onClick={handleQuickView}
            className="flex justify-center items-center w-[40px] h-[40px] hover:bg-white bg-gray-50 rounded-[500px] cursor-pointer shadow-md"
          >
            <FaRegEye />
          </div>
          <Heart
            id={props.id}
            dislikeStyles="flex justify-center items-center w-[40px] h-[40px] text-red-600 hover:bg-white bg-gray-50 rounded-[500px] cursor-pointer shadow-md"
            likeStyles="flex justify-center items-center w-[40px] h-[40px] text-gray-400 hover:bg-white bg-gray-50 rounded-[500px] cursor-pointer shadow-md"
          />
        </div>
      </div>
      <div className="w-full flex flex-col items-center p-[10px] gap-y-[10px] bg-white bottom-[-44px]">
        <h3
          className="cursor-pointer text-second font-normal hover:underline underline-offset-4"
          onClick={() => navigate(`/san-pham?brand=${props.brandSlug}`)}
        >
          {props.brandName}
        </h3>
        <h3
          className="cursor-pointer font-primary font-normal text-[20px] hover:underline underline-offset-4 mb-[5px] truncate w-full text-center"
          onClick={() => navigate(`/san-pham/${props.slug}`)}
        >
          {props.name}
        </h3>
        <div className="flex flex-row text-[#F7BF63]">
          <ShowStarAvg star={props.star} />
        </div>
        <div className="flex flex-round gap-x-[20px] mt-[10px]">
          {props.priceDiscount == null ? (
            <span>
              {new Intl.NumberFormat("vi-VN", {
                style: "currency",
                currency: "VND",
              }).format(props.price)}
            </span>
          ) : (
            <>
              <span className="font-bold">
                {new Intl.NumberFormat("vi-VN", {
                  style: "currency",
                  currency: "VND",
                }).format(props.priceDiscount)}
              </span>
              <span className="line-through">
                {new Intl.NumberFormat("vi-VN", {
                  style: "currency",
                  currency: "VND",
                }).format(props.price)}
              </span>
            </>
          )}
        </div>
      </div>
    </div>
  );
}

export default ProductCard;
