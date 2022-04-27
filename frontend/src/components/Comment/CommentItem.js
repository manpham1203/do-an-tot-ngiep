import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import { AiFillStar } from "react-icons/ai";
import * as moment from "moment";
import "moment/locale/nl";

function CommentItem(props) {
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/comment/ProductCmtItem`,
      params: { id: id },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(props.id);
  }, [props.id]);

  console.log(data);
  return (
    <div className={`flex flex-row gap-x-[20px] ${props.className} pb-[20px]`}>
      <div className="w-[150px] flex flex-col justify-center items-center gap-y-[10px]">
        <div className="w-[100px] h-[100px]">
          <img
            src=""
            alt=""
            className="w-full h-full object-cover object-center "
          />
        </div>
        <h2 className="text-center">Tên</h2>
        {data?.star && data.star === 1 ? (
          <div className="flex flex-row items-center text-gray-400 justify-center">
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar />
            <AiFillStar />
            <AiFillStar />
            <AiFillStar />
          </div>
        ) : data.star === 2 ? (
          <div className="flex flex-row items-center text-gray-400 justify-center">
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar />
            <AiFillStar />
            <AiFillStar />
          </div>
        ) : data.star === 3 ? (
          <div className="flex flex-row items-center text-gray-400 justify-center">
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar />
            <AiFillStar />
          </div>
        ) : data.star === 4 ? (
          <div className="flex flex-row items-center text-gray-400 justify-center">
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar />
          </div>
        ) : (
          <div className="flex flex-row items-center text-gray-400 justify-center">
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
            <AiFillStar className="text-[#F7BF63]" />
          </div>
        )}
      </div>

      <div className="font-light">
        <p className="italic text-gray-400 mb-[5px]">{moment(data.createdAt).startOf('hour').fromNow()}</p>
        <p>{data.content}</p>
      </div>
    </div>
  );
}

export default CommentItem;
