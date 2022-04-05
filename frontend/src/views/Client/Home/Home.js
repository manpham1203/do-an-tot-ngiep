import React, {  } from "react";
import Carousel from "../../../components/Carousel/Carousel";
import ListBrand from "../../../components/ListBrand/ListBrand";
import ProductBrandSlideShow from "../../../components/ProductBrandSlideShow/ProductBrandSlideShow";
import ProductCategorySlideShow from "../../../components/ProductCategorySlideShow/ProductCategorySlideShow";
import WeTalkPride from "../../../components/WeTalkPride/WeTalkPride";

function Home() {
  document.title="Web";

  return (
    <div className="w-[100%]">
      <Carousel></Carousel>
      <ListBrand />
      <WeTalkPride />
      <ProductBrandSlideShow />
      <ProductCategorySlideShow />
      
    </div>
  );
}

export default Home;
