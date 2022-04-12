import React from "react";

function ListTab(props) {
  return (
    <button
      onClick={() => props.setShow(props.id)}
      className={`${props.show === props.id ? "underline" : ""}`}
    >
      {props.name}
    </button>
  );
}

export default ListTab;
