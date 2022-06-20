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

function PostTrash(props) {
  const [data, setData] = useState({ totalPage: 0, totalResult: 0, data: [] });
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true);
  const [limit, setLimit] = useState(10);
  const [query, setQuery] = useState("");
  const fetchData = async () => {
    setLoading(true);
    await api({
      method: "GET",
      url: `/Post/RowsAdminDeleted`,
      params: {
        deleted: true,
        limit: limit,
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
            data: res.data.posts,
          });
        } else {
          setLoading(true);
        }
      })
      .catch(() => setLoading(true));
  };
  useEffect(() => {
    fetchData();
  }, [limit, query, currentPage]);

  const handleTrash = async (id) => {
    await api({
      method: "PUT",
      url: `/post/deleted/${id}`,
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
      url: `/post/delete/${id}`,
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
  const [productSelect, setProductSelect] = useState([]);
  const [checkAll, setCheckAll] = useState(false);
  const handleProductSelect = (event) => {
    setCheckAll(false);
    let newArray = [...productSelect, event.target.id];
    if (productSelect.includes(event.target.id)) {
      newArray = newArray.filter((x) => x !== event.target.id);
    }
    if (newArray.length === data.data.length) {
      setCheckAll(true);
    }
    setProductSelect(newArray);
  };
  const handleSelectAll = (e) => {
    if (e.target.checked) {
      setCheckAll(true);
      let newArr = [];
      for (var i = 0; i < data.data.length; i++) {
        newArr.push(data.data[i]);
      }
      setProductSelect(newArr);
    } else {
      setCheckAll(false);
      setProductSelect([]);
    }
  };
  const handleDeletedFalseList = async () => {
    await api({
      method: "PUT",
      url: `/post/DeletedFalseList`,
      data: productSelect,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Cập nhật thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          setShowTable(false);
          fetchData();
          setProductSelect([]);
          setCheckAll(false);
          setShowTable(true);
        } else {
          toast.error(`Cập nhật thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() =>
        toast.error(`Cập nhật thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        })
      );
  };
  const [showTable, setShowTable] = useState(true);
  return (
    <>
      <div className="py-[10px] shadow-admin rounded-[8px] flex flex-row items-center justify-end gap-x-[20px] px-[20px] mb-[20px]">
        <button
          onClick={handleDeletedFalseList}
          className="bg-yellow-400 rounded-[8px] h-[40px] px-[20px] text-third"
        >
          Khôi phục
        </button>
      </div>
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
        {showTable && (
          <Table className="w-full">
            <Thead>
              <Tr>
                <Th className="w-[60px]">
                  <div className="w-full justify-center">
                    <input
                      className="w-5 h-5 border-gray-200 rounded"
                      type="checkbox"
                      onChange={handleSelectAll}
                      checked={checkAll}
                    />
                  </div>
                </Th>
                <Th className="w-[100px]">Hình</Th>
                <Th>Tiêu đề</Th>
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
                      handleProductSelect={handleProductSelect}
                      productSelect={productSelect}
                    />
                  );
                })}
            </Tbody>
          </Table>
        )}

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
              {loading ? (
                "loading"
              ) : (
                <Pagination
                  forcePage={currentPage}
                  setCurrentPage={setCurrentPage}
                  totalPage={data?.totalPage}
                  itemsPerPage={data?.data.length}
                />
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default PostTrash;
