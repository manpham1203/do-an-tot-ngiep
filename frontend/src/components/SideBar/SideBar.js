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
  const [tabMenu, setTabMenu] = useState(null);
  const iconParent = <FaRegCircle className="w-[24px]" />;
  const iconchild = <BsCircleFill className="w-[24px]" />;
  const location = useLocation();
  const Menu = [
    {
      title: "Sản Phẩm",
      icon: iconParent,
    },
    {
      title: "Thương Hiệu",
      icon: iconParent,
    },
    {
      title: "Danh mục",
      icon: iconParent,
    },
    {
      title: "Tin tức",
      icon: iconParent,
    },
    {
      title: "Banner",
      icon: iconParent,
    },
    {
      title: "Đơn hàng",
      icon: iconParent,
    },
    {
      title: "Người dùng",
      icon: iconParent,
    },
    {
      title: "Liên Hệ",
      icon: iconParent,
    },
  ];
  const ProductTab = [
    {
      title: "Danh Sách",
      path: "san-pham/danh-sach",
      icon: iconchild,
    },
    {
      title: "Tạo Mới",
      path: "san-pham/tao-moi",
      icon: iconchild,
    },
    {
      title: "Thùng Rác",
      path: "san-pham/thung-rac",
      icon: iconchild,
    },
  ];
  const BrandTab = [
    {
      title: "Danh Sách",
      path: "thuong-hieu/danh-sach",
      icon: iconchild,
    },
    {
      title: "Tạo Mới",
      path: "thuong-hieu/tao-moi",
      icon: iconchild,
    },
    {
      title: "Thùng Rác",
      path: "thuong-hieu/thung-rac",
      icon: iconchild,
    },
  ];
  const CategoryTab = [
    {
      title: "Danh Sách",
      path: "danh-muc/danh-sach",
      icon: iconchild,
    },
    {
      title: "Tạo Mới",
      path: "danh-muc/tao-moi",
      icon: iconchild,
    },
    {
      title: "Thùng Rác",
      path: "danh-muc/thung-rac",
      icon: iconchild,
    },
  ];
  const OrderTab = [
    {
      title: "Dơn Hàng",
      path: "don-hang/danh-sach",
      icon: iconchild,
    },
    // {
    //   title: "Đơn Hàng Chưa Xử Lý",
    //   path: "don-hang/chua-xu-li",
    //   icon: iconchild,
    // },
    // {
    //   title: "Đơn Hàng Đã huỷ",
    //   path: "don-hang/da-huy",
    //   icon: iconchild,
    // },
  ];
  const PostTab = [
    {
      title: "Danh Sách",
      path: "tin-tuc/danh-sach",
      icon: iconchild,
    },
    {
      title: "Tạo Mới",
      path: "tin-tuc/tao-moi",
      icon: iconchild,
    },
    {
      title: "Thùng Rác",
      path: "tin-tuc/da-xoa",
      icon: iconchild,
    },
  ];
  const BannerTab = [
    {
      title: "Danh Sách",
      path: "banner/danh-sach",
      icon: iconchild,
    },
    {
      title: "Tạo Mới",
      path: "banner/tao-moi",
      icon: iconchild,
    },
    {
      title: "Thùng Rác",
      path: "banner/da-xoa",
      icon: iconchild,
    },
  ];
  const UserTab = [
    {
      title: "Danh Sách",
      path: "nguoi-dung/danh-sach",
      icon: iconchild,
    },
    {
      title: "Thùng Rác",
      path: "nguoi-dung/da-xoa",
      icon: iconchild,
    },
  ];
  const ContactTab = [
    {
      title: "Danh Sách",
      path: "lien-he/danh-sach",
      icon: iconchild,
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
            Phạm Minh Mẫn
          </h1>
        </div>
        <NavLink
          to="/admin"
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
          {Menu.map((Menu, index) => (
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
                  {ProductTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
                  {BrandTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                          
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
              {tab === 2 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {CategoryTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                          
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
              {tab === 5 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {OrderTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                          
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
              {tab === 3 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {PostTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                          
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
              {tab === 6 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {UserTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                          
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
              {tab === 4 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {BannerTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                          
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
              {tab === 7 && (
                <ul className={`${index === tab ? null : "hidden"}`}>
                  {ContactTab.map((p, i) => {
                    return (
                      <li key={i} onClick={() => setTabMenu(i)}>
                        <NavLink
                          to={p.path}
                          style={({ isActive }) =>
                            isActive
                              ? {
                                  backgroundColor: "#494E53",
                                  color: "#D1D5DB",
                                }
                              : undefined
                          }
                          className={`
                          
                  relative flex justify-between items-center h-[40px] font-semibold text-[16px] rounded-md p-2 cursor-pointer
                    hover:bg-[#494E53]
                    text-gray-300 text-sm gap-x-4 mt-2
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
        <div className="w-full p-[20px] " id="abc">
          <Outlet />
        </div>
      </div>
    </div>
  );
}

export default SideBar;
