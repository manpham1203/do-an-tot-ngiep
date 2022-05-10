import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCloseAdminViewProduct } from "../../redux/adminViewProduct/adminViewProductActions";

function OverlayAdminViewProduct(props) {
    const dispatch=useDispatch();

    return (
        <div onClick={()=>dispatch(setCloseAdminViewProduct())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default OverlayAdminViewProduct;