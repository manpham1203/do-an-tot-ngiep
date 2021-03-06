import React, { useEffect, useReducer, useState } from "react";
import { FaTimes } from "react-icons/fa";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";

const MobileMenu = (props) => {
  const { menu, user } = useSelector((s) => s);
  return (
    <div
      className={`z-[10] fixed bg-third right-0 top-0 bottom-0 w-[100%] sm:w-[300px]  ${
        props.openMenu ? null : "translate-x-full"
      } transition-all duration-[0.3s]
      lg:hidden
      `}
    >
      <div className="flex justify-end p-[20px]">
        <button
          className="cursor-pointer text-[25px]"
          onClick={() => props.setOpenMenu(false)}
        >
          <FaTimes />
        </button>
      </div>
      <div className="bg-third h-full">
        <ul className="ml-[20px]">
          {menu.map((item, index) => {
            return (
              <li key={item.id} className="mt-[20px]">
                <Link to={item.slug} onClick={() => props.setOpenMenu(false)}>
                  {item.title}
                </Link>
              </li>
            );
          })}
          {user.id != null && (
            <li className="mt-[20px] md:hidden">
              <Link
                to="/danh-sach-yeu-thich"
                onClick={() => props.setOpenMenu(false)}
              >
                SẢN PHẨM YÊU THÍCH
              </Link>
            </li>
          )}

          <li className="mt-[20px] md:hidden">
            <Link to="/gio-hang" onClick={() => props.setOpenMenu(false)}>
              GIỎ HÀNG
            </Link>
          </li>
          <li className="mt-[20px] md:hidden">
            {typeof user.id === "string" ? (
              <Link to="/tai-khoan" onClick={() => props.setOpenMenu(false)}>
                TÀI KHOẢN
              </Link>
            ) : (
              <Link to="/dang-nhap" onClick={() => props.setOpenMenu(false)}>
                ĐĂNG NHẬP/ĐĂNG KÝ
              </Link>
            )}
          </li>
        </ul>
      </div>
    </div>
  );
};

export default MobileMenu;
