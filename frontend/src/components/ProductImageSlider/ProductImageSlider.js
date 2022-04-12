import React, { useEffect, useReducer, useState } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Thumbs } from "swiper";
import api from "../../apis/api";
import { useParams } from "react-router-dom";

function ProductImageSlider(props) {
  const [active, setActive] = useState(null);
  
  return (
    <>
    
      <Swiper
        loop={false}
        spaceBetween={10}
        slidesPerView={5}
        navigation={true}
        grabCursor={true}
        modules={[Navigation, Thumbs]}
        className="productSlideThumb h-[500px] w-[107.7px] mr-[10px]"
        onSwiper={setActive}
        direction={"vertical"}
      >
        {props.images?.map((item, index) => {
          return (
            <SwiperSlide key={index} className={`flex  items-center justify-center border border-gray-400`}>
              <img src={item.imageSrc} alt="product images" />
            </SwiperSlide>
          );
        })}
      </Swiper>
        <Swiper
        loop={false}
        spaceBetween={10}
        navigation={true}
        modules={[Navigation, Thumbs]}
        grabCursor={true}
        thumbs={{ swiper: active }}
        className="w-full border border-gray-400"
      >
        {props.images?.map((item, index) => {
          return (
            <SwiperSlide key={index}>
              <img src={item.imageSrc} alt="product images" />
            </SwiperSlide>
          );
        })}
      </Swiper>
    </>
  );
}

export default ProductImageSlider;
