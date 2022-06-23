import React, { useState, useEffect } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Autoplay, Pagination } from "swiper";
import "swiper/css";
import "swiper/css/navigation";
// import bgbanner from "../../assets/bgbanner.jpg";
import api from "../../apis/api";
import { Link } from "react-router-dom";

function Carousel(props) {
  const [data, setData] = useState([]);
  // const navigationPrevRef = React.useRef(null);
  // const navigationNextRef = React.useRef(null);

  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/banner/GetList`,
      params: { deleted: false, published: true },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, []);
  return (
    <div className="w-[100%] hero ">
      <Swiper
        navigation={true}
        // navigation={{
        //   prevEl: navigationPrevRef.current,
        //   nextEl: navigationNextRef.current,
        // }}
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
        loop={false}
        modules={[Navigation, Autoplay, Pagination]}
        className="mySwiper h-screen bg-[url('assets/bgbanner.jpg')] bg-center bg-cover"
      >
        {data.length > 0 &&
          data.map((item) => {
            return (
              <SwiperSlide key={item.id} className={``}>
                <div className="flex flex-col md:flex-row w-full items-center justify-center gap-y-[20px] md:justify-evenly h-screen">
                  <img
                    src={item.imageSrc}
                    alt=""
                    className="w-[200px] sm:w-[200px] md:w-[40%] lg:w-[400px] lg:h-[400px] object-cover object-center"
                  />
                  <div className="text-third max-w-[400px] flex flex-col gap-y-[20px]">
                    <h2 className="text-[20px] lg:text-[45px] haha font-semibold">
                      {item.content}
                    </h2>
                    <p className="font-light haha text-[16px] lg:text-[18px]">
                      {item.subContent}
                    </p>
                    {item.linkTo !== null && (
                      <Link
                        to={item.linkTo}
                        className="border-2 border-third text-third transition-all duration-200 px-[30px] py-[10px] w-fit text-[20px] hover:bg-third hover:text-second"
                      >
                        ĐI ĐẾN
                      </Link>
                    )}
                  </div>
                </div>
              </SwiperSlide>
            );
          })}
        {/* <div ref={navigationPrevRef}>haha</div>
        <div ref={navigationNextRef}>hihi</div> */}
      </Swiper>
    </div>
  );
}

export default Carousel;
