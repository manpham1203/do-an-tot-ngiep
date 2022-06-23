import React, { useEffect, useState } from "react";

function ProductSlideShow(props) {
  
  return (
    <div className="ProductSlideShow container mx-auto pt-[80px]">
      {props.children}
    </div>
  );
}

export default ProductSlideShow;
