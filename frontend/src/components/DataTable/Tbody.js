import React from 'react';

function Tbody(props) {
    return (
        <tbody className={`divide-y divide-gray-600 ${props.className}`}>
            {props.children}
        </tbody>
    );
}

export default Tbody;