import React, { useEffect, useState, memo, useReducer } from "react";

import { FiUser, FiMoon, FiSun } from "react-icons/fi";
import { AiOutlineShopping } from "react-icons/ai";
import { GiHamburgerMenu } from "react-icons/gi";
import { FiHeart } from "react-icons/fi";
import { Link, Outlet, useLocation, useNavigate } from "react-router-dom";

import { useSelector } from "react-redux";
import MobileMenu from "../MobileMenu/MobileMenu";
import useClickOutSide from "../../hooks/useClickOutSide";
import Navigation from "../Navigation/Navigation";
import MenuOverlay from "../MobileMenu/MenuOverlay";
import Footer from "../Footer/Footer";

function NavBar(props) {
  const [scroll, setScroll] = useState(false);
  const [navHome, setNavHome] = useState(true);
  const [openMenu, setOpenMenu] = useState(false);
  const location = useLocation();
  const navigate = useNavigate();
  const { cart, user } = useSelector((store) => store);

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

  const [sun, setSun] = useState(true);

  return (
    <>
      <div
        className={`w-[100%] h-[50px] lg:h-[70px] z-[6] transition-all duration-[300ms] font-primary fixed font-medium flex items-center
      ${navHome && scroll ? "bg-third border-b border-b-[#161a2133] " : ""}
      ${navHome === false ? "border-b border-b-[#161a2133] bg-third" : ""}
      ${props.className}
      `}
      >
        <div
          className={`container px-[10px] sm:px-[20px] mx-auto flex justify-between`}
        >
          {/* left */}
          <span
            className={`w-fit lg:w-[150px] flex flex-row items-center justify-start text-[25px] cursor-pointer
          ${
            navHome === false
              ? "text-black"
              : navHome && scroll
              ? "text-second"
              : "text-third"
          }
          `}
          onClick={() => navigate("/")}
          >
            WatchStore
          </span>
          {/* center */}
          <Navigation navHome={navHome} scroll={scroll} />

          {/* right */}
          <div
            className={`w-fit lg:w-[150px] flex  flex-row items-center justify-end gap-x-[16px] md:gap-x-[25px] 
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
            {user.id != null && (
              <Link
                to="/danh-sach-yeu-thich"
                className="text-[25px] hidden md:inline-block"
              >
                <FiHeart />
              </Link>
            )}

            <div
              className="relative  hidden md:inline-block"
              onClick={() => navigate("/gio-hang")}
            >
              <AiOutlineShopping className="cursor-pointer text-[25px]" />
              {cart.length > 0 ? (
                <span className="absolute top-[-12px] right-[-12px] bg-red-500 text-white rounded-[50px] w-[18px] h-[18px] flex justify-center items-center text-[13px]">
                  {cart.length}
                </span>
              ) : null}
            </div>
            {typeof user.id === "string" ? (
              <button
                className="text-[25px]  hidden md:inline-block"
                onClick={() => navigate("/tai-khoan")}
              >
                <FiUser />
              </button>
            ) : (
              <button
                className="text-[25px]  hidden md:inline-block"
                onClick={() => navigate("/dang-nhap")}
              >
                <FiUser />
              </button>
            )}

            <button className="text-[25px] " onClick={() => setSun(!sun)}>
              {sun ? <FiSun /> : <FiMoon />}
            </button>
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
