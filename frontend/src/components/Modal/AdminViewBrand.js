import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import api from "../../apis/api";
import { FaTimes } from "react-icons/fa";
import { setCloseadminViewBrand } from "../../redux/adminViewBrand/adminViewBrandActions";
import Table from "../Table/Table";
import Tr from "../Table/Tr";
import Td from "../Table/Td";
import Th from "../Table/Th";
import * as moment from "moment";
import "moment/locale/nl";

function AdminViewBrand(props) {
  const dispatch = useDispatch();
  const { adminViewBrand } = useSelector((s) => s);
  const [data, setData] = useState();
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/${adminViewBrand.type}/${adminViewBrand.type}detail/`,
      params: { id: id },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(adminViewBrand.id);
  }, [adminViewBrand.id]);
  console.log(data);
  return (
    <div className="flex  p-[30px] bg-third fixed w-[80%] h-[500px] rounded-xl top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] z-[2000]">
      <div className="w-fit absolute right-0 top-0">
        <FaTimes
          onClick={() => dispatch(setCloseadminViewBrand())}
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
            <Th className="text-left px-[20px] w-[300px]">Tên</Th>
            <Td className=" px-[20px]">{data?.name}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Slug</Th>
            <Td className=" px-[20px]">{data?.slug}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Phát hành</Th>
            <Td className=" px-[20px]">{data?.publish ? "True" : "False"}</Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Cập nhật lần cuối</Th>
            <Td className=" px-[20px]">
              {data?.updateAt == null
                ? "Chưa có"
                : moment(data?.updateAt).format("DD-MM-yyyy, h:mm:ss a")}
            </Td>
          </Tr>
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Ngày tạo</Th>
            <Td className=" px-[20px]">
              {moment(data?.createdAt).format("DD-MM-yyyy, h:mm:ss")}
            </Td>
          </Tr>
          
          <Tr>
            <Th className="text-left px-[20px] w-[300px]">Hình</Th>
            <Td className=" px-[20px]">
              <div className="w-[100px] h-[100px]">
                <img src={data?.imageSrc} alt="" className="w-full h-full object-cover object-center" />
              </div>
            </Td>
          </Tr>
        </Table>
      </div>
    </div>
  );
}

export default AdminViewBrand;
