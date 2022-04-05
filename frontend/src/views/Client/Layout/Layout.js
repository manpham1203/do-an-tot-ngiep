import React from "react";
import { Outlet } from "react-router-dom";
import Footer from "../../../components/Footer/Footer";
import Header from "../../../components/NavBar/NavBar";

function Layout(props) {
  return (
    <>
      <Header />
      <Outlet></Outlet>
      <Footer />
    </>
  );
}

export default Layout;
