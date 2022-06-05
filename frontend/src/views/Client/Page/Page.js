import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../../../apis/api";

function Page(props) {
  const { slug } = useParams();
  const [data, setData] = useState();
  document.title = data?.title || "Trang";
  const fetchData = async (s) => {
    await api({
      method: "GET",
      url: `/page/GetBySlug/${s}`,
      data: null,
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(slug);
  }, [slug]);
  console.log(data);
  return <div className="container px-[10px] sm:px-[20px] mx-auto">
      <div dangerouslySetInnerHTML={{ __html: data?.content }}></div>
  </div>;
}

export default Page;
