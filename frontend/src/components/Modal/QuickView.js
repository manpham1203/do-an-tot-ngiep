import React, { useEffect, useState } from "react";
import { FaTimes } from "react-icons/fa";
import { useDispatch, useSelector } from "react-redux";
import { setClose } from "../../redux/quickView/quickViewActions";
import api from "../../apis/api";
import ProductImageSlider from "../ProductImageSlider/ProductImageSlider";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Thumbs } from "swiper";
import { Link } from "react-router-dom";
import ShowStarAvg from "../ShowStar/ShowStarAvg";
import { toast } from "react-toastify";
import { addToCart, adjustQty } from "../../redux/cart/cartActions";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import Heart from "../Wishlist/Heart";

function QuickView(props) {
  const dispatch = useDispatch();
  const { quickView } = useSelector((store) => store);
  const [number, setNumber] = useState(1);
  const [scroll, setScroll] = useState();
  const [data, setData] = useState();
  const [active, setActive] = useState(null);
  const { cart } = useSelector((store) => store);

  const fetchData = async (slug) => {
    await api({
      method: "GET",
      url: `/Product/productdetail/${slug}`,
      data: null,
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(quickView.id);
  }, [quickView.id]);
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
              `sản phẩm "${data.name}" thêm vào giỏ hàng thành công`,
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
        toast.success(`sản phẩm "${data.name}" thêm vào giỏ hàng thành công`, {
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

  const handleNumber = (e) => {
    const re = /^[1-9\b]+$/;
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
  return (
    <div className="bg-third fixed w-[80%] h-[500px] rounded-xl top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] z-[2000]">
      <div className="text-right w-fit absolute right-0 top-0">
        <FaTimes
          onClick={() => dispatch(setClose())}
          className="inline-block text-[30px] m-[10px] text-second cursor-pointer"
        />
      </div>
      <div className="grid grid-cols-2 p-[25px] gap-x-[25px]">
        <Swiper
          loop={false}
          spaceBetween={10}
          navigation={true}
          modules={[Navigation]}
          grabCursor={true}
          style={{
            "--swiper-navigation-color": "#bg-color-second",
            "--swiper-pagination-color": "#bg-color-second",
          }}
          className="shadow-admin rounded-xl h-[450px] w-full"
        >
          {data?.pictureVMs?.map((item, index) => {
            return (
              <SwiperSlide autoHeight={true} key={index}>
                <img
                  src={item.imageSrc}
                  alt="product images"
                  className=" object-cover object-center w-full h-full"
                />
              </SwiperSlide>
            );
          })}
        </Swiper>
        <div className="w-full flex flex-col p-[20px] h-[450px] overflow-hidden">
          <h2 className="text-[16px]">{data?.brandNameVM?.name}</h2>
          <h2 className="text-[25px] productCard2Name">{data?.name}</h2>
          <div className="flex flex-row items-center text-[#F7BF63] gap-x-[5px]">
            <ShowStarAvg star={data?.star} />

            <span className="ml-[10px] text-[#3f3d4f]">
              ({data?.starCount} lượt đánh giá)
            </span>
          </div>
          {data?.priceDiscount === null ? (
            <span className="text-[25px] mt-[10px]">
              {new Intl.NumberFormat("vi-VN", {
                style: "currency",
                currency: "VND",
              }).format(data?.price)}
            </span>
          ) : (
            <div className="flex flex-row gap-x-[20px]">
              <span className="text-[25px] mt-[10px] ">
                {new Intl.NumberFormat("vi-VN", {
                  style: "currency",
                  currency: "VND",
                }).format(data?.priceDiscount)}
              </span>
              <span className="text-[25px] mt-[10px] line-through opacity-[0.5]">
                {new Intl.NumberFormat("vi-VN", {
                  style: "currency",
                  currency: "VND",
                }).format(data?.price)}
              </span>
            </div>
          )}
          <div className="mt-[10px]">
            <span className="font-medium">Lượt thích: </span>
            {data?.like}
          </div>
          <div className="mt-[10px]">
            <span className="font-medium">Lượt xem: </span>
            {data?.view}
          </div>

          <div className="mt-[10px]">
            <div className="">
              <span className="font-medium">Loại sản phẩm: </span>
              {data?.categoryNameVMs?.map((item, index) => {
                return (index =
                  1 === data?.categoryNameVMs.length ? (
                    <span
                      key={item.id}
                      className="cursor-pointer hover:underline underline-offset-4"
                      // onClick={() =>
                      //   navigate(`/san-pham?&category=${item.slug}`)
                      // }
                    >
                      {item.name}
                    </span>
                  ) : (
                    <span
                      key={item.id}
                      className="cursor-pointer hover:underline underline-offset-4"
                      // onClick={() =>
                      //   navigate(`/san-pham?&category=${item.slug}`)
                      // }
                    >
                      {item.name},&nbsp;
                    </span>
                  ));
              })}
            </div>
          </div>

          <div className="flex flex-row items-center mt-[20px] border border-gray-400 w-fit">
            <div
              className={` px-[10px] border-r border-gray-400 h-[40px] flex items-center w-fit ${
                number === 1 ? "text-gray-200 " : "text-second cursor-pointer"
              }`}
              onClick={() =>
                setNumber((number) => {
                  return number <= 1 ? 1 : number - 1;
                })
              }
            >
              <BsDashLg />
            </div>
            <input
              className="number_cart-item w-[100px] text-center h-[40px] "
              type="number"
              value={number}
              onChange={(e) => handleNumber(e)}
              onBlur={(e) => handleBlurNumber(e)}
              min="1"
            />
            <div
              className={`px-[10px] border-l border-gray-400 h-[40px] flex items-center w-fit ${
                number === 9 ? "text-gray-200 " : "text-second cursor-pointer"
              }`}
              onClick={() =>
                setNumber((number) => (number >= 9 ? 9 : number + 1))
              }
            >
              <BsPlusLg />
            </div>
          </div>
          <button
            className="mt-[20px] p-[10px] bg-second text-third font-medium mb-[20px]"
            onClick={() => addCart(data?.id, number)}
          >
            THÊM VÀO GIỎ HÀNG
          </button>

          <Heart
            id={data?.id}
            titleDislike="Xoá khỏi danh sách yêu thích"
            titleLike="Thêm vào danh sách yêu thích"
            dislikeStyles="text-red-600 cursor-pointer flex flex-row hover:underline underline-offset-4 items-center gap-x-[10px]"
            likeStyles="text-gray-400 cursor-pointer flex flex-row hover:underline underline-offset-4 items-center gap-x-[10px]"
          />
        </div>
      </div>
    </div>
  );
}

export default QuickView;
