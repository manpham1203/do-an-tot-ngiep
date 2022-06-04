import React, { useEffect, useState } from "react";
import Carousel from "../../../components/Carousel/Carousel";
import Heading from "../../../components/Heading/Heading";
import ListProductCard from "../../../components/ProductSlideShow/ListProductCard";
import ListTab from "../../../components/ProductSlideShow/ListTab";
import ProductSlideShow from "../../../components/ProductSlideShow/ProductSlideShow";
import api from "../../../apis/api";
import PostSlideShow from "../../../components/PostSlideShow/PostSlideShow";

function Home() {
  const [brand, setBrand] = useState([]);
  const [category, setCategory] = useState([]);
  const [mostBought, setMostBought] = useState([]);
  const [newProduct, setNewProduct] = useState([]);
  const [onSale, setOnSale] = useState([]);
  const fetchProductsOfBrand = async () => {
    await api({
      method: "GET",
      url: `/Brand/AllBrandWithProductCard`,
      data: null,
    })
      .then((res) => {
        setBrand(res.data);
      })
      .catch(() => console.log("fail"));
  };
  const fetchProductsOfCategory = async () => {
    await api({
      method: "GET",
      url: `/Category/AllCategoryWithProductCard`,
      data: null,
    })
      .then((res) => {
        setCategory(res.data);
      })
      .catch(() => console.log("fail"));
  };
  const fetchMostBought = async () => {
    await api({
      method: "GET",
      url: `/product/MostBought`,
      params: { take: 8 },
    })
      .then((res) => {
        setMostBought(res.data);
      })
      .catch(() => console.log("fail"));
  };
  const fetchNewProduct = async () => {
    await api({
      method: "GET",
      url: `/product/newproduct`,
      params: { take: 10 },
    })
      .then((res) => {
        setNewProduct(res.data);
      })
      .catch(() => console.log("fail"));
  };
  const fetchOnSale = async () => {
    await api({
      method: "GET",
      url: `/product/onsale`,
      params: { take: 10 },
    })
      .then((res) => {
        setOnSale(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchProductsOfCategory();
    fetchProductsOfBrand();
    fetchMostBought();
    fetchNewProduct();
    fetchOnSale();
  }, []);
  const [showBrand, setShowBrand] = useState();
  const [showCategory, setShowCategory] = useState();

  useEffect(() => {
    setShowCategory(category[0]?.id);
  }, [category]);
  useEffect(() => {
    setShowBrand(brand[0]?.id);
  }, [brand]);
  document.title = "Trang chủ";
  return (
    <div className="w-[100%]">
      <Carousel></Carousel>
      <ProductSlideShow>
        <Heading title="Thương Hiệu" className="text-center" />
        <div className="w-full">
          <div className="mx-auto w-fit sm:w-[640px] md:w-[768px] lg:w-[1024px] xl:w-[1280px] 2xl:w-[1436px] flex flex-wrap flex-row justify-center gap-x-[20px] font-primary text-[20px]">
            {brand.length > 0 &&
              brand.map((item) => {
                return (
                  <ListTab
                    key={item?.id}
                    name={item?.name}
                    id={item?.id}
                    show={showBrand}
                    setShow={setShowBrand}
                  />
                );
              })}
          </div>
        </div>

        {brand.length > 0 &&
          brand.map((item) => {
            return (
              <ListProductCard
                key={item?.id}
                id={item?.id}
                products={item.productCardVMs}
                show={showBrand}
              />
            );
          })}
      </ProductSlideShow>
      <ProductSlideShow>
        <Heading title="Danh Mục" className="text-center" />
        <div className="w-full">
          <div className="mx-auto w-fit sm:w-[640px] md:w-[768px] lg:w-[1024px] xl:w-[1280px] 2xl:w-[1436px] flex flex-wrap flex-row justify-center gap-x-[20px] font-primary text-[20px]">
            {category.length > 0 &&
              category.map((item) => {
                return (
                  <ListTab
                    key={item?.id}
                    name={item?.name}
                    id={item?.id}
                    show={showCategory}
                    setShow={setShowCategory}
                  />
                );
              })}
          </div>
        </div>
        {category.length > 0 &&
          category.map((item) => {
            return (
              <ListProductCard
                key={item?.id}
                id={item?.id}
                products={item?.productCardVMs}
                show={showCategory}
              />
            );
          })}
      </ProductSlideShow>
      <ProductSlideShow>
        <Heading title="Đang Giảm Giá" className="text-center" />
        {onSale.length > 0 && <ListProductCard products={onSale} />}
      </ProductSlideShow>
      <ProductSlideShow>
        <Heading title="Mua Nhiều Nhất" className="text-center" />
        {mostBought.length > 0 && <ListProductCard products={mostBought} />}
      </ProductSlideShow>
      <ProductSlideShow>
        <Heading title="Sản Phẩm Mới" className="text-center" />
        {newProduct.length > 0 && <ListProductCard products={newProduct} />}
      </ProductSlideShow>
      <PostSlideShow slideLg={4} slideMd={3} slideSm={2} slide={1}>
        <Heading title="Tin tức" className="text-center" />
      </PostSlideShow>
    </div>
  );
}

export default Home;
