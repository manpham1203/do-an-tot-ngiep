import React, { useEffect, useState } from "react";
import Carousel from "../../../components/Carousel/Carousel";
import Heading from "../../../components/Heading/Heading";
import ListProductCard from "../../../components/ProductSlideShow/ListProductCard";
import ListTab from "../../../components/ProductSlideShow/ListTab";
import ProductSlideShow from "../../../components/ProductSlideShow/ProductSlideShow";
import api from "../../../apis/api";
import PostSlideShow from "../../../components/PostSlideShow/PostSlideShow";
import ProductRow from "../../../components/Skeleton/ProductRow";
import PostRow from "../../../components/Skeleton/PostRow";

function Home() {
  document.title = "WatchStore | Nhà tôi 3 đời bán đồng hồ";
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
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return (
    <div className="w-full">
      <Carousel></Carousel>
      {brand.length > 0 ? (
        <ProductSlideShow>
          <Heading
            title="Thương Hiệu"
            className="text-center text-[22px] sm:text-[25px] md:text-[28px]"
          />
          <div className="w-full">
            <div className="mx-auto w-fit sm:w-[640px] md:w-[768px] lg:w-[1024px] xl:w-[1280px] 2xl:w-[1436px] flex flex-wrap flex-row justify-center gap-x-[20px] font-primary text-[20px]">
              {brand.length > 0 &&
                brand.map((item) => {
                  return (
                    <ListTab
                      className="text-[18px] sm:text-[20px] md:text-[22px] "
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
      ) : (
        <ProductRow />
      )}
      {category.length > 0 ? (
        <ProductSlideShow>
          <Heading
            title="Danh Mục"
            className="text-center text-[22px] sm:text-[25px] md:text-[28px]"
          />
          <div className="w-full">
            <div className="mx-auto w-fit sm:w-[640px] md:w-[768px] lg:w-[1024px] xl:w-[1280px] 2xl:w-[1436px] flex flex-wrap flex-row justify-center gap-x-[20px] font-primary text-[20px]">
              {category.length > 0 &&
                category.map((item) => {
                  return (
                    <ListTab
                      className="text-[18px] sm:text-[20px] md:text-[22px]"
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
      ) : (
        <ProductRow />
      )}

      {onSale.length > 0 ? (
        <ProductSlideShow>
          <Heading
            title="Đang Giảm Giá"
            className="text-center text-[22px] sm:text-[25px] md:text-[28px]"
          />
          {onSale.length > 0 && <ListProductCard products={onSale} />}
        </ProductSlideShow>
      ) : (
        <ProductRow />
      )}

      {mostBought.length > 0 ? (
        <ProductSlideShow>
          <Heading
            title="Mua Nhiều Nhất"
            className="text-center text-[22px] sm:text-[25px] md:text-[28px]"
          />
          {mostBought.length > 0 && <ListProductCard products={mostBought} />}
        </ProductSlideShow>
      ) : (
        <ProductRow />
      )}
      {/* {newProduct.length > 0 && (
        <ProductSlideShow>
          <Heading
            title="Sản Phẩm Mới"
            className="text-center text-[22px] sm:text-[25px] md:text-[28px]"
          />
          {newProduct.length > 0 && <ListProductCard products={newProduct} />}
        </ProductSlideShow>
      )} */}

      <PostSlideShow
        slideLg={4}
        slideMd={3}
        slideSm={2}
        slide={1}
      ></PostSlideShow>
    </div>
  );
}

export default Home;
