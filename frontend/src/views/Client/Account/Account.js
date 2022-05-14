import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import ChangePassword from "../../../components/UserInfo/ChangePassword";
import EditInfo from "../../../components/UserInfo/EditInfo";
import Info from "../../../components/UserInfo/Info";
import OrderList from "../../../components/UserInfo/OrderList";
import { logout } from "../../../redux/user/userActions";
import OrderCancel from '../../../components/UserInfo/OrderCancel';
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
  const [tab, setTab] = useState(0);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const handleLogout = () => {
    dispatch(logout());
    navigate("/dang-nhap");
  };
  const { user } = useSelector((store) => store);
  return (
    <>
      {user.id !== null && (
        <div className="mt-[30px] container mx-auto flex flex-row gap-x-[20px]">
          <div className="w-[400px]">
            {data.map((item, index) => {
              return (
                <div
                  key={index}
                  className={`${
                    index === 0 ? "border-t border-t-[#ebebeb]" : null
                  } border-b border-b-[#ebebeb] p-[15px] uppercase`}
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
            <div className="w-[400px]">
              <h2
                onClick={handleLogout}
                className="border-b border-b-[#ebebeb] p-[15px] hover:underline uppercase cursor-pointer"
              >
                đăng xuất
              </h2>
            </div>
          </div>
          <div className="w-full flex">
            {/* {tab === 0 && <Info />} */}
            {tab === 1 && <EditInfo />}
            {tab === 2 && <ChangePassword />}
            {tab === 3 && <OrderList userId={user.id} />}
            {tab === 4 && <OrderCancel userId={user.id} />}
            {tab === 5 && <Ordered userId={user.id} />}
            {tab === 6 && <PurchasedProduct userId={user.id} />}
          </div>
        </div>
      )}
    </>
  );
}

export default Account;
