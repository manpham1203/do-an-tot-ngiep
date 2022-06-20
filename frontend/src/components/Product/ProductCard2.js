import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { addToCart } from "../../redux/cart/cartActions";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import product from "../../assets/product.jpg";
import { AiFillShopping, AiFillHeart } from "react-icons/ai";
import { FaHeart, FaRegEye } from "react-icons/fa";
import ShowStarAvg from "../../components/ShowStar/ShowStarAvg";
import Heart from "../Wishlist/Heart";
import { setOpen } from "../../redux/quickView/quickViewActions";

function ProductCard2(props) {
  const navigate = useNavigate();
  const store = useSelector((store) => store);
  const cart = store.cart;
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
    <div className="flex flex-row rounded-[8px] gap-x-[25px] shadow-md bg-white dark:bg-second text-second dark:text-third">
      <div className="w-[200px] h-[200px] flex-none p-[10px]">
        <img
          src={props.image}
          alt=""
          className="w-full h-full object-cover object-center rounded-[8px]"
        />
      </div>
      <div className=" w-full">
        <div className="w-full flex flex-col pr-[10px]">
          <h2
            onClick={() => navigate(`/san-pham?thuong-hieu=${props.brandSlug}`)}
            className="font-normal cursor-pointer hover:underline underline-offset-4"
          >
            {props.brandName}
          </h2>
          <h2
            onClick={() => navigate(`/san-pham/${props.slug}`)}
            className="mt-[10px] cursor-pointer hover:underline underline-offset-4 font-medium productCard2Name"
          >
            {props.name}
          </h2>
          <p className="mt-[10px]">
            {props.priceDiscount === null ? (
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
          </p>
          <div className="flex flex-row text-[#F7BF63] mt-[10px] gap-x-[5px]">
            <ShowStarAvg star={props.star} />
          </div>
        </div>

        <div className="w-full flex flex-row text-[20px] justify-evenly mt-[20px] h-[30px]">
          <button
            onClick={() => addCart(props.id, props.name)}
            className="w-fit"
          >
            <AiFillShopping />
          </button>
          <div className="border-r border-gray-400" />
          <button
            onClick={handleQuickView}
            className=" w-fit"
          >
            <FaRegEye />
          </button>
          <div className="border-r border-gray-400" />
          <Heart
            id={props.id}
            dislikeStyles="text-red-600 flex items-center cursor-pointer"
            likeStyles="text-gray-400 flex items-center cursor-pointer"
          />
        </div>
      </div>
    </div>
  );
}

export default ProductCard2;
