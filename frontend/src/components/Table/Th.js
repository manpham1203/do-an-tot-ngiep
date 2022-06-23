import React from 'react';

function Th(props) {
    return (
        <th className={`${props?.className} border border-slate-300  h-[40px]`} style={props?.style}>
            {props.children}
        </th>
    );
}

export default Th;