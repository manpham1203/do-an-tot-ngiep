import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../../../apis/api";
import PageContent from "../../../components/Skeleton/PageContent";

function Page(props) {
  const { slug } = useParams();
  const [data, setData] = useState();
  document.title = data?.title || "Trang";
  const [loading, setLoading] = useState(true);
  const fetchData = async (s) => {
    await api({
      method: "GET",
      url: `/page/GetBySlug/${s}`,
      data: null,
    })
      .then((res) => {
        if (res.status === 200) {
          setLoading(false);
          setData(res.data);
        }
      })
      .catch(() => setLoading(true));
  };
  useEffect(() => {
    fetchData(slug);
    window.scrollTo(0, 0);
  }, [slug]);
  return loading ? (
    <PageContent />
  ) : (
    <div className="container px-[10px] sm:px-[20px] mx-auto text-second dark:text-third">
      <div dangerouslySetInnerHTML={{ __html: data?.content }}></div>
    </div>
  );
}

export default Page;
