import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import api from "../../apis/api";
import { FaTimes } from "react-icons/fa";
import { setCloseAdminViewUser } from "../../redux/adminViewUser/adminViewUserActions";
import Table from "../Table/Table";
import Tr from "../Table/Tr";
import Td from "../Table/Td";
import Th from "../Table/Th";
import * as moment from "moment";
import "moment/locale/nl";
import defaultuser from '../../assets/defaultuser.png'

function AdminViewUser(props) {
  const dispatch = useDispatch();
  const { adminViewUser } = useSelector((s) => s);
  const data = adminViewUser.data;
  console.log(data);
  return (
    <div className="flex  p-[30px] bg-third fixed w-[80%] h-[500px] rounded-xl top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] z-[2000]">
      <div className="w-fit absolute right-0 top-0">
        <FaTimes
          onClick={() => dispatch(setCloseAdminViewUser())}
          className="inline-block text-[30px] text-second cursor-pointer"
        />
      </div>
      <div className="overflow-auto w-full h-full ">
        <Table className="border-separate w-full border border-gray-200">
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Id</Th>
            <Td className=" px-[20px]">{data?.id}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Tên đăng nhập</Th>
            <Td className=" px-[20px]">{data?.username}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Họ</Th>
            <Td className=" px-[20px]">{data?.lastName}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Tên</Th>
            <Td className=" px-[20px]">{data?.firstName}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Ngày sinh</Th>
            <Td className=" px-[20px]">
              {data?.birthday
                ? moment(data?.birthday).format("DD-MM-yyyy, h:mm:ss a")
                : "Chưa có"}
            </Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Địa chỉ</Th>
            <Td className=" px-[20px]">{data?.address || "Chưa có"}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Email</Th>
            <Td className=" px-[20px]">{data?.email || "Chưa có"}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Số điện thoại</Th>
            <Td className=" px-[20px]">{data?.phoneNumber || "Chưa có"}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Ngày tham gia</Th>
            <Td className=" px-[20px]">
              {moment(data?.createdAt).format("DD-MM-yyyy, h:mm:ss a")}
            </Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Cập nhật lần cuối</Th>
            <Td className=" px-[20px]">
              {data?.updatedAt == null
                ? "Chưa có"
                : moment(data?.updateAt).format("DD-MM-yyyy, h:mm:ss a")}
            </Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Vai trò</Th>
            <Td className=" px-[20px]">{data?.role === 0 && "Khách hàng"}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Hình</Th>
            <Td className=" px-[20px]">
              <div className="w-[100px] h-[100px]">
                <img
                  src={data.imageSrc || defaultuser}
                  className="w-full h-full object-cover object-center"
                  alt=""
                />
              </div>
            </Td>
          </Tr>
        </Table>
      </div>
    </div>
  );
}

export default AdminViewUser;
