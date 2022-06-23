import React from "react";
import ProductCard from "./ProductCard";

function ProductRow(props) {
  return (
    <div className="container px-[10px] sm:px-[20px] mx-auto flex flex-row gap-x-[25px] my-[20px]">
      <div className=" w-full">
        <ProductCard />
      </div>
      <div className="hidden sm:block w-full">
        <ProductCard />
      </div>
      <div className="hidden md:block w-full">
        <ProductCard />
      </div>
      <div className="hidden lg:block w-full">
        <ProductCard />
      </div>
    </div>
  );
}

export default ProductRow;
