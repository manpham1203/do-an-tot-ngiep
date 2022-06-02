import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../../../apis/api";

function Page(props) {
  const { slug } = useParams();
  const [data, setData] = useState();
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
  return <div className="container mx-auto">
      <div dangerouslySetInnerHTML={{ __html: data?.content }}></div>
  </div>;
}

export default Page;
