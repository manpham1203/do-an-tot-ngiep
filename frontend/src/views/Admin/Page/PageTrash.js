import React, { useEffect, useState } from "react";
import Table from "../../../components/Table/Table";
import Thead from "../../../components/Table/Thead";
import Th from "../../../components/Table/Th";
import Tbody from "../../../components/Table/Tbody";
import Tr from "../../../components/Table/Tr";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import Pagination from "../../../components/Pagination/Pagination";
import RowTrash from "./RowTrash";


function PageTrash(props) {
    const [data, setData] = useState({ totalPage: 0, totalResult: 0, data: [] });
  const [currentPage, setCurrentPage] = useState(1);
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/page/pagepagination`,
      params: {
        deleted: true,
        limit: 10,
        currentPage: currentPage,
      },
    })
      .then((res) => {
        setData({
          ...data,
          totalPage: res.data.totalPage,
          totalResult: res.data.totalResult,
          data: res.data.data,
        });
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, []);
  const handleTrash = async (id) => {
    await api({
      method: "PUT",
      url: `/page/deleted`,
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
      url: `/page/delete`,
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
        <Table className="w-full">
          <Thead>
            <Tr>
              <Th className="w-[50px]">
                <div className="flex justify-center">
                  <input
                    className="w-5 h-5 border-gray-200 rounded"
                    type="checkbox"
                    id="row_1"
                    disabled
                  />
                </div>
              </Th>
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
              {data?.data.length > 0 && (
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

export default PageTrash;