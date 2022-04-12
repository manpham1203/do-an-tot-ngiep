import React, { useEffect, useState } from "react";
import Heading from "./Heading";
import ListProductCard from "./ListProductCard";
import ListTab from "./ListTab";

function ProductSlideShow(props) {
  
  return (
    <div className="ProductSlideShow container mx-auto pt-[80px]">
      {props.children}
    </div>
  );
}

export default ProductSlideShow;
