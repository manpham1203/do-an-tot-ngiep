import React from 'react';

function Heading(props) {
    return (
        <h1 className="font-primary font-bold text-[30px] text-center">
        {props.title}
      </h1>
    );
}

export default Heading;