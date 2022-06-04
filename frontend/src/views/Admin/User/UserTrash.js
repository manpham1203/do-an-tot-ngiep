import React, { useEffect, useState } from "react";
import Table from "../../../components/Table/Table";
import Thead from "../../../components/Table/Thead";
import Th from "../../../components/Table/Th";
import Tbody from "../../../components/Table/Tbody";
import Tr from "../../../components/Table/Tr";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import RowTrash from "./RowTrash";
import Pagination from "../../../components/Pagination/Pagination";

function UserTrash(props) {
  const [data, setData] = useState({ totalPage: 0, totalResult: 0, ids: [] });
  const [currentPage, setCurrentPage] = useState(1);
  const [query, setQuery] = useState("");
  const [loading, setLoading] = useState(true);
  const fetchData = async () => {
    setLoading(true);
    await api({
      method: "GET",
      url: `/user/userpagination`,
      params: {
        deleted: true,
        limit: 10,
        currentPage: currentPage,
        query: query,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          setLoading(false);
          setData({
            ...data,
            totalPage: res.data.totalPage,
            totalResult: res.data.totalResult,
            ids: res.data.ids,
          });
        } else {
          setLoading(true);
        }
      })
      .catch(() => setLoading(true));
  };
  useEffect(() => {
    fetchData();
  }, [query, currentPage]);

  const handleTrash = async (id) => {
    await api({
      method: "PUT",
      url: `/user/deleted`,
      params: { id: id },
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Khôi phục thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(id);
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
      method: "Delete",
      url: `/user/delete`,
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
    <>
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
              <Th className="w-[60px]">
                <div className="w-full justify-center">
                  <input
                    className="w-5 h-5 border-gray-200 rounded"
                    type="checkbox"
                    disabled
                  />
                </div>
              </Th>
              <Th className="w-[50px]">Hình</Th>
              <Th>Tiêu đề</Th>
              <Th className="w-[150px]">Phát hành</Th>
              <Th className="w-[200px]">Hành động</Th>
            </Tr>
          </Thead>
          <Tbody>
            {data?.ids?.length > 0 &&
              data?.ids.map((item) => {
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
                <span className="font-medium"> {data?.ids?.length} </span>
                trong
                <span className="font-medium"> {data?.totalResult} </span>
                kết quả
              </p>
            </div>
            <div>
              {loading ? (
                "loading"
              ) : (
                <Pagination
                  forcePage={currentPage}
                  setCurrentPage={setCurrentPage}
                  totalPage={data?.totalPage}
                  itemsPerPage={data?.ids?.length}
                />
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default UserTrash;
