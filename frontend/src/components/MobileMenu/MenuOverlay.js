import React from 'react';

function MenuOverlay(props) {
    return (
        <div onClick={()=>props.setOpenMenu(false)} className={`${props.openMenu?null:"hidden"} lg:hidden w-100% h-screen bg-[black] opacity-[0.5] fixed top-0 left-0 right-0 bottom-0 z-[7] transition-all duration-[0.3s]`}>
            
        </div>
    );
}

export default MenuOverlay;