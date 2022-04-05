import React, { useRef } from "react";
import { useController, useWatch } from "react-hook-form";

function Date({ control, ...props }) {
  const { field } = useController({
    control,
    name: props.name,
    defaultValue: props.value,
  });
  const ref = useRef();
  return (
    <div className="relative">
      <input
        {...field}
        {...props}
        type="date"
        className={`w-full h-[50px] border border-input-border outline-none rounded-[4px] px-[20px]`}
      />
      <label htmlFor="" className="absolute  left-[12px] top-[-12px] bg-white px-[10px] text-input-label">{props.label}</label>
    </div>
  );
}

export default Date;
