import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCloseAdminViewOrder } from "../../redux/adminViewOrder/adminViewOrderActions";

function OverlayAdminViewOrder(props) {
    const dispatch=useDispatch();

    return (
        <div onClick={()=>dispatch(setCloseAdminViewOrder())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default OverlayAdminViewOrder;