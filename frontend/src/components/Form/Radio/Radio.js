import React from "react";
import { useController, useWatch } from "react-hook-form";

function Radio({ control, ...props }) {
  const { field } = useController({
    control,
    name: props.name,
    defaultValue: props.value,
  });
  return (
    <div>
      <label
        className="form-radio inline-flex items-center cursor-pointer mr-[10px]  "
      >
        <input
          {...field}
          {...props}
          type="radio"
          className="radio-input w-[20px] h-[20px] hidden"
        />
        <div
          className="mr-[10px] radio-circle w-[20px] h-[20px] border-[0.5px] border-second rounded-full p-[2px] flex justify-center items-center
        "
        >
          {/* after:content-[' '] after:w-full after:h-full after:rounded-full after:block after:bg-blue-500 after:scale-0 after:transition-transform after:duration-[1s] */}
          {/* <div className="w-[10px] h-[10px] rounded-full bg-blue-500"></div> */}
        </div>
        {props.label}
      </label>
    </div>
  );
}

export default Radio;
