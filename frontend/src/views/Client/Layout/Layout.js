import React, { useEffect, useState } from "react";
import { Outlet, useLocation } from "react-router-dom";
import Footer from "../../../components/Footer/Footer";
import Header from "../../../components/NavBar/NavBar";
import { FaTimes } from "react-icons/fa";

function Layout(props) {
  const [mt, setMt] = useState(true);
  const location = useLocation();
  const [scroll, setScroll] = useState(false);
  useEffect(() => {
    if (location.pathname === "/") {
      setMt(false);
    } else {
      setMt(true);
    }
  }, [location]);
  useEffect(() => {
    const setNav = () => {
      setScroll(window.scrollY > 10);
    };
    window.addEventListener("scroll", setNav);
    return () => {
      window.removeEventListener("scroll", setNav);
    };
  }, []);
  const [top, setTop] = useState(true);
  return (
    <>
      {top && (
        <div
          className={`border-b border-b-[#161a2133]  bg-third w-[100%] z-[7] transition-all duration-[300ms] font-primary fixed font-medium 
      `}
        >
          <div className="container px-[10px] sm:px-[20px] mx-auto flex flex-row justify-between items-center h-[60px] sm:h-[30px] ">
            <p>Trang web này được tạo ra với mục đích học tập và nghiên cứu.</p>
            <FaTimes onClick={()=>setTop(false)} className="hover:text-second cursor-pointer text-gray-400" />
          </div>
        </div>
      )}

      <Header className={`${top && "mt-[60px] sm:mt-[30px]"}`} />
      <div className={`${mt && top ? "pt-[150px] sm:pt-[120px]" : mt && "pt-[70px] sm:pt-[100px]"} `}>
        <Outlet></Outlet>
      </div>

      <Footer />
    </>
  );
}

export default Layout;
