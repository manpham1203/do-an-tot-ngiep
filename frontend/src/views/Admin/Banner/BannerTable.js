import React, { useEffect, useState } from "react";
import Table from "../../../components/DataTable/Table";
import Thead from "../../../components/DataTable/Thead";
import Tbody from "../../../components/DataTable/Tbody";
import Tr from "../../../components/DataTable/Tr";
import Th from "../../../components/DataTable/Th";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import Row from "./Row";
import Pagination from "../../../components/Pagination/Pagination";

function BannerTable(props) {
  const [data, setData] = useState({ totalPage: 0, totalResult: 0, data: [] });
  const [currentPage, setCurrentPage] = useState(1);
  const [limit, setLimit] = useState(10);
  const [query, setQuery] = useState("");
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/banner/pagination`,
      params: {
        deleted: false,
        limit: limit,
        currentPage: currentPage,
        query: query,
      },
    })
      .then((res) => {
        setData({
          ...data,
          totalPage: res.data.totalPage,
          totalResult: res.data.totalResult,
          data: res.data.bannerId,
        });
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, [query]);
  const handleTrash = async (id) => {
    await api({
      method: "PUT",
      url: `/banner/deleted`,
      params: { id: id },
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Chuyển vào thùng rác thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData();
        } else {
          toast.error(`Thao tác thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thao tác thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  return (
    <div>
      <div className="bg-white mb-[20px] shadow-admin rounded-[8px] overflow-hidden p-[20px]">
        <div className="flex flex-col w-[300px] mb-[20px]">
          <label
            htmlFor="search"
            className="block mb-2 text-sm font-medium text-gray-900 "
          >
            Tìm kiếm
          </label>
          <input
            id="search"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            className="bg-gray-50 block p-2.5 border focus:ring-1 outline-none border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500"
          />
        </div>
        <Table>
          <Thead>
            <Tr>
              <Th className="w-[100px]">Hình</Th>
              <Th>Nội dung</Th>
              <Th className="w-[200px]">Phát hành</Th>
              <Th className="w-[200px]">Hành động</Th>
            </Tr>
          </Thead>
          <Tbody>
            {data?.data?.length > 0 &&
              data?.data.map((item) => {
                return <Row key={item} id={item} handleTrash={handleTrash} />;
              })}
          </Tbody>
        </Table>

        <div className="bg-white px-4 py-3 flex items-center justify-betweensm:px-6">
          <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
            <div>
              <p className="text-sm text-gray-700">
                Hiển thị{" "}
                <span className="font-medium"> {data?.data?.length} </span>
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
                  itemsPerPage={data?.data.length}
                />
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default BannerTable;
