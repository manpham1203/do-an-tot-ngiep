import React, { useEffect, useState, memo, useReducer } from "react";

import { FiUser } from "react-icons/fi";
import { AiOutlineShopping } from "react-icons/ai";
import { GiHamburgerMenu } from "react-icons/gi";
import { FiSearch } from "react-icons/fi";
import { NavLink, Outlet, useLocation, useNavigate } from "react-router-dom";

import { useSelector } from "react-redux";
import MobileMenu from "../MobileMenu/MobileMenu";
import useClickOutSide from "../../hooks/useClickOutSide";
import Navigation from "../Navigation/Navigation";
import MenuOverlay from "../MobileMenu/MenuOverlay";
import Footer from "../Footer/Footer";

function NavBar() {
  const [scroll, setScroll] = useState(false);
  const [navHome, setNavHome] = useState(true);
  const [openMenu, setOpenMenu] = useState(false);
  const location = useLocation();
  const navigate = useNavigate();
  const store = useSelector((store) => store);
  const cart = store.cart;
  const user = store.user;

  useEffect(() => {
    if (location.pathname === "/") {
      setNavHome(true);
    } else {
      setNavHome(false);
    }
  }, [location.pathname]);

  useEffect(() => {
    const setNav = () => {
      setScroll(window.scrollY > 10);
    };
    window.addEventListener("scroll", setNav);
    return () => {
      window.removeEventListener("scroll", setNav);
    };
  }, []);

  return (
    <>
      <div
        className={`w-[100%] h-[50px] lg:h-[70px] z-[6] transition-all duration-[300ms] font-primary fixed font-medium flex items-center
      ${navHome && scroll ? "bg-[#F8F7F4] border-b border-b-[#161a2133] " : ""}
      ${navHome === false ? "border-b border-b-[#161a2133] bg-[#F8F7F4]" : ""}
      `}
      >
        <div className={`container mx-auto flex justify-between`}>
          {/* left */}
          <span
            className={`w-[150px] flex flex-row items-center justify-start
          ${
            navHome === false
              ? "text-black"
              : navHome && scroll
              ? "text-black"
              : "text-white"
          }
          `}
          >
            Logo
          </span>
          {/* center */}
          <Navigation navHome={navHome} scroll={scroll} />

          {/* right */}
          <div
            className={` w-[150px] flex flex-row items-center justify-end gap-x-[16px] md:gap-x-[25px] 
            transition-all duration-[0.3s]
            ${
              navHome === false
                ? "text-black"
                : navHome && scroll
                ? "text-black"
                : "text-white"
            }
            `}
          >
            <button className="text-[25px]">
              <FiSearch />
            </button>

            <div className="relative" onClick={() => navigate("/gio-hang")}>
              <AiOutlineShopping className="cursor-pointer text-[25px]" />
              {cart.length > 0 ? (
                <span className="absolute top-[-12px] right-[-12px] bg-red-500 text-white rounded-[50px] w-[18px] h-[18px] flex justify-center items-center text-[13px]">
                  {cart.length}
                </span>
              ) : null}
            </div>
            {typeof user.id === "string" ? (
              <button
                className="text-[25px]"
                onClick={() => navigate("/tai-khoan")}
              >
                <FiUser />
              </button>
            ) : (
              <button
                className="text-[25px]"
                onClick={() => navigate("/dang-nhap")}
              >
                <FiUser />
              </button>
            )}

            <button
              type="button"
              className="lg:hidden"
              onClick={() => setOpenMenu(true)}
            >
              <GiHamburgerMenu className="cursor-pointer text-[25px]" />
            </button>
          </div>

          {/* mobile menu */}
        </div>
      </div>
      <MenuOverlay openMenu={openMenu} setOpenMenu={setOpenMenu} />
      <MobileMenu openMenu={openMenu} setOpenMenu={setOpenMenu} />
    </>
  );
}

export default memo(NavBar);