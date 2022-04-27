import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import CommentItem from "./CommentItem";

function Comment(props) {
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/comment/IdsOfProduct`,
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

  return (
    <div className="flex flex-col gap-y-[25px]">
      {data.length > 0 &&
        data.map((item, index) => {
          return (
            <CommentItem
              key={index}
              id={item}
              index={index}
              length={data.length}
              className={`${
                index + 1 !== data.length && "border-b border-gray-200"
              } `}
            />
          );
        })}
    </div>
  );
}

export default Comment;
