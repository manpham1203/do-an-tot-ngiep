import React from "react";
import { useController, useWatch } from "react-hook-form";

function Input({ control, ...props }) {
  const { field } = useController({
    control,
    name: props.name,
    defaultValue: "",
  });
  return (
    <div className="w-full relative">
      <input
        {...field}
        {...props}
        placeholder=" "
        
        className={`form-input border border-input-border text-input-color font-normal rounded-[4px] w-[100%] h-[50px] px-[20px] transition-all duration-[0.25s] focus:border-second outline-none bg-third ${props.className}`}
      />
      <label
        htmlFor={props.id}
        className="form-label absolute left-[20px] top-[50%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-input-label"
      >
        {props.label}
      </label>
    </div>
  );
}

export default Input;
