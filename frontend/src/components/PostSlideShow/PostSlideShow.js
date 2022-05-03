import React, { useEffect, useState } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation } from "swiper";
import "swiper/css/navigation";
import "swiper/css";
import PostCard2 from "../Post/PostCard2";
import api from "../../apis/api";
import Heading from "../../components/Heading/Heading";
import * as moment from "moment";
import "moment/locale/vi";

function PostSlideShow(props) {
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/Post/postcards`,
      params: { id: id },
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
    <div className="container mx-auto pt-[80px]">
      <Heading title="Tin tức" textCenter={props.textCenter} />
      <Swiper
        slidesPerView={props.slide}
        // spaceBetween={30}
        navigation={true}
        modules={[Navigation]}
        breakpoints={{
          640: {
            slidesPerView: props.slideSm,
            // spaceBetween: 20,
          },
          768: {
            slidesPerView: props.slideMd,
            // spaceBetween: 30,
          },
          1024: {
            slidesPerView: props.slideLg,
            // spaceBetween: 30,
          },
        }}
        className={`mySwipe p-[20px]`}
      >
        {data.map((item) => {
          return (
            <SwiperSlide
              key={item?.slug}
              className="rounded-[8px]  list-product-card"
            >
              <PostCard2
                key={item?.id}
                title={item?.title}
                slug={item?.slug}
                image={item?.imageSrc}
                // day={moment(item?.createdAt).format("DD")}
                createdAt={moment(item?.createdAt).format('Do MMMM YYYY')}
                shortDescription={item?.shortDescription}
                view={item?.view}
              />
            </SwiperSlide>
          );
        })}
      </Swiper>
    </div>
  );
}

export default PostSlideShow;
