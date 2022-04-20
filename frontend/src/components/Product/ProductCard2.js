import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { addToCart } from "../../redux/cart/cartActions";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import product from "../../assets/product.jpg";
import { AiFillStar, AiFillHeart } from "react-icons/ai";

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
    const check = cart.every((item) => {
      return item.cartId !== objCart.cartId;
    });
    if (cart.length < 8) {
      if (check) {
        dispatch(addToCart(objCart));
        toast.success(`sản phẩm "${name}" thêm vào giỏ hàng thành công`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        });
      } else {
        toast.warn("sản phẩm đã tồn tại trong giỏ hàng !", {
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
  return (
    <div className="flex flex-row rounded-[8px] gap-x-[25px] shadow-md bg-white">
      <div className="w-[200px] h-[200px]">
        <img
          src={props.image}
          alt=""
          className="w-full h-full object-cover object-center"
        />
      </div>
      <div className="flex flex-col justify-around">
        <div>
          <h2 onClick={() => navigate(`/san-pham/${props.slug}`)} className="font-normal cursor-pointer hover:underline underline-offset-4">
          {props.brandName}
          </h2>
          <h2 className="mt-[10px] cursor-pointer hover:underline underline-offset-4 font-medium">{props.name}</h2>
          <p className="mt-[10px]">
            {props.priceDiscount === 0 ? (
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
          <div className="flex flex-row text-[#F7BF63] mt-[10px]">
            <AiFillStar />
            <AiFillStar />
            <AiFillStar />
            <AiFillStar />
            <AiFillStar />
          </div>
        </div>

        <button
          onClick={() => addCart(props.id, props.name)}
          className="w-[180px] border-2 border-second h-[35px] mt-[10px]"
        >
          Thêm vào giỏ hàng
        </button>
      </div>
    </div>
  );
}

export default ProductCard2;
