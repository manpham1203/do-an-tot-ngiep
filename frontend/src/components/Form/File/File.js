import React from "react";
import { useController, useWatch } from "react-hook-form";

function File({ control, ...props }) {
  const { field } = useController({
    control,
    name: props.name,
    defaultValue: [],
  });
  return (
    <div>
      <input {...field} {...props} type="file" accept="image/*" multiple />
    </div>
  );
}

export default File;
