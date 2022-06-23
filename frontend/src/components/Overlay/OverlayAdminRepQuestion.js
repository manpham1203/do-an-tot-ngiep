import React from 'react';
import { useDispatch } from 'react-redux';
import { setCloseadminRepQuestion } from "../../redux/adminRepQuestion/adminRepQuestionActions";

function OverlayAdminRepQuestion(props) {
    const dispatch=useDispatch();
    return (
        <div onClick={()=>dispatch(setCloseadminRepQuestion())} className={`bg-second/50 fixed w-full h-full top-0 z-[1000] cursor-pointer`}>
            
        </div>
    );
}

export default OverlayAdminRepQuestion;