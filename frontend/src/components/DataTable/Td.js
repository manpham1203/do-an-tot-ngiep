import React from 'react';

function Td(props) {
    return (
        <td className={`px-4 py-2 text-gray-700 ${props.className}`}>
            {props.children}
        </td>
    );
}

export default Td;