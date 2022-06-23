import React from "react";

function FooterSkeleton(props) {
  return (
    <div className="w-full">
      <div className="font-medium h-[18px] w-full bg-gray-200 mb-[20px]"></div>
      <ul className="font-light ">
        {[...Array(5)].map((item, index) => {
          return (
            <li key={index} className="mb-[10px]">
              <div className=" h-[16px] bg-gray-200 w-full"></div>
            </li>
          );
        })}
      </ul>
    </div>
  );
}

export default FooterSkeleton;
