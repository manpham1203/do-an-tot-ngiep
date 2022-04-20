import React, { useEffect, useState } from "react";
import { Outlet, useLocation } from "react-router-dom";
import Footer from "../../../components/Footer/Footer";
import Header from "../../../components/NavBar/NavBar";

function Layout(props) {
  const [mt, setMt]=useState(true);
  const location = useLocation();
  useEffect(()=>{
    if(location.pathname==="/"){
      setMt(false);
    }
    else{
      setMt(true);
    }
  }, [location])
  return (
    <>
      <Header />
      <div className={`${mt && 'pt-[70px]'} `}>
        <Outlet></Outlet>
      </div>
      <Footer />
    </>
  );
}

export default Layout;
