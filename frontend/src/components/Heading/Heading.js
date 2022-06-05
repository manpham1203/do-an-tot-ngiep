import React from "react";

function Heading(props) {
  return (
    <h1 className={`font-primary font-medium ${props.className} `}>
      {props.title}
    </h1>
  );
}

export default Heading;
