import React, { useEffect, useState } from "react";
import Table from "../../../components/Table/Table";
import Thead from "../../../components/Table/Thead";
import Th from "../../../components/Table/Th";
import Tbody from "../../../components/Table/Tbody";
import Tr from "../../../components/Table/Tr";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import Row from "./Row";
import Pagination from "../../../components/Pagination/Pagination";
import RowTrash from "./RowTrash";

function BannerTrash(props) {
  const [data, setData] = useState({ totalPage: 0, totalResult: 0, data: [] });
  const [loading, setLoading]=useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [limit, setLimit] = useState(10);
  const [query, setQuery] = useState("");
  const fetchData = async () => {
    setLoading(true);
    await api({
      method: "GET",
      url: `/banner/pagination`,
      params: {
        deleted: true,
        limit: limit,
        currentPage: currentPage,
        query: query,
      },
    })
      .then((res) => {
        if(res.status === 200){
          setLoading(true);
          setData({
            ...data,
            totalPage: res.data.totalPage,
            totalResult: res.data.totalResult,
            data: res.data.bannerId,
          });
        }
       else{
        setLoading(true);
       }
      })
      .catch(() => setLoading(true));
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
          toast.warn(`Khôi phục thành công`, {
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
  const handleDelete = async (id) => {
    await api({
      method: "DELETE",
      url: `/banner/delete`,
      params: { id: id },
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Xoá thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(id);
        } else {
          toast.error(`Xoá thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Xoá thất bại`, {
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
        <Table className="w-full">
          <Thead>
            <Tr>
              <Th className="w-[100px]">Hình</Th>
              <Th>Nội dung</Th>
              <Th className="w-[150px]">Phát hành</Th>
              <Th className="w-[200px]">Hành động</Th>
            </Tr>
          </Thead>
          <Tbody>
            {data?.data?.length > 0 &&
              data?.data.map((item) => {
                return (
                  <RowTrash
                    key={item}
                    id={item}
                    handleTrash={handleTrash}
                    handleDelete={handleDelete}
                  />
                );
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
            {loading? "loading" : (
              <Pagination
                forcePage={currentPage}
                setCurrentPage={setCurrentPage}
                totalPage={data?.data?.totalPage}
                itemsPerPage={data.data?.length}
              />
            )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default BannerTrash;
