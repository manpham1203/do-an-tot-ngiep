import { data } from "autoprefixer";
import React, {
  useEffect,
  useState,
  memo,
  useMemo,
  useRef,
  useReducer,
} from "react";
import { useDispatch, useSelector } from "react-redux";
import CartItem from "../../../components/Cart/CartItem";
import api from "../../../apis/api";

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

function Cart() {
  const dispatch = useDispatch();
  const [cartData, setCartData] = useState([]);
  const [state, dispatchProduct] = useReducer(reducer, initState);
  const store = useSelector((state) => state);
  const cart = store.cart;
  document.title = "Giỏ Hàng";

  const fetchProducts = async () => {
    dispatchProduct(loading());
    await api({
      method: "GET",
      url: `/api/productfull`,
      data: null,
    })
      .then((res) => {
        dispatchProduct(success(res.data));
      })
      .catch(() => dispatchProduct(fail()));
  };

  useEffect(() => {
    fetchProducts();
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
    setCartData(newArr);
  }, [cart, state.data]);

  return (
    <>
    {cart.length === 0 ? (
        <div className="pt-[100px]">Cart Empty</div>
      ) : (
        <div className="pt-[100px]">
          <div className="flex shadow-md my-10">
            <div className="w-3/4 bg-[#F8F7F4] px-10 py-10">
              <div className="flex justify-between border-b pb-8">
                <h1 className="font-semibold text-2xl">Shopping Cart</h1>
                <h2 className="font-semibold text-2xl">3 Items</h2>
              </div>
              <div className="flex mt-10 mb-5">
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-2/5">
                  Product Details
                </h3>
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-1/5 text-center">
                  Quantity
                </h3>
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-1/5 text-center">
                  Price
                </h3>
                <h3 className="font-semibold text-gray-600 text-xs uppercase w-1/5 text-center">
                  Total
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

              <h2 className="flex font-semibold text-indigo-600 text-sm mt-10">
                <svg
                  className="fill-current mr-2 text-indigo-600 w-4"
                  viewBox="0 0 448 512"
                >
                  <path d="M134.059 296H436c6.627 0 12-5.373 12-12v-56c0-6.627-5.373-12-12-12H134.059v-46.059c0-21.382-25.851-32.09-40.971-16.971L7.029 239.029c-9.373 9.373-9.373 24.569 0 33.941l86.059 86.059c15.119 15.119 40.971 4.411 40.971-16.971V296z" />
                </svg>
                Continue Shopping
              </h2>
            </div>

            <div id="summary" className="w-1/4 px-8 py-10">
              <h1 className="font-semibold text-2xl border-b pb-8">
                Order Summary
              </h1>
              <div className="flex justify-between mt-10 mb-5">
                <span className="font-semibold text-sm uppercase">Items 3</span>
                <span className="font-semibold text-sm">
                  {cartData.reduce((result, prod) => {
                    return result + prod.item.price * prod.qty;
                  }, 0)}
                </span>
              </div>
              <div>
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
              </button>
              <div className="border-t mt-8">
                <div className="flex font-semibold justify-between py-6 text-sm uppercase">
                  <span>Total cost</span>
                  <span>100</span>
                </div>
                <button className="bg-indigo-500 font-semibold hover:bg-indigo-600 py-3 text-sm text-white uppercase w-full">
                  Checkout
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
