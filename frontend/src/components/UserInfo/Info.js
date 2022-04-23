import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import api from "../../apis/api";
import defaultuser from "../../assets/defaultuser.png";
import * as moment from "moment";
import "moment/locale/nl";

function Info(props) {
  const [data, setData] = useState({});
  const store = useSelector((store) => store);
  const user = store.user;
  console.log("user", user);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/user/${id}`,
    })
      .then((res) => {
        console.log(res);
        setData(res.data);
      })
      .catch(() => console.log("vo day"));
  };
  useEffect(() => {
    if (user.id !== null) {
      fetchData(user.id);
    }
  }, [user.id]);
  const empty = '"chưa có thông tin"';
  return (
    <div className="flex flex-row gap-x-[20px]">
      <div className="rounded-[500px] overflow-hidden w-[200px] h-[200px]">
        <img src={defaultuser} alt="" className="" />
      </div>
      <div className="flex flex-row gap-x-[20px]">
        <div className="font-semibold">
          <h2 className="">Họ và tên:</h2>
          <h2 className="">Tên đăng nhập:</h2>
          <h2 className="">Ngày sinh:</h2>
          <h2 className="">Email:</h2>
          <h2 className="">Số điện thoại:</h2>
          <h2 className="">Địa chỉ:</h2>
        </div>
        <div>
          <h2 className="font-medium">
            {data.lastName} {data.firstName}
          </h2>
          <h2 className="font-medium">{data.username}</h2>
          <h2
            className={`${
              data.birthday === null ? "font-normal" : "font-medium"
            }`}
          >
            {moment(data.birthday).format("DD-MM-yyyy") || empty}
          </h2>

          <h2
            className={`${data.email === null ? "font-normal" : "font-medium"}`}
          >
            {data.email || empty}
          </h2>
          <h2
            className={`${
              data.phoneNumber === null ? "font-normal" : "font-medium"
            }`}
          >
            {data.phoneNumber || empty}
          </h2>
          <h2
            className={`${
              data.address === null ? "font-normal" : "font-medium"
            }`}
          >
            {data.address || empty}
          </h2>
        </div>
      </div>
    </div>
  );
}

export default Info;
