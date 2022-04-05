import React from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Autoplay, Pagination } from "swiper";
import "swiper/css";
import "swiper/css/navigation";
import banner7 from "../../assets/banner7.png";

function Carousel(props) {
  return (
    <div className="w-[100%] hero ">
      <Swiper
        navigation={true}
        autoplay={{
          delay: 5000,
          disableOnInteraction: true,
        }}
        grabCursor={true}
        pagination={{
          clickable: true,
        }}
        style={{
          "--swiper-navigation-color": "#fff",
          "--swiper-pagination-color": "#fff",
        }}
        speed={2000}
        loop={true}
        modules={[Navigation, Autoplay, Pagination]}
        className="mySwiper h-screen"
      >
        <SwiperSlide className="before">
          <img
            src={banner7}
            alt=""
            className="w-full h-full object-cover object-right md:object-center"
          />
        </SwiperSlide>
        <SwiperSlide className="before">
          <img
            src={banner7}
            alt=""
            className="w-full h-full object-cover object-right md:object-center"
          />
        </SwiperSlide>
      </Swiper>
    </div>
  );
}

export default Carousel;
