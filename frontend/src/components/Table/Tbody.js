import React from 'react';

function Tbody(props) {
    return (
        <tbody className={`${props.className}`}>
            {props.children}
        </tbody>
    );
}

export default Tbody;