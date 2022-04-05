import React, { useEffect, useState } from "react";
import Login from "../../../components/Login/Login";
import Register from "../../../components/Register/Register";
const tabs=["Đăng Nhập", "Đăng Ký"]
function LoginPage(props) {
  const [tab, setTab]=useState(0);

  

  return (
    <div className="pt-[100px]">
      <div className="w-[575px] mx-auto px-[50px] pb-[50px] pt-[40px] bg-white">
        <div className="flex flex-row w-full border-b border-input-border">
        {tabs.map((item, index)=>{
          return(
            <div key={index}
            onClick={()=>setTab(index)}
             className={`w-full text-center p-[10px] cursor-pointer text-[22px]
             ${tab===index&&"border-b-2 border-b-main-color"}`}>
            {item}
          </div>
          )
        })}        
        </div>
        {tab===0?<Login />:<Register setTab={setTab} />}
        
      </div>
    </div>
  );
}

export default LoginPage;
