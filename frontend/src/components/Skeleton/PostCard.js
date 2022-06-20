import React from "react";
import imgthumb from "../../assets/imgthumb.jpg";

function PostCard(props) {
  return (
    <div className="w-full flex flex-col gap-y-[10px] bg-white rounded-xl shadow-md overflow-hidden  p-[10px] animate-pulse">
      <div className="w-full block h-[186px]">
        <img
          src={imgthumb}
          alt=""
          className="w-full h-full object-cover object-center rounded-xl"
        />
      </div>
      <div className="flex flex-row font-light gap-x-[10px] text-[15px] h-[15px] w-full bg-gray-200"></div>
      <div className="short-desc-postcard2 font-medium text-[18px] h-[18px] w-full bg-gray-200"></div>
    </div>
  );
}

export default PostCard;
