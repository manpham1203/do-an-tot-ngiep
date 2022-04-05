import React from 'react';

function Button({...props}) {
    return (
        <button {...props}
        className={`px-[20px] py-[10px] min-w-[200px] min-h-[46px] border ${props.disabled?"border-main-color  text-main-color ":"border-main-color bg-white text-main-color hover:bg-main-color hover:text-white"}  `}
        >
            {props.children}
        </button>
    );
}

export default Button;