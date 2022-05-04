import { data } from "autoprefixer";
import React, { useEffect, useState, memo, useReducer } from "react";
import { useDispatch, useSelector } from "react-redux";
import CartItem from "../../../components/Cart/CartItem";
import { setData } from "../../../redux/checkout/checkoutActions";
import api from "../../../apis/api";
import { Link, useNavigate } from "react-router-dom";
import { HiOutlineArrowNarrowRight } from "react-icons/hi";
import { toast } from "react-toastify";
import { setCart, removeFromCart,setCartEmpty } from "../../../redux/cart/cartActions";

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
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [cartData, setCartData] = useState([]);
  const [state, dispatchProduct] = useReducer(reducer, initState);
  const { cart, user } = useSelector((state) => state);
  document.title = "Giỏ Hàng";

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
  }, [cart]);

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
    // for (let m of cart) {
    //   var check = newArr.find((x) => x.item.id === m.cartId);
    //   // if (!check) {
    //   //   console.log("check", check, m);
    //   //   dispatch(removeFromCart(m.cartId));
    //   // }
    //   if (check === undefined) {
    //     dispatch(removeFromCart(m.cartId));
    //   }
    //   console.log("check",check);
    // }
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

  console.log(state);
  return (
    <>
      {cart.length === 0 ? (
        <div className="pt-[30px] flex flex-col items-center">
          <h2 className="text-center">Bạn chưa thêm sản phẩm vào giỏ hàng</h2>
          <button
            onClick={() => navigate("/san-pham")}
            className=" px-[20px] h-[30px] border-2 border-second mt-[20px] flex flex-row items-center"
          >
            Tiếp tục mua hàng
            <HiOutlineArrowNarrowRight className="text-[20px]" />
          </button>
        </div>
      ) : (
        <div className="">
          <div className="flex my-10">
            <div className="w-3/4 px-10 py-[20px]">
              <div className="flex justify-between border-b pb-8">
                <h1 className="font-semibold text-2xl">Giỏ hàng</h1>
                <h2 className="font-semibold text-2xl">
                  {cart.length} sản phẩm
                </h2>
              </div>
              <div className="flex mt-10 mb-5">
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-2/5">
                  Sản phẩm
                </h3>
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-1/5 text-center">
                  Số lượng
                </h3>
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-1/5 text-center">
                  Đơn giá
                </h3>
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-1/5 text-center">
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
                className="flex font-semibold text-second text-sm mt-10 w-fit"
              >
                Tiếp tục mua hàng...
              </Link>
            </div>

            <div id="summary" className="w-1/4 px-8 py-[20px]">
              <h1 className="font-semibold text-2xl border-b pb-8">Đơn hàng</h1>
              <div className="flex justify-between mt-10 mb-5">
                <span className="font-semibold text-sm uppercase">
                  {cart.length} sản phẩm
                </span>
                <span className="font-semibold text-sm">
                  {cartData.reduce((result, prod) => {
                    return result + prod.item.currentPrice * prod.qty;
                  }, 0)}
                </span>
              </div>
              {/* <div>
                <label className="font-medium inline-block mb-3 text-sm uppercase">
                  Shipping
                </label>
                <select className="block p-2 text-gray-600 w-full text-sm">
                  <option>Standard shipping - $10.00</option>
                </select>
              </div>
              <div className="py-10">
                <label
                  htmlFor="promo"
                  className="font-semibold inline-block mb-3 text-sm uppercase"
                >
                  Promo Code
                </label>
                <input
                  type="text"
                  id="promo"
                  placeholder="Enter your code"
                  className="p-2 text-sm w-full"
                />
              </div>
              <button className="bg-red-500 hover:bg-red-600 px-5 py-2 text-sm text-white uppercase">
                Apply
              </button> */}
              <div className="border-t mt-8">
                <div className="flex font-semibold justify-between py-6 text-sm uppercase">
                  <span>Tổng thanh toán</span>
                  <span>
                    {cartData.reduce((result, prod) => {
                      return result + prod.item.currentPrice * prod.qty;
                    }, 0)}
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
