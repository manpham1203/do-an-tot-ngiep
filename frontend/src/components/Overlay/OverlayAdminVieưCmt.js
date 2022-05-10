import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCloseadminViewCmt } from "../../redux/adminViewCmt/adminViewCmtActions";

function OverlayAdminViewCmt(props) {
    const dispatch=useDispatch();

    return (
        <div onClick={()=>dispatch(setCloseadminViewCmt())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default OverlayAdminViewCmt;