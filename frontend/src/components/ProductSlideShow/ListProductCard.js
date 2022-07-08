import React from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation } from "swiper";
import "swiper/css/navigation";
import "swiper/css";
import ProductCard from "../Product/ProductCard";

function ListProductCard(props) {
  return (
    <>
      <Swiper
        slidesPerView={1}
        // spaceBetween={30}
        navigation={true}
        modules={[Navigation]}
        breakpoints={{
          640: {
            slidesPerView: 2,
            // spaceBetween: 20,
          },
          768: {
            slidesPerView: 3,
            // spaceBetween: 30,
          },
          1024: {
            slidesPerView: 4,
            // spaceBetween: 30,
          },
          1280: {
            slidesPerView: 4,
            // spaceBetween: 30,
          },
        }}
        className={`mySwipe p-[20px] 
        ${props?.id && props?.show !== props?.id ? "hidden" : null}
        
         `}
      >
        {props.products.map((prod) => {
          return (
            <SwiperSlide key={prod.id} className="rounded-[8px]  list-product-card">
              <ProductCard
                id={prod.id}
                name={prod.name}
                slug={prod.slug}
                brandName={prod.brandNameVM.name}
                price={prod.price}
                priceDiscount={prod.priceDiscount}
                image={prod.imageSrc}
                star={prod.star}
                quantityInStock={prod.quantityInStock}
              />
            </SwiperSlide>
          );
        })}
      </Swiper>
    </>
  );
}

export default ListProductCard;
