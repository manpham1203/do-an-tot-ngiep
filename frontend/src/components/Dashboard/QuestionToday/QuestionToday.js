import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../Pagination/Pagination";
import Table from "../../Table/Table";
import Thead from "../../Table/Thead";
import Th from "../../Table/Th";
import Tbody from "../../Table/Tbody";
import Tr from "../../Table/Tr";
import Td from "../../Table/Td";
import { BsPlusLg, BsDashLg } from "react-icons/bs";

function QuestionToday(props) {
  const [data, setData] = useState({
    totalPage: 0,
    totalResult: 0,
    questionVMs: [],
  });
  const [currentPage, setCurrentPage] = useState(1);
  const [query, setQuery] = useState({
    email: "",
    name: "",
    content: "",
  });
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/question/questiontoday`,
      params: {
        limit: 10,
        currentPage: currentPage,
        email: query.email,
        name: query.name,
        content: query.content,
      },
    })
      .then((res) => {
        setData({
          ...data,
          totalPage: res.data.totalPage,
          totalResult: res.data.totalResult,
          questionVMs: res.data.questionVMs,
        });
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, [query, currentPage]);
  const [show, setShow] = useState(false);
  return (
    <div className="w-full  bg-white shadow-admin rounded-[8px]">
      <div
        className={`p-[20px] flex flex-row justify-between items-center cursor-pointer rounded-[8px] ${
          show && "shadow-admin"
        }`}
        onClick={() => setShow(!show)}
      >
        <h2 className="text-[20px]">Liên hệ hôm nay <span className="text-red-500">({data.totalResult})</span></h2>
        {show ? <BsDashLg /> : <BsPlusLg />}
      </div>
      <div className={`${!show&&"hidden"} p-[20px]`}>
        <div className="flex flex-row gap-x-[20px]">
          <div className="inline-flex flex-col w-[250px] mb-[20px]">
            <label
              htmlFor="email"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Tìm theo email
            </label>
            <input
              id="email"
              value={query.email}
              onChange={(e) => setQuery({ ...query, email: e.target.value })}
              className="bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
            />
          </div>
          <div className="inline-flex flex-col w-[250px] mb-[20px]">
            <label
              htmlFor="name"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Tìm theo tên
            </label>
            <input
              id="name"
              value={query.name}
              onChange={(e) => setQuery({ ...query, name: e.target.value })}
              className="bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
            />
          </div>
          <div className="inline-flex flex-col w-[250px] mb-[20px]">
            <label
              htmlFor="content"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Tìm theo nội dung
            </label>
            <input
              id="content"
              value={query.content}
              onChange={(e) => setQuery({ ...query, content: e.target.value })}
              className="bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
            />
          </div>
        </div>

        <Table className="w-full">
          <Thead>
            <Tr>
              <Th className="w-[250px]">Họ và Tên</Th>
              <Th className="w-[250px]">Email</Th>
              <Th className="">Nội dung</Th>
            </Tr>
          </Thead>
          <Tbody>
            {data?.questionVMs?.map((item) => {
              return (
                <Tr key={item.id}>
                  <Td className="pl-[20px]">{item.name}</Td>
                  <Td className="pl-[20px]">{item.email}</Td>
                  <Td className="pl-[20px] ">
                    <p className="productCard2Name">{item.content}</p>
                  </Td>
                </Tr>
              );
            })}
          </Tbody>
        </Table>

        <div className="bg-white px-4 py-3 flex items-center justify-betweensm:px-6">
          <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
            <div>
              <p className="text-sm text-gray-700">
                Hiển thị{" "}
                <span className="font-medium">
                  {" "}
                  {data?.questionVMs?.length}{" "}
                </span>
                trong
                <span className="font-medium"> {data?.totalResult} </span>
                kết quả
              </p>
            </div>
            <div>
              {currentPage > 0 && (
                <Pagination
                  setCurrentPage={setCurrentPage}
                  totalPage={data?.totalPage}
                  itemsPerPage={data?.questionVMs?.length}
                />
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default QuestionToday;
