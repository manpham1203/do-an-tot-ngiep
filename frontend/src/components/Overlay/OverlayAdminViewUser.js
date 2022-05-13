import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCloseAdminViewUser } from "../../redux/adminViewUser/adminViewUserActions";

function OverlayAdminViewUser(props) {
    const dispatch=useDispatch();

    return (
        <div onClick={()=>dispatch(setCloseAdminViewUser())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default OverlayAdminViewUser;