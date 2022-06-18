import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import ChangePassword from "../../../components/UserInfo/ChangePassword";
import EditInfo from "../../../components/UserInfo/EditInfo";
import Info from "../../../components/UserInfo/Info";
import OrderList from "../../../components/UserInfo/OrderList";
import { logout } from "../../../redux/user/userActions";
import OrderCancel from "../../../components/UserInfo/OrderCancel";
import Ordered from "../../../components/UserInfo/Ordered";
import PurchasedProduct from "../../../components/UserInfo/PurchasedProduct";

const data = [
  // "thông tin cá nhân",
  "sửa thông tin",
  "đổi mật khẩu",
  "quản lý đơn hàng",
  "đơn hàng đã huỷ",
  "lịch sử mua hàng",
  "sản phẩm đã mua",
];
function Account(props) {
  document.title = "Quản lý tài khoản";
  const [tab, setTab] = useState(0);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const handleLogout = () => {
    dispatch(logout());
    navigate("/dang-nhap");
  };
  const { user } = useSelector((store) => store);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return (
    <>
      {user.id !== null && (
        <div className="container px-[10px] sm:px-[20px] mx-auto flex flex-col lg:flex-row gap-x-[20px]">
          <div className="w-full lg:w-[350px] flex flex-row lg:flex-col flex-wrap mb-[25px] lg:mb-[0] gap-x-[20px] text-second dark:text-third">
            {data.map((item, index) => {
              return (
                <div
                  key={index}
                  className={`${
                    index === 0 ? "lg:border-t border-t-[#ebebeb]" : null
                  } border-b border-b-[#ebebeb] p-[15px] uppercase `}
                >
                  <h2
                    onClick={() => setTab(index)}
                    className="hover:underline cursor-pointer"
                  >
                    {item}
                  </h2>
                </div>
              );
            })}
            <div className="lg:w-[350px]">
              <h2
                onClick={handleLogout}
                className="border-b border-b-[#ebebeb] p-[15px] hover:underline uppercase cursor-pointer"
              >
                đăng xuất
              </h2>
            </div>
          </div>
          <div className="w-full ">
            {/* {tab === 0 && <Info />} */}
            {tab === 0 && <EditInfo />}
            {tab === 1 && <ChangePassword />}
            {tab === 2 && <OrderList userId={user.id} />}
            {tab === 3 && <OrderCancel userId={user.id} />}
            {tab === 4 && <Ordered userId={user.id} />}
            {tab === 5 && <PurchasedProduct userId={user.id} />}
          </div>
        </div>
      )}
    </>
  );
}

export default Account;
