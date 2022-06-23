import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import api from "../../../apis/api";
import PageContent from "../../../components/Skeleton/PageContent";

function About(props) {
  document.title = "Giới Thiệu";
  const [data, setData] = useState();
  const [loading, setLoading] = useState(true);
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/page/GetBySlug/gioi-thieu`,
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
    fetchData();
  }, []);

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return loading ? (
    <PageContent />
  ) : (
    <div className="container px-[10px] sm:px-[20px] mx-auto mt-[20px] text-second dark:text-third">
      <div dangerouslySetInnerHTML={{ __html: data?.content }}></div>
    </div>
  );
}

export default About;
