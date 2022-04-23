import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import PostCard from "../../../components/Post/PostCard";

function Posts(props) {
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/Post/postcards`,
      params: { id: id },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, []);
  console.log(data);
  return (
    <div>
      {data.map((item) => {
        return <PostCard key={item.id} title={item.title} image={item.imageSrc} createdAt={item.createdAt} shortDescription={item.shortDescription} />;
      })}
    </div>
  );
}

export default Posts;
