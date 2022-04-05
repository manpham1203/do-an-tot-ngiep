import React from "react";
import { useController, useWatch } from "react-hook-form";

function AdminInput({ control, ...props }) {
  const { field } = useController({
    control,
    name: props.name,
    defaultValue: "",
  });
  return (
    <div htmlFor="mb-6">
      <label
        htmlFor={props.name}
        className="block mb-2 text-sm font-medium text-gray-900 "
      >
       {props.label}
      </label>
      <input
         {...field}
        {...props}
        id={props.name}
        className="bg-gray-50 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
      />
    </div>
  );
}

export default AdminInput;
