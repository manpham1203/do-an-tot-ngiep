import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCloseAdminViewPost } from "../../redux/adminViewPost/adminViewPostActions";

function OverlayAdminViewPost(props) {
    const dispatch=useDispatch();

    return (
        <div onClick={()=>dispatch(setCloseAdminViewPost())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default OverlayAdminViewPost;