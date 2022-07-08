import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCloseadminViewPostCmt } from "../../redux/adminViewPostCmt/adminViewPostCmtActions";

function OverlayAdminViewPostCmt(props) {
    const dispatch=useDispatch();

    return (
        <div onClick={()=>dispatch(setCloseadminViewPostCmt())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default OverlayAdminViewPostCmt;