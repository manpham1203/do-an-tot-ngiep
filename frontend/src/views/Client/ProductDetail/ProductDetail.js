import React, { useEffect, useReducer, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import product from "../../../assets/product.jpg";
import product3 from "../../../assets/product3.jpg";
import product2 from "../../../assets/product2.jpg";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { addToCart, adjustQty } from "../../../redux/cart/cartActions";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import ProductImageSlider from "../../../components/ProductImageSlider/ProductImageSlider";
import BrandWidget from "../../../components/Widget/BrandWidget";
import CategoryWidget from "../../../components/Widget/CategoryWidget";
import MostBoughtWidget from "../../../components/Widget/MostBoughtWidget";
import NewProductWidget from "../../../components/Widget/NewProductWidget";
import PageContent from '../../../components/Skeleton/PageContent'

import "swiper/css";
import "swiper/css/pagination";
import "swiper/css/thumbs";
import RelatedProducts from "../../../components/Product/RelatedProducts";
import ShowStarAvg from "../../../components/ShowStar/ShowStarAvg";
import ProductCmt from "../../../components/Comment/ProductCmt";
import OnSaleWidget from "../../../components/Widget/OnSaleWidget";

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
  document.title = "Sản phẩm: " + state?.data?.name || "Chi tiết sản phẩm";

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
    // const check = cart.every((item) => {
    //   return item.cartId !== objCart.cartId;
    // });
    const check = cart.some((x) => x.cartId === objCart.cartId);
    const check2 = cart.find((x) => x.cartId === objCart.cartId);
    if (cart.length <= 8) {
      if (check) {
        if (check2.qty <= 8) {
          if (check2.qty + number > 9) {
            dispatch(adjustQty({ cartId: check2.cartId, qty: 9 }));
            toast.warn(`sản phẩm đã đạt số lượng tối đa`, {
              position: toast.POSITION.TOP_RIGHT,
              autoClose: 3000,
            });
          } else {
            dispatch(addToCart(objCart));
            toast.success(
              `sản phẩm "${state.data.name}" thêm vào giỏ hàng thành công`,
              {
                position: toast.POSITION.TOP_RIGHT,
                autoClose: 3000,
              }
            );
          }
        } else {
          toast.warn("Sản phẩm đã đạt số lượng tối đa !", {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      } else {
        dispatch(addToCart(objCart));
        toast.success(
          `sản phẩm "${state.data.name}" thêm vào giỏ hàng thành công`,
          {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          }
        );
      }
    } else {
      toast.warn("Giỏ hàng đã đầy !", {
        position: toast.POSITION.TOP_RIGHT,
        autoClose: 3000,
      });
    }
  };

  const handleNumber = (e) => {
    const re = /^[0-9\b]+$/;
    if (e.target.value === "" || re.test(e.target.value)) {
      setNumber(e.target.value);
    }
  };
  const handleBlurNumber = (e) => {
    if (e.target.value === "" || e.target.value < 1) {
      setNumber(1);
    }
    if (e.target.value > 9) {
      setNumber(9);
    }
  };

  useEffect(() => {
    if (state?.data?.id !== null) {
      var timer = setTimeout(async () => {
        await api({
          method: "PUT",
          url: `/product/increaseview`,
          params: { id: state.data.id },
        })
          .then((res) => {
            if (res.status === 200) {
              console.log("increase success");
            } else {
              console.log("increase fail");
            }
          })
          .catch(() => {
            console.log("increase fail");
          });
      }, 5000);
      return () => clearTimeout(timer);
    }
  }, [state?.data?.id]);

  return state.loading ? (
    <PageContent />
  ) : state.fail ? (
    <PageContent />
  ) : (
    <div className="container mx-auto px-[10px] sm:px-[20px] text-second dark:text-third">
      <div className="flex flex-col lg:flex-row items-center lg:items-start justify-center gap-x-[20px] ">
        <div className="w-full p-[20px] flex flex-col-reverse lg:flex-row h-fit gap-x-[20px] lg:items-start items-center justify-center">
          <ProductImageSlider images={state.data?.pictureVMs} />
        </div>

        <div className="w-full p-[20px]">
          <div className=" flex flex-col gap-y-[10px] items-center lg:items-start">
            <h2 className="text-[18px] sm:text-[20px]">
              {state.data.brandNameVM?.name}
            </h2>
            <h2 className="text-[20px] sm:text-[25px] lg:text-[30px]">
              {state.data.name}
            </h2>
            <div className="flex flex-row items-center text-[#F7BF63] gap-x-[5px]">
              <ShowStarAvg star={state.data.star} />

              <span className="ml-[10px] text-second dark:text-third">
                ({state.data.starCount} lượt đánh giá)
              </span>
            </div>
            {state.data.priceDiscount === null ? (
              <span className="text-[20px] md:text-[23px] sm:text-[25px] lg:text-[28px]  mt-[10px]">
                {new Intl.NumberFormat("vi-VN", {
                  style: "currency",
                  currency: "VND",
                }).format(state.data.price)}
              </span>
            ) : (
              <div className="flex flex-row gap-x-[20px]">
                <span className="text-[20px] md:text-[23px] sm:text-[25px] lg:text-[28px]  mt-[10px] ">
                  {new Intl.NumberFormat("vi-VN", {
                    style: "currency",
                    currency: "VND",
                  }).format(state.data.priceDiscount)}
                </span>
                <span className="text-[20px] md:text-[23px] sm:text-[25px] lg:text-[28px] mt-[10px] line-through opacity-[0.5]">
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

            <div className="mt-[10px]">
              <div className="">
                <span className="font-medium">Loại sản phẩm: </span>
                {state.data.categoryNameVMs?.map((item, index) => {
                  return index + 1 === state.data.categoryNameVMs.length ? (
                    <span
                      key={item.id}
                      className="cursor-pointer hover:underline underline-offset-4"
                      onClick={() =>
                        navigate(`/san-pham?&danh-muc=${item.slug}`)
                      }
                    >
                      {item.name}
                    </span>
                  ) : (
                    <span
                      key={item.id}
                      className="cursor-pointer hover:underline underline-offset-4"
                      onClick={() =>
                        navigate(`/san-pham?&danh-muc=${item.slug}`)
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
                className="number_cart-item w-[100px] text-center h-[40px] text-second"
                type="number"
                value={number}
                onChange={(e) => handleNumber(e)}
                onBlur={(e) => handleBlurNumber(e)}
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
              className="mt-[20px] p-[10px] bg-second text-third font-medium flex flex-row items-center"
              onClick={() => addCart(state.data.id, number)}
            >
              THÊM VÀO GIỎ HÀNG
            </button>
          </div>
        </div>
      </div>
      <div className="flex flex-row gap-x-[25px]">
        <div className="w-full">
          <h2 className="text-[20px]  md:text-[25px] mb-[25px]">
            Mô tả sản phẩm
          </h2>
          <div
            className="text-justify"
            dangerouslySetInnerHTML={{ __html: state.data.description }}
          ></div>
          <h2 className="text-[20px]  md:text-[25px]  mb-[25px] mt-[25px]">
            Đánh giá sản phẩm
          </h2>
          {state?.data?.id && <ProductCmt id={state.data.id} />}
        </div>
        <div className="w-[300px] xl:w-[350px] hidden lg:block flex-none">
          <BrandWidget />
          <CategoryWidget />
          {/* <NewProductWidget /> */}
          <MostBoughtWidget />
          <OnSaleWidget />
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
