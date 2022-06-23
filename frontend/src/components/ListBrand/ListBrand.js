import React from "react";
import brands from "../../assets/brands.png";

function ListBrand(props) {
  return (
    <div className="w-[100%] bg-white">
      <div className="container mx-auto pt-[40px]">
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 ">
          <div className="w-full flex justify-center">
            <div className="flex items-center justify-center w-[150px] h-[150px] before:w-[100px] before:h-[100px] before:block before:absolute before:border before:border-gray-600 before:rotate-45">
              <img src={brands} alt="" className="w-[80px]" />
            </div>
          </div>
          <div className="w-full flex justify-center">
            <div className="flex items-center justify-center w-[150px] h-[150px] before:w-[100px] before:h-[100px] before:block before:absolute before:border before:border-gray-600 before:rotate-45">
              <img src={brands} alt="" className="w-[80px]" />
            </div>
          </div>
          <div className="w-full flex justify-center">
            <div className="flex items-center justify-center w-[150px] h-[150px] before:w-[100px] before:h-[100px] before:block before:absolute before:border before:border-gray-600 before:rotate-45">
              <img src={brands} alt="" className="w-[80px]" />
            </div>
          </div>
          <div className="w-full flex justify-center">
            <div className="flex items-center justify-center w-[150px] h-[150px] before:w-[100px] before:h-[100px] before:block before:absolute before:border before:border-gray-600 before:rotate-45">
              <img src={brands} alt="" className="w-[80px]" />
            </div>
          </div>
          <div className="w-full flex justify-center">
            <div className="flex items-center justify-center w-[150px] h-[150px] before:w-[100px] before:h-[100px] before:block before:absolute before:border before:border-gray-600 before:rotate-45">
              <img src={brands} alt="" className="w-[80px]" />
            </div>
          </div>
          <div className="w-full flex justify-center">
            <div className="flex items-center justify-center w-[150px] h-[150px] before:w-[100px] before:h-[100px] before:block before:absolute before:border before:border-gray-600 before:rotate-45">
              <img src={brands} alt="" className="w-[80px]" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ListBrand;
