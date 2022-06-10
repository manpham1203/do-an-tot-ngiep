import { data } from "autoprefixer";
import React, { useEffect, useState, memo, useReducer } from "react";
import { useDispatch, useSelector } from "react-redux";
import CartItem from "../../../components/Cart/CartItem";
import { setData } from "../../../redux/checkout/checkoutActions";
import api from "../../../apis/api";
import { Link, useNavigate } from "react-router-dom";
import { HiOutlineArrowNarrowRight } from "react-icons/hi";
import { toast } from "react-toastify";
import {
  setCart,
  removeFromCart,
  setCartEmpty,
} from "../../../redux/cart/cartActions";

const initState = {
  loading: false,
  fail: false,
  data: [],
};
//action
const LOADING = "LOADING";
const SUCCESS = "SUCCESS";
const FAIL = "FAIL";
const loading = () => {
  return {
    type: LOADING,
  };
};
const success = (payload) => {
  return {
    type: SUCCESS,
    payload: payload,
  };
};
const fail = (payload) => {
  return {
    type: FAIL,
    payload: payload,
  };
};
const reducer = (state, action) => {
  switch (action.type) {
    case LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case SUCCESS:
      return {
        ...state,
        loading: false,
        fail: false,
        data: action.payload,
      };
    case FAIL:
      return {
        ...state,
        loading: false,
        fail: true,
      };
    default:
      return state;
  }
};

function Abc() {
  const navigate = useNavigate();
  return (
    <div className="cursor-default">
      <h2>Bạn cần phải đăng nhập để tiến hành thanh toán</h2>
      <button
        className="hover:underline underline-offset-4 font-semibold"
        onClick={() => navigate("/dang-nhap")}
      >
        Tiến hành đăng nhập...
      </button>
    </div>
  );
}
function Cart() {
  document.title = "Giỏ Hàng";
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [cartData, setCartData] = useState([]);
  const [state, dispatchProduct] = useReducer(reducer, initState);
  const { cart, user } = useSelector((state) => state);

  const fetchProducts = async (arr) => {
    dispatchProduct(loading());
    await api({
      method: "POST",
      url: `/Product/cartrows`,
      data: arr,
    })
      .then((res) => {
        dispatchProduct(success(res.data));
      })
      .catch(() => dispatchProduct(fail()));
  };

  useEffect(() => {
    var newArr = [];
    for (var i = 0; i < cart.length; i++) {
      newArr.push(cart[i].cartId);
    }
    fetchProducts(newArr);
  }, []);

  useEffect(() => {
    var newArr = [];
    for (var i = 0; i < cart.length; i++) {
      for (var j = 0; j < state.data.length; j++) {
        if (state.data[j].id === cart[i].cartId) {
          newArr.push({
            item: state.data[j],
            qty: cart[i].qty,
          });
        }
      }
    }
    // dispatch(setCartEmpty());
    // dispatch(setCart(cartData));
    if (newArr.length > 0) {
      for (let m of cart) {
        var check = newArr.find((x) => x.item.id === m.cartId);
        // if (!check) {
        //   console.log("check", check, m);
        //   dispatch(removeFromCart(m.cartId));
        // }
        if (check === undefined) {
          dispatch(removeFromCart(m.cartId));
        }
        console.log("check", check);
      }
    }

    setCartData(newArr);
  }, [cart, state.data]);

  const handleCheckout = () => {
    if (user.id == null) {
      toast.warn(<Abc />, {
        position: toast.POSITION.TOP_RIGHT,
        autoClose: 3000,
      });
      return;
    }
    navigate("/thanh-toan");
  };
  return (
    <>
      {cart.length === 0 ? (
        <div className="pt-[30px] flex flex-col items-center container px-[10px] sm:px-[20px] mx-auto text-second dark:text-third">
          <h2 className="text-center">Bạn chưa thêm sản phẩm vào giỏ hàng</h2>
          <button
            onClick={() => navigate("/san-pham")}
            className=" px-[20px] h-[30px] border-2 border-second dark:border-third mt-[20px] flex flex-row items-center "
          >
            Tiếp tục mua hàng
            <HiOutlineArrowNarrowRight className="text-[20px]" />
          </button>
        </div>
      ) : (
        <div className=" container px-[10px] sm:px-[20px] mx-auto text-second dark:text-third">
          <div className="flex flex-col lg:flex-row gap-x-[25px]">
            <div className="w-full lg:w-3/4">
              <div className="hidden md:flex justify-between border-b pb-8">
                <h1 className="font-semibold text-2xl">Giỏ hàng</h1>
                <h2 className="font-semibold text-2xl">
                  {cart.length} sản phẩm
                </h2>
              </div>
              <h1 className="block md:hidden font-semibold text-2xl border-b pb-8 mb-[30px]">
                Giỏ hàng
              </h1>
              <div className="hidden md:flex mt-10 mb-5">
                <h3 className="font-semibold  text-xs uppercase w-2/5">
                  Sản phẩm
                </h3>
                <h3 className="font-semibold text-xs uppercase w-1/5 text-center">
                  Số lượng
                </h3>
                <h3 className="font-semibold  text-xs uppercase w-1/5 text-center">
                  Đơn giá
                </h3>
                <h3 className="font-semibold  text-xs uppercase w-1/5 text-center">
                  Thành tiền
                </h3>
              </div>

              {state.loading ? (
                <>loading</>
              ) : state.fail ? (
                <>fail</>
              ) : (
                cartData.map((cart) => {
                  return (
                    <CartItem
                      key={cart.item.id}
                      cartItem={cart.item}
                      qty={cart.qty}
                    />
                  );
                })
              )}

              <Link
                to="/san-pham"
                className="flex font-semiboldtext-sm mt-10 w-fit mb-[30px]"
              >
                Tiếp tục mua hàng...
              </Link>
            </div>

            <div className="w-full lg:w-1/4">
              <h1 className="font-semibold text-2xl border-b pb-8">Đơn hàng</h1>
              <div className="flex justify-between mt-10 mb-5">
                <span className="font-semibold text-sm uppercase">
                  {cart.length} sản phẩm
                </span>
                <span className="font-semibold text-sm">
                  {new Intl.NumberFormat("vi-VN", {
                    style: "currency",
                    currency: "VND",
                  }).format(
                    cartData.reduce((result, prod) => {
                      return result + prod.item.currentPrice * prod.qty;
                    }, 0)
                  )}
                </span>
              </div>

              <div className="border-t mt-8">
                <div className="flex font-semibold justify-between py-6 text-sm uppercase">
                  <span>Tổng thanh toán</span>
                  <span>
                    {}
                    {new Intl.NumberFormat("vi-VN", {
                      style: "currency",
                      currency: "VND",
                    }).format(
                      cartData.reduce((result, prod) => {
                        return result + prod.item.currentPrice * prod.qty;
                      }, 0)
                    )}
                  </span>
                </div>
                <button
                  onClick={handleCheckout}
                  className="bg-second font-semibold  py-3 text-sm text-third uppercase w-full"
                >
                  Thanh toán
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default memo(Cart);
