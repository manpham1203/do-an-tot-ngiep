import React from 'react';

function Tr(props) {
    return (
        <tr className={`${props?.className}`} style={props?.style}>
            {props.children}
        </tr>
    );
}

export default Tr;