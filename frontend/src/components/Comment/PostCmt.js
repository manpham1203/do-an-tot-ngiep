import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import PostCmtItem from "./PostCmtItem";
import Pagination from "../Pagination/Pagination";

function ProductCmt(props) {
  const [data, setData] = useState({
    totalPage: 0,
    totalResult: 0,
    list: [],
  });
  const [currentPage, setCurrentPage] = useState(1);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/comment/IdsOfPost`,
      params: { id: id, currentPage: currentPage },
    })
      .then((res) => {
        setData({
          ...data,
          totalPage: res.data.totalPage,
          totalResult: res.data.totalResult,
          list: res.data.list,
        });
        props.setCmtCount(res.data.totalResult);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(props.id);
  }, [props.id]);
  useEffect(() => {
    fetchData(props.id);
  }, [currentPage]);

  return (
    <div className="flex flex-col gap-y-[25px]">
      {data.list.length > 0 &&
        data.list.map((item, index) => {
          return (
            <PostCmtItem
              key={index}
              id={item}
              index={index}
              length={data.list.length}
              className={`${
                index + 1 !== data.list.length && "border-b border-gray-200"
              } `}
            />
          );
        })}
      {data.list.length > 0 && (
        <Pagination
          forcePage={currentPage}
          setCurrentPage={setCurrentPage}
          totalPage={data.totalPage}
          itemsPerPage={data.list.length}
        />
      )}
    </div>
  );
}

export default ProductCmt;
