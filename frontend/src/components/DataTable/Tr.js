import React from 'react';

function Tr(props) {
    return (
        <tr className={`bg-white ${props.className}`}>
            {props.children}
        </tr>
    );
}

export default Tr;