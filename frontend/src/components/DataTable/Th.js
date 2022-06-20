import React from 'react';

function Th(props) {
    return (
        <th className={`sticky left-0 px-4 py-2 text-left bg-white text-gray-900 ${props.className}`}>
            {props.children}
        </th>
    );
}

export default Th;