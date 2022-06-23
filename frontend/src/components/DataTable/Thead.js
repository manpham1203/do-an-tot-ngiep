import React from 'react';

function Thead(props) {
    return (
        <thead className={`${props.className}`}>
           {props.children} 
        </thead>
    );
}

export default Thead;