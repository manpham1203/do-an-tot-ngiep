import React from 'react';

function Table(props) {
    return (
        <table className={`${props?.className}`} style={props?.style}>
            {props.children}
        </table>
    );
}

export default Table;