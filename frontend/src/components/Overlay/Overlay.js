import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setClose } from "../../redux/quickView/quickViewActions";

function Overlay(props) {
    const dispatch=useDispatch();
  
    return (
        <div onClick={()=>dispatch(setClose())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default Overlay;