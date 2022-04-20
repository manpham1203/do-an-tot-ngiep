import React, { useEffect, useReducer, useState } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Thumbs } from "swiper";
import api from "../../apis/api";
import { useParams } from "react-router-dom";

function ProductImageSlider(props) {
  const [active, setActive] = useState(null);

  return (
    <>
      <div className="h-[100px] w-auto hidden lg:block">
        <Swiper
          loop={false}
          spaceBetween={10}
          slidesPerView={5}
          navigation={true}
          grabCursor={true}
          modules={[Navigation, Thumbs]}
          className="productSlideThumb h-[500px] w-[92px]"
          onSwiper={setActive}
          direction={"vertical"}
          style={{
            "--swiper-navigation-color": "#bg-color-second",
            "--swiper-pagination-color": "#bg-color-second",
          }}
        >
          {props.images?.map((item, index) => {
            return (
              <SwiperSlide
                key={index}
                className={` overflow-hidden`}
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
      <div className="w-[500px] h-[500px]">
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
      <div className="block lg:hidden mt-[20px]">
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
              <SwiperSlide
                key={index}
                className={` overflow-hidden`}
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
    </>
  );
}

export default ProductImageSlider;
