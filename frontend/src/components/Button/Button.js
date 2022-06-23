import React from 'react';

function Button({...props}) {
    return (
        <button {...props}
        className={`px-[20px] py-[10px] min-w-[200px] min-h-[46px] border ${props.disabled?"border-second  text-second ":"border-second bg-third text-second hover:bg-second hover:text-third"}  `}
        >
            {props.children}
        </button>
    );
}

export default Button;