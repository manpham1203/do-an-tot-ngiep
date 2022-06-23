import React from 'react';

function Td(props) {
    return (
        <td className={` border border-slate-300 h-[40px] ${props?.className}`} style={props?.style}>
            {props.children}
        </td>
    );
}

export default Td;