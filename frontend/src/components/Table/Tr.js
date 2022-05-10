import React from 'react';

function Tr(props) {
    return (
        <tr className={`${props.className}`}>
            {props.children}
        </tr>
    );
}

export default Tr;