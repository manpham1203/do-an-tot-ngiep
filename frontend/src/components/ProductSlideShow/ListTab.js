import React from "react";

function ListTab(props) {
  return (
    <button
      onClick={() => props.setShow(props.id)}
      className={`${props.show === props.id ? "underline font-normal decoration-1 underline-offset-4 " : "font-light"} text-second dark:text-third ${props.className}`}
    >
      {props.name}
    </button>
  );
}

export default ListTab;
