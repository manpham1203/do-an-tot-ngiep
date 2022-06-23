import React, { useState } from "react";
// import { styled, useTheme } from '@mui/material/styles';
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import List from "@mui/material/List";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemText from "@mui/material/ListItemText";
import ListItemIcon from "@mui/material/ListItemIcon";
import IconButton from "@mui/material/IconButton";
import { GiHamburgerMenu } from "react-icons/gi";
import { FaRegCircle } from "react-icons/fa";
import { BsCircleFill } from "react-icons/bs";
import { IoIosArrowBack, IoIosArrowDown } from "react-icons/io";
import { NavLink, Outlet, useLocation, useNavigate } from "react-router-dom";
import Collapse from "@mui/material/Collapse";

const iconchild = <FaRegCircle className="w-[24px]" />;
const iconParent = <BsCircleFill className="w-[24px]" />;
const Menu = [
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
  {
    title: "Danh mục",
    pathname: "/admin/danh-muc/bang-danh-muc",
    icon: iconParent,
  },
];
const ProductTab = [
  {
    title: "Danh Sách",
    path: "san-pham",
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
    path: "thuong-hieu",
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
    path: "danh-muc",
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
function SideBar2(props) {
  const navigate = useNavigate();
  const [open, setOpen] = useState(true);
  const [openTab, setOpenTab] = useState(undefined);
  const drawerWidth = 240;
  const onHandleOpenTab = (index) => {
    if (index === openTab) {
      setOpenTab(undefined);
    } else {
      setOpenTab(index);
    }
  };
  return (
    <div className="tw-flex">
      <AppBar
        position="fixed"
        className={`${
          open ? `tw-w-[calc(100%--240px)]` : `tw-w-[calc(100%--80px)]`
        }
        tw-transition-all tw-duration-200`}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            onClick={() => setOpen(!open)}
            edge="start"
          >
            <GiHamburgerMenu />
          </IconButton>
          <Typography variant="h6" component="div" className="tw-flex-grow">
            News
          </Typography>
          <Button color="inherit">Login</Button>
        </Toolbar>
      </AppBar>
      <AppBar
        position="fixed"
        className={`tw-transition-all tw-duration-200 tw-h-[64px] tw-bg-white tw-left-0 ${
          open ? `tw-w-[240px]` : "tw-w-[80px]"
        }`}
      >
        haha
      </AppBar>
      <div
        className={`tw-transition-all tw-duration-200 ${
          open ? `tw-w-[240px]` : "tw-w-[80px]"
        } tw-border-r border tw-fixed tw-overflow-y-auto tw-h-[calc(100vh-64px)] tw-mt-[64px]`}
      >
        <List>
          {Menu.map((item, index) => {
            return (
              <React.Fragment key={index}>
                <ListItemButton
                  onClick={() => onHandleOpenTab(index)}
                  className={`tw-transition-all tw-duration-200
                  ${open ? "" : "tw-h-[48px] tw-justify-center"}
                  ${openTab === index ? "tw-bg-[#EEEDEA]" : ""}
                  `}
                >
                  <ListItemIcon className={`tw-justify-center`}>
                    {/* <GiHamburgerMenu /> */}
                    {item.icon}
                  </ListItemIcon>
                  <ListItemText
                    className={`${
                      open ? "tw-opacity-[1]" : "tw-opacity-0"
                    } tw-transition-all tw-duration-200 `}
                  >
                    {item.title}
                  </ListItemText>
                  {openTab === index ? <IoIosArrowBack /> : <IoIosArrowDown />}
                </ListItemButton>
                <Collapse
                  in={openTab === index ? true : false}
                  timeout="auto"
                  unmountOnExit
                >
                  {openTab === 0 && (
                    <List component="div" disablePadding>
                      {ProductTab.map((itemChild, indexChild) => {
                        return (
                          <ListItemButton
                            onClick={() => navigate(itemChild.path)}
                            key={indexChild}
                            className={`tw-transition-all tw-duration-200 tw-ml-[10px] ${
                              open ? "" : "tw-h-[48px] tw-justify-center"
                            }`}
                          >
                            <ListItemIcon className={`tw-justify-center`}>
                              {/* <GiHamburgerMenu /> */}
                              {itemChild.icon}
                            </ListItemIcon>
                            <ListItemText
                              className={`${
                                open ? "tw-opacity-[1]" : "tw-opacity-0"
                              } tw-transition-all tw-duration-200 `}
                            >
                              {itemChild.title}
                            </ListItemText>
                          </ListItemButton>
                        );
                      })}
                    </List>
                  )}
                  {openTab === 1 && (
                    <List component="div" disablePadding>
                      {BrandTab.map((itemChild, indexChild) => {
                        return (
                          <ListItemButton
                            onClick={() => navigate(itemChild.path)}
                            key={indexChild}
                            className={`tw-transition-all tw-duration-200 tw-ml-[10px] ${
                              open ? "" : "tw-h-[48px] tw-justify-center"
                            }`}
                          >
                            <ListItemIcon className={`tw-justify-center`}>
                              {/* <GiHamburgerMenu /> */}
                              {itemChild.icon}
                            </ListItemIcon>
                            <ListItemText
                              className={`${
                                open ? "tw-opacity-[1]" : "tw-opacity-0"
                              } tw-transition-all tw-duration-200 `}
                            >
                              {itemChild.title}
                            </ListItemText>
                          </ListItemButton>
                        );
                      })}
                    </List>
                  )}
                  {openTab === 2 && (
                    <List component="div" disablePadding>
                      {CategoryTab.map((itemChild, indexChild) => {
                        return (
                          <ListItemButton
                            onClick={() => navigate(itemChild.path)}
                            key={indexChild}
                            className={`tw-transition-all tw-duration-200 tw-ml-[10px] ${
                              open ? "" : "tw-h-[48px] tw-justify-center"
                            }`}
                          >
                            <ListItemIcon className={`tw-justify-center`}>
                              {/* <GiHamburgerMenu /> */}
                              {itemChild.icon}
                            </ListItemIcon>
                            <ListItemText
                              className={`${
                                open ? "tw-opacity-[1]" : "tw-opacity-0"
                              } tw-transition-all tw-duration-200 `}
                            >
                              {itemChild.title}
                            </ListItemText>
                          </ListItemButton>
                        );
                      })}
                    </List>
                  )}
                </Collapse>
              </React.Fragment>
            );
          })}
        </List>
      </div>
      <div
        className={`tw-w-[calc(100%-240px)] tw-ml-[240px] tw-mt-[64px] tw-p-[20px]`}
      >
        <Outlet />
      </div>
    </div>
  );
}

export default SideBar2;
