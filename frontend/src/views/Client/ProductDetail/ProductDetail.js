import React, { useEffect, useReducer, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import product from "../../../assets/product.jpg";
import product3 from "../../../assets/product3.jpg";
import product2 from "../../../assets/product2.jpg";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { AiFillStar } from "react-icons/ai";
import { addToCart } from "../../../redux/cart/cartActions";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import ProductImageSlider from "../../../components/ProductImageSlider/ProductImageSlider";

import "swiper/css";
import "swiper/css/pagination";
import "swiper/css/thumbs";

const initState = {
  loading: false,
  fail: false,
  data: {},
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
const fail = () => {
  return {
    type: FAIL,
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
function ProductDetail() {
  const [state, dispatchProduct] = useReducer(reducer, initState);
  const { slug } = useParams();
  const navigate = useNavigate();
  const [number, setNumber] = useState(1);
  const store = useSelector((store) => store);
  const cart = store.cart;
  const dispatch = useDispatch();
  // const carts = store.cart;
  document.title = `${state.data.name}`;

  const fetchProductDetail = async (slug) => {
    dispatchProduct(loading());
    await api({
      method: "GET",
      url: `/Product/productdetail/${slug}`,
      data: null,
    })
      .then((res) => {
        dispatchProduct(success(res.data));
      })
      .catch(() => dispatchProduct(fail()));
  };
  useEffect(() => {
    fetchProductDetail(slug);
  }, [slug]);

  const addCart = (id, qty) => {
    var objCart = {
      cartId: id,
      qty: qty,
    };
    const check = cart.every((item) => {
      return item.cartId !== objCart.cartId;
    });
    if (cart.length < 8) {
      if (check) {
        dispatch(addToCart(objCart));
        toast.success(
          `sản phẩm "${state.data.name}" thêm vào giỏ hàng thành công`,
          {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          }
        );
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

  const onHandleNumber = (e) => {
    const re = /^[1-9\b]+$/;

    if (e.target.value <= 1) {
      setNumber(1);
    }
    if (e.target.value >= 9) {
      setNumber(9);
    }
    if (e.target.value <= 9 && e.target.value >= 1) {
      if (e.target.value === "" || re.test(e.target.value)) {
        setNumber(e.target.value);
      }
    }
  };

  console.log(state);

  return (
    <div className="pt-[100px] container mx-auto">
      {state.loading ? (
        <>loading</>
      ) : state.fail ? (
        <>fail</>
      ) : (
        <div className="flex flex-row gap-x-[20px] ">
          <div className="w-[50%] p-[20px] flex flex-row">
            <ProductImageSlider images={state.data?.productImageVMs} />
          </div>

          <div className="w-[50%] p-[20px]">
            <div className=" flex flex-col gap-y-[10px] items-center bg-gradient-to-b from-[white] to-transparent ">
              <h2>{state.data.brandNameVM?.name}</h2>
              <h2 className="text-[30px] mt-[5px]">{state.data.name}</h2>
              <div className="flex flex-row items-center text-[#F7BF63]">
                <AiFillStar />
                <AiFillStar />
                <AiFillStar />
                <AiFillStar />
                <AiFillStar />
                <span className="ml-[10px] text-[#3f3d4f]">
                  (1 lượt đánh giá)
                </span>
              </div>
              <span className="mt-[10px]">Lượt thích: {state.data.likes}</span>
              <span className="mt-[10px]">Lượt xem: {state.data.likes}</span>
              {state.data.priceDiscount === 0 ? (
                <span className="text-[25px] mt-[10px]">
                  {new Intl.NumberFormat("vi-VN", {
                    style: "currency",
                    currency: "VND",
                  }).format(state.data.price)}
                </span>
              ) : (
                <div className="flex flex-row gap-x-[20px]">
                  <span className="text-[25px] mt-[10px] font-semibold">
                    {new Intl.NumberFormat("vi-VN", {
                      style: "currency",
                      currency: "VND",
                    }).format(state.data.priceDiscount)}
                  </span>
                  <span className="text-[25px] mt-[10px] line-through">
                    {new Intl.NumberFormat("vi-VN", {
                      style: "currency",
                      currency: "VND",
                    }).format(state.data.price)}
                  </span>
                </div>
              )}

              <span className="flex flex-row mt-[10px]">
                Loại sản phẩm:
                {state.data.categoryNameVMs?.map((item) => {
                  return (
                    <p
                      key={item.id}
                      className="cursor-pointer"
                      onClick={() => navigate(`/category/${item.id}`)}
                    >
                      {item.name},&nbsp;
                    </p>
                  );
                })}
              </span>

              <div className="flex flex-row items-center gap-x-[5px] mt-[20px]">
                <div
                  className="cursor-pointer"
                  onClick={() => setNumber((number) => number<=1?1: number - 1)}
                >
                  <BsDashLg />
                </div>
                <input
                  type="number"
                  className="number_cart-item border border-gray-400 w-[50px] text-center"
                  value={number}
                  onChange={(e) => onHandleNumber(e)}
                  min="1"
                />
                <div
                  className="cursor-pointer"
                  onClick={() => setNumber((number) => number>=9?9: number + 1)}
                >
                  <BsPlusLg />
                </div>
              </div>
              <button
                className="mt-[20px] p-[10px] bg-[#F8F7F4] border border-[#161a2133]"
                onClick={() => addCart(state.data.id, number)}
              >
                THÊM VÀO GIỎ HÀNG
              </button>
            </div>
          </div>
        </div>
      )}

      <div className="">
        <h1>Description: </h1>
        <div
          dangerouslySetInnerHTML={{ __html: state.data.fullDescription }}
        ></div>
      </div>
    </div>
  );
}

export default ProductDetail;
