import React from "react";
import imgthumb from "../../assets/imgthumb.jpg";

function ProductCard(props) {
  return (
    <div className="w-full h-fit relative group  rounded-[8px] overflow-hidden shadow-md animate-pulse">
      <div className="w-full overflow-hidden relative ">
        <img
          src={imgthumb}
          alt="alt"
          className="w-full object-center object-cover"
        />
      </div>
      <div className="w-full flex flex-col items-center p-[10px] gap-y-[10px] bg-white bottom-[-44px]">
        <div className=" w-[100px]  h-[18px] skeleton"></div>
        <div className="font-primary font-normal h-[18px] bg-gray-200 underline-offset-4 mb-[5px] truncate w-full text-center"></div>
        <div className="flex flex-row text-[#F7BF63] h-[18px] w-[70%] bg-gray-200"></div>
        <div className="flex flex-round gap-x-[20px] mt-[10px] h-[18px] w-[60%] bg-gray-200"></div>
      </div>
    </div>
  );
}

export default ProductCard;
