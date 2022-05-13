import React, { useEffect, useReducer } from "react";
import { NavLink } from "react-router-dom";
import { useSelector } from "react-redux";

function Navigation(props) {
  const {menu} = useSelector(s=>s);
  return (
    <ul className={`lg:flex flex-row items-center justify-center hidden `}>
      {menu.map((item, index) => {
        return (
          <li className="group relative flex flex-row" key={item.id}>
            <NavLink
              to={item.slug}
              className={`nav-link flex-row items-center p-[10px] ${
                props.navHome === false
                  ? "text-second"
                  : props.navHome && props.scroll
                  ? "text-second"
                  : "text-third"
              }`}
              style={({ isActive }) =>
                isActive ? { textDecoration: "underline", textUnderlineOffset:"4px", fontWeight:"500" } : {fontWeight:"400"}
              }
            >
              {item.title}
            </NavLink>
          </li>
        );
      })}
    </ul>
  );
}

export default Navigation;
