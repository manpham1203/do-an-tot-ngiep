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
  return (
    <div className="container mx-auto mt-[20px]">
      <div className="flex flex-row gap-x-[25px]">
        <div className="flex flex-col gap-y-[25px]">
          {data.map((item) => {
            return (
              <PostCard
                key={item.id}
                title={item.title}
                slug={item.slug}
                image={item.imageSrc}
                createdAt={item.createdAt}
                shortDescription={item.shortDescription}
                view={item.view}
              />
            );
          })}
        </div>
        <div className="w-[300px] hidden lg:block lg:flex-none">widget</div>
      </div>
    </div>
  );
}

export default Posts;
