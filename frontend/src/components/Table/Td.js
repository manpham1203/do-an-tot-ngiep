import React from 'react';

function Td(props) {
    return (
        <td className={`${props.className} border border-slate-300 h-[40px]`}>
            {props.children}
        </td>
    );
}

export default Td;