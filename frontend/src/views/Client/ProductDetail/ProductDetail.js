import React, { useEffect, useReducer, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import product from "../../../assets/product.jpg";
import product3 from "../../../assets/product3.jpg";
import product2 from "../../../assets/product2.jpg";
import { BsPlusLg, BsDashLg } from "react-icons/bs";

import { addToCart } from "../../../redux/cart/cartActions";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import ProductImageSlider from "../../../components/ProductImageSlider/ProductImageSlider";
import BrandWidget from "../../../components/Widget/BrandWidget";
import CategoryWidget from "../../../components/Widget/CategoryWidget";

import "swiper/css";
import "swiper/css/pagination";
import "swiper/css/thumbs";
import NewProductWidget from "../../../components/Widget/NewProductWidget";
import RelatedProducts from "../../../components/Product/RelatedProducts";
import Comment from "../../../components/Comment/Comment";
import ShowStarAvg from "../../../components/ShowStar/ShowStarAvg";

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
    setNumber(1);
    fetchProductDetail(slug);
  }, [slug]);

  useEffect(() => {
    window.scrollTo(0, 0);
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

  console.log("check", 4 < state.data.star < 5);

  return (
    <div className="container mx-auto">
      {state.loading ? (
        <>loading</>
      ) : state.fail ? (
        <>fail</>
      ) : (
        <div className="flex flex-col lg:flex-row items-center lg:items-start justify-center gap-x-[20px] ">
          <div className="w-full p-[20px] flex flex-col-reverse lg:flex-row h-fit gap-x-[20px] lg:items-start items-center justify-center">
            <ProductImageSlider images={state.data?.pictureVMs} />
          </div>

          <div className="w-full p-[20px]">
            <div className=" flex flex-col gap-y-[10px] items-center lg:items-start">
              <h2 className="text-[20px]">{state.data.brandNameVM?.name}</h2>
              <h2 className="text-[30px]">{state.data.name}</h2>
              <div className="flex flex-row items-center text-[#F7BF63] gap-x-[5px]">
                <ShowStarAvg star={state.data.star} />

                <span className="ml-[10px] text-[#3f3d4f]">
                  ({state.data.starCount} lượt đánh giá)
                </span>
              </div>
              {state.data.priceDiscount === null ? (
                <span className="text-[28px] mt-[10px]">
                  {new Intl.NumberFormat("vi-VN", {
                    style: "currency",
                    currency: "VND",
                  }).format(state.data.price)}
                </span>
              ) : (
                <div className="flex flex-row gap-x-[20px]">
                  <span className="text-[28px] mt-[10px] ">
                    {new Intl.NumberFormat("vi-VN", {
                      style: "currency",
                      currency: "VND",
                    }).format(state.data.priceDiscount)}
                  </span>
                  <span className="text-[28px] mt-[10px] line-through opacity-[0.5]">
                    {new Intl.NumberFormat("vi-VN", {
                      style: "currency",
                      currency: "VND",
                    }).format(state.data.price)}
                  </span>
                </div>
              )}
              <div className="mt-[10px]">
                <span className="font-medium">Lượt thích: </span>
                {state.data.like}
              </div>
              <div className="mt-[10px]">
                <span className="font-medium">Lượt xem: </span>
                {state.data.view}
              </div>

              <div className="text-justify">{state.data.shortDescription}</div>

              <div className="mt-[10px]">
                <div className="">
                  <span className="font-medium">Loại sản phẩm: </span>
                  {state.data.categoryNameVMs?.map((item) => {
                    return (
                      <span
                        key={item.id}
                        className="cursor-pointer hover:underline underline-offset-4"
                        onClick={() =>
                          navigate(`/san-pham?&category=${item.slug}`)
                        }
                      >
                        {item.name},&nbsp;
                      </span>
                    );
                  })}
                </div>
              </div>

              <div className="flex flex-row items-center mt-[20px] border border-gray-400">
                <div
                  className="cursor-pointer px-[10px] border-r border-gray-400 h-[40px] flex items-center"
                  onClick={() =>
                    setNumber((number) => (number <= 1 ? 1 : number - 1))
                  }
                >
                  <BsDashLg />
                </div>
                <input
                  className="number_cart-item w-[100px] text-center h-[40px] "
                  type="number"
                  value={number}
                  onChange={(e) => onHandleNumber(e)}
                  min="1"
                />
                <div
                  className="cursor-pointer px-[10px] border-l border-gray-400 h-[40px] flex items-center"
                  onClick={() =>
                    setNumber((number) => (number >= 9 ? 9 : number + 1))
                  }
                >
                  <BsPlusLg />
                </div>
              </div>
              <button
                className="mt-[20px] p-[10px] border-2 border-second font-medium"
                onClick={() => addCart(state.data.id, number)}
              >
                THÊM VÀO GIỎ HÀNG
              </button>
            </div>
          </div>
        </div>
      )}
      <div className="flex flex-row gap-x-[25px]">
        <div className="w-full">
          <h2 className="text-[25px] mb-[25px]">Mô tả sản phẩm</h2>
          <div
            className="text-justify"
            dangerouslySetInnerHTML={{ __html: state.data.fullDescription }}
          ></div>
          <h2 className="text-[25px] mb-[25px] mt-[25px]">Đánh giá sản phẩm</h2>
          {state?.data?.id && <Comment id={state.data.id} />}
        </div>
        <div className="w-[350px]">
          <BrandWidget />
          <CategoryWidget />
          <NewProductWidget />
        </div>
      </div>
      <div>
        {state.data?.brandId && (
          <RelatedProducts brandId={state.data?.brandId} />
        )}
      </div>
    </div>
  );
}

export default ProductDetail;
