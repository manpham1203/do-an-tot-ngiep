import React from 'react';

function Tbody(props) {
    return (
        <tbody className={`${props?.className}`} style={props?.style}>
            {props.children}
        </tbody>
    );
}

export default Tbody;