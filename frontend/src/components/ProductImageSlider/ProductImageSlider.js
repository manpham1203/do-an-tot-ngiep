import React, { useEffect, useReducer, useState } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Thumbs } from "swiper";
import api from "../../apis/api";
import { useParams } from "react-router-dom";

function ProductImageSlider(props) {
  const [active, setActive] = useState(null);
  const [isMobile, setIsMobile] = useState(false);
  const [windowSize, setWindowSize] = useState({
    width: undefined,
    height: undefined,
  });
  useEffect(() => {
    const handleSize = () => {
      setWindowSize({
        width: window.innerWidth,
        heigth: window.innerHeight,
      });
    };
    window.addEventListener("resize", handleSize);
    handleSize();
    return () => window.removeEventListener("resize", handleSize);
  }, []);
  useEffect(() => {
    if (windowSize.width < 1024) {
      setIsMobile(true);
    } else {
      setIsMobile(false);
    }
  }, [windowSize]);
  console.log(isMobile);
  return (
    <>
      <div className="lg:h-[500px] lg:w-[92px] w-full mt-[20px] lg:mt-0 ">
        <Swiper
          loop={false}
          spaceBetween={10}
          slidesPerView={5}
          navigation={true}
          grabCursor={true}
          modules={[Navigation, Thumbs]}
          className={`${
            !isMobile && "productSlideThumb"
          } w-full h-[92px] lg:h-[500px] lg:w-[92px]`}
          onSwiper={setActive}
          direction={isMobile ? "horizontal" : "vertical"}
          style={{
            "--swiper-navigation-color": "#bg-color-second",
            "--swiper-pagination-color": "#bg-color-second",
          }}
        >
          {props.images?.map((item, index) => {
            return (
              <SwiperSlide
                key={index}
                className={` overflow-hidden cursor-pointer`}
              >
                <img
                  src={item.imageSrc}
                  alt="product images"
                  className="w-full h-full object-cover object-center"
                />
              </SwiperSlide>
            );
          })}
        </Swiper>
      </div>

      <div className="w-full lg:h-[500px] lg:w-[500px] h-fit">
        <Swiper
          loop={false}
          spaceBetween={10}
          navigation={true}
          modules={[Navigation, Thumbs]}
          grabCursor={true}
          thumbs={{ swiper: active }}
          style={{
            "--swiper-navigation-color": "#bg-color-second",
            "--swiper-pagination-color": "#bg-color-second",
          }}
          className=" border border-gray-400"
        >
          {props.images?.map((item, index) => {
            return (
              <SwiperSlide key={index}>
                <img
                  src={item.imageSrc}
                  alt="product images"
                  className="w-full h-full object-cover object-center"
                />
              </SwiperSlide>
            );
          })}
        </Swiper>
      </div>
      {/* <div className="mt-[20px]">
        <Swiper
          loop={false}
          spaceBetween={10}
          slidesPerView={5}
          navigation={true}
          grabCursor={true}
          modules={[Navigation, Thumbs]}
          className="h-[92px] w-[500px]"
          onSwiper={setActive}
          direction={"horizontal"}
          style={{
            "--swiper-navigation-color": "#bg-color-second",
            "--swiper-pagination-color": "#bg-color-second",
          }}
        >
          {props.images?.map((item, index) => {
            return (
              <SwiperSlide key={index} className={` overflow-hidden`}>
                <img
                  src={item.imageSrc}
                  alt="product images"
                  className="w-full h-full object-cover object-center"
                />
              </SwiperSlide>
            );
          })}
        </Swiper>
      </div> */}
    </>
  );
}

export default ProductImageSlider;
