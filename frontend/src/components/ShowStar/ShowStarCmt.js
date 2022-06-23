import React from "react";
import { AiFillStar } from "react-icons/ai";

function ShowStarCmt(props) {
  return (
    <>
      {props.star === 1 ? (
        <div className="flex flex-row items-center text-gray-400 justify-center">
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar />
          <AiFillStar />
          <AiFillStar />
          <AiFillStar />
        </div>
      ) : props.star === 2 ? (
        <div className="flex flex-row items-center text-gray-400 justify-center">
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar />
          <AiFillStar />
          <AiFillStar />
        </div>
      ) : props.star === 3 ? (
        <div className="flex flex-row items-center text-gray-400 justify-center">
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar />
          <AiFillStar />
        </div>
      ) : props.star === 4 ? (
        <div className="flex flex-row items-center text-gray-400 justify-center">
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar />
        </div>
      ) : (
        <div className="flex flex-row items-center text-gray-400 justify-center">
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
          <AiFillStar className="text-[#F7BF63]" />
        </div>
      )}
    </>
  );
}

export default ShowStarCmt;
