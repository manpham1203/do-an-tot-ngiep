import React from "react";

function ProductList(props) {
 
  return (
    <div className="grid grid-cols-4 gap-x-[20px] max-w-[1200px] mx-auto my-[40px]">
      {props.children}
    </div>
  );
}

export default ProductList;
