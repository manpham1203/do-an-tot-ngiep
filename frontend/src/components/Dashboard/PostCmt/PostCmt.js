import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../Pagination/Pagination";
import Select from "react-select";
import Table from "../../Table/Table";
import Thead from "../../Table/Thead";
import Tr from "../../Table/Tr";
import Th from "../../Table/Th";
import Tbody from "../../Table/Tbody";
import { FaRegEdit, FaRegEye } from "react-icons/fa";
import Td from "../../Table/Td";
import defaultuser from "../../../assets/defaultuser.png";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { setOpenadminViewPostCmt } from "../../../redux/adminViewPostCmt/adminViewPostCmtActions";
import { useDispatch } from "react-redux";

function PostCmt(props) {
  const [data, setData] = useState();
  const [currentPage, setCurrentPage] = useState(1);
  const [star, setStar] = useState(null);
  const [loading, setLoading] = useState(true);
  const fetchData = async () => {
    setLoading(true);
    await api({
      method: "GET",
      url: `/comment/CmtPagination`,
      params: {
        currentPage: currentPage,
        limit: 5,
        objectType: "post",
      },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
          setLoading(false);
        }
      })
      .catch(() => setLoading(true));
  };
  useEffect(() => {
    fetchData();
  }, [currentPage]);
  const dispatchQV = useDispatch();
  const handleQuickView = (id) => {
    const obj = {
      show: true,
      id: id,
    };
    dispatchQV(setOpenadminViewPostCmt(obj));
  };
  const [show, setShow] = useState(true);
  return (
    <div className="w-full bg-white shadow-admin rounded-[8px]">
      <div
        className={`p-[20px] flex flex-row justify-between items-center cursor-pointer rounded-[8px] ${
          show && "shadow-admin"
        }`}
        onClick={() => setShow(!show)}
      >
        <h2 className="text-[20px]">Bình luận bài viết</h2>
        {show ? <BsDashLg /> : <BsPlusLg />}
      </div>

      <div className={`w-full p-[20px] ${!show && "hidden"}`}>
        <Table className="border-collapse w-full ">
          <Thead>
            <Tr>
              <Th className="w-[80px]">Avatar</Th>
              <Th className="full">Bình luận</Th>

              <Th className="w-[150px]">Hành động</Th>
            </Tr>
          </Thead>
          <Tbody>
            {data?.list?.map((item) => {
              return (
                <Tr key={item.id}>
                  <Td className="py-[10px]">
                    <div className="w-full flex justify-center">
                      <div className="w-[60px] h-[60px]">
                        <img
                          src={item?.imageSrc || defaultuser}
                          alt=""
                          className="w-full h-full object-cover object-center"
                        />
                      </div>
                    </div>
                  </Td>
                  <Td className="full">
                    <div className="w-full px-[20px] short-desc-postcard2">
                      {item?.content}
                    </div>
                  </Td>

                  <Td>
                    <div className="flex flex-row justify-center text-[25px] gap-x-[20px] w-full">
                      <FaRegEye
                        onClick={() => handleQuickView(item?.id)}
                        className="cursor-pointer"
                      />
                      
                    </div>
                  </Td>
                </Tr>
              );
            })}
          </Tbody>
        </Table>
        <div className="mt-[20px]">
          {loading ? (
            "loading"
          ) : (
            <Pagination
              forcePage={currentPage}
              setCurrentPage={setCurrentPage}
              totalPage={data?.totalPage}
              itemsPerPage={data?.list?.length}
            />
          )}
        </div>
      </div>
    </div>
  );
}

export default PostCmt;
