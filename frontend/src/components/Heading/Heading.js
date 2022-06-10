import React from "react";

function Heading(props) {
  return (
    <h1 className={`font-primary font-medium text-second dark:text-third ${props.className} `}>
      {props.title}
    </h1>
  );
}

export default Heading;
