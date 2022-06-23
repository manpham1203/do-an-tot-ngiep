import React from "react";
import { useController } from "react-hook-form";

function Checkbox({ control, text, ...props }) {
  const { field } = useController({
    control,
    name: props.name,
    defaultValue: false,
  });
  return (
    <div>
      <label
        htmlFor={props.name}
        className="inline-flex items-center cursor-pointer"
      >
        <input
          // checked
          id={props.name}
          type="checkbox"
          {...field}
          {...props}
          checked={field.value}
          className="form-checkbox hidden"
        />
        <div className="checkbox-box bg-white box-content w-[18px] h-[18px] p-[1px] border border-main-color flex items-center justify-center mr-[10px] rounded-[3px]"></div>
        {props.label}
      </label>
    </div>
  );
}

export default Checkbox;
