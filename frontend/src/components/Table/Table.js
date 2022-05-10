import React from 'react';

function Table(props) {
    return (
        <table className={`${props.className}`}>
            {props.children}
        </table>
    );
}

export default Table;