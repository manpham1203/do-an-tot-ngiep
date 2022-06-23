import userEvent from "@testing-library/user-event";
import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
// import bglogin from '../../../assets/bglogin.jpg'
import Login from "../../../components/Login/Login";
import Register from "../../../components/Register/Register";
const tabs = ["Đăng Nhập", "Đăng Ký"];
function LoginPage(props) {
  document.title = "Đăng Nhập";
  const [tab, setTab] = useState(0);
  const { user } = useSelector((store) => store);

  return (
    <>
      {user.id === null && (
        <div className="mt-[40px] container px-[10px] sm:px-[20px] mx-auto">
          <div className="w-full md:w-[575px] mx-auto px-[10px] sm:px-[20px] md:px-[50px] pb-[50px] pt-[40px] bg-third dark:bg-darkMode border border-second dark:border-third">
            <div className="flex flex-row w-full border-b border-input-border">
              {tabs.map((item, index) => {
                return (
                  <div
                    key={index}
                    onClick={() => setTab(index)}
                    className={`w-full text-center p-[10px] cursor-pointer text-[18px] md:text-[22px] text-second dark:text-third
             ${tab === index && "border-b-2 border-second dark:border-third"}`}
                  >
                    {item}
                  </div>
                );
              })}
            </div>
            {tab === 0 ? <Login /> : <Register setTab={setTab} />}
          </div>
        </div>
      )}
    </>
  );
}

export default LoginPage;
