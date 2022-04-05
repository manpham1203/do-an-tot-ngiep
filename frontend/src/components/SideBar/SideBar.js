import React, { useEffect, useState } from "react";
import defaultuser from "../../assets/defaultuser.png";
import { IoIosArrowBack } from "react-icons/io";
import { GiHamburgerMenu } from "react-icons/gi";
import { FaRegCircle } from "react-icons/fa";
import { BsCircleFill } from "react-icons/bs";
import { NavLink, Outlet, useLocation } from "react-router-dom";

function SideBar(props) {
  const [open, setOpen] = useState(true);
  const [tab, setTab] = useState(null);
  const iconParent = <FaRegCircle className="w-[24px]" />;
  const iconchild = <BsCircleFill className="w-[24px]" />;
  const location = useLocation();
  const Menus = [
    {
      title: "Sản Phẩm",
      pathname: "/admin/san-pham/bang-san-pham",
      icon: iconParent,
    },
    {
      title: "Thương Hiệu",
      pathname: "/admin/thuong-hieu/bang-thuong-hieu",
      icon: iconParent,
    },
  ];
  const Product = [
    {
      title: "Bảng sản phẩm",
      to: "/admin/bang-san-pham",
      icon: iconchild,
    },
    {
      title: "Thêm sản phẩm",
      to: "/admin/them-san-pham",
      icon: iconchild,
    },
    { title: "Thùng rác", to: "/admin/thung-rac-san-pham", icon: iconchild },
  ];
  const Brand = [
    {
      title: "Bảng thương hiệu",
      to: "/admin/bang-thuong-hieu",
      icon: iconParent,
    },
    {
      title: "Thêm thương hiệu",
      to: "/admin/them-thuong-hieu",
      icon: iconParent,
    },
    {
      title: "Thùng rác",
      to: "/admin/thung-rac-thuong-hieu",
      icon: iconParent,
    },
  ];
  const handleClick = (i) => {
    if (tab === i) {
      return setTab(null);
    }
    setTab(i);
  };
  return (
    <div className="flex">
      <div
        id="style-4"
        className={`overflow-y-auto fixed top-0 ${
          open ? "w-[300px]" : "w-[80px] "
        } bg-sidebar h-screen p-5  pt-8 z-50 transition-all duration-200`}
      >
        <div className="flex gap-x-4 items-center">
          <img
            src={defaultuser}
            alt=""
            className={`w-[40px]  cursor-pointer duration-500 ${
              open && "rotate-[360deg]"
            }`}
          />
          <h1
            className={`text-white origin-left font-medium text-xl duration-200 ${
              !open && "scale-0"
            }`}
          >
            Designer
          </h1>
        </div>
        <NavLink
          to="/admin/dashboard"
          className={`text-[#343a40] bg-[#EBECEC] mt-6 relative flex justify-between h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointertext-gray-300 text-sm items-center gap-x-4`}
        >
          <div className="flex flex-row items-center">
            <FaRegCircle className="w-[24px]" />
            <span className={`${!open && "hidden"} origin-left duration-200`}>
              Dashboard
            </span>
          </div>
        </NavLink>
        <div className="">
          {Menus.map((Menu, index) => (
            <div key={index}>
              <div
                onClick={() => handleClick(index)}
                className={`relative flex justify-between  h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer 
                  text-sm items-center text-[#343a40] bg-[#EBECEC]  mt-2
                   `}
              >
                <div className="flex flex-row items-center">
                  {Menu.icon}
                  <span
                    className={`${!open && "hidden"} origin-left duration-200`}
                  >
                    {Menu.title}
                  </span>
                </div>
                <div>
                  <IoIosArrowBack
                    className={`${!open && "hidden"} duration-200 ${
                      tab === index && "rotate-[-90deg]"
                    }`}
                  />
                </div>
              </div>
              {tab === 0 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {Product.map((p, i) => {
                    return (
                      <li key={i}>
                        <NavLink
                          to={p.to}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                            ${
                              location.pathname !== p.to && "hover:bg-[#494E53]"
                            }
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer ${
                    tab !== index && "hover:bg-[#494E53]"
                  }  text-gray-300 text-sm items-center gap-x-4 mt-2
                                `}
                        >
                          <div className="flex flex-row items-center">
                            {p.icon}
                            <span
                              className={`${
                                !open && "hidden"
                              } origin-left duration-200`}
                            >
                              {p.title}
                            </span>
                          </div>
                        </NavLink>
                      </li>
                    );
                  })}
                </ul>
              )}
              {tab === 1 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {Brand.map((p, i) => {
                    return (
                      <li key={i}>
                        <NavLink
                          to={p.to}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                            ${
                              location.pathname !== p.to && "hover:bg-[#494E53]"
                            }
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer ${
                    tab !== index && "hover:bg-[#494E53]"
                  }  text-gray-300 text-sm items-center gap-x-4 mt-2
                                `}
                        >
                          <div className="flex flex-row items-center">
                            {p.icon}
                            <span
                              className={`${
                                !open && "hidden"
                              } origin-left duration-200`}
                            >
                              {p.title}
                            </span>
                          </div>
                        </NavLink>
                      </li>
                    );
                  })}
                </ul>
              )}
            </div>
          ))}
        </div>
      </div>
      <div
        className={` h-screen ${
          open ? "ml-[300px]" : "ml-[80px]"
        } w-full  transition-all duration-200`}
      >
        <div className="flex flex-row items-center gap-x-[25px] w-full h-[60px] bg-white shadow-admin">
          <GiHamburgerMenu
            className="text-[25px] ml-[25px] cursor-pointer"
            src="./src/assets/control.png"
            onClick={() => setOpen(!open)}
          />
          <h2>home</h2>
        </div>
        <div className="w-full p-[20px] ">
          <div className="flex justify-between items-center h-[50px] bg-white px-[20px] mb-[20px] shadow-admin rounded-[8px]">
            <h2 className="font-semibold text-[25px]">Sản phẩm</h2>
            <p>san-pham/bang-san-pham</p>
          </div>
          <div className="bg-white rounded-[8px] p-[20px] shadow-admin">
            <Outlet />
          </div>
        </div>
      </div>
    </div>
  );
}

export default SideBar;
