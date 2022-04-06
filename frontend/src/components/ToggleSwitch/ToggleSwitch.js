import React from "react";

function ToggleSwitch(props) {
  return (
    <div
      className={`w-[50px] h-[25px]  flex items-center rounded-full relative
                  ${props.switch ? "bg-blue-600 " : "bg-gray-300"}
                  transition-all duration-200 cursor-pointer
                  `}
    >
      <div
        className={`w-[18px] h-[18px] bg-white rounded-full  absolute
                    ${props.switch ? "ml-[4px]" : "ml-[28px]"}
                    transition-all duration-200
                    `}
      ></div>
    </div>
  );
}

export default ToggleSwitch;
