import React from 'react';

function Thead(props) {
    return (
        <thead className={`${props?.className}`} style={props?.style}>
            {props.children}
        </thead>
    );
}

export default Thead;