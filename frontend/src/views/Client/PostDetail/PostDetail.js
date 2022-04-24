import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../../../apis/api";
import * as moment from "moment";
import "moment/locale/nl";
import BrandWidget from "../../../components/Widget/BrandWidget";
import CategoryWidget from "../../../components/Widget/CategoryWidget";
import NewProductWidget from "../../../components/Widget/NewProductWidget";

function PostDetail(props) {
  const [data, setData] = useState({});
  const { slug } = useParams();
  const fetchData = async (slug) => {
    await api({
      method: "GET",
      url: `/Post/postdetail`,
      params: { slug: slug },
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
  return (
    <div className="container mx-auto mt-[20px]">
      <div className="flex flex-row gap-x-[25px]">
        <div className="grow-[1] overflow-hidden">
          <div className="w-full h-[420px]">
            <img
              src={data?.imageSrc}
              alt=""
              className="w-full h-full object-cover object-center"
            />
          </div>
          <div className="flex flex-row font-light gap-x-[20px] mt-[20px]">
            <span>
              {moment(data?.createdAt).format("DD") +
                "-" +
                moment(data?.createdAt).format("MM") +
                "-" +
                moment(data?.createdAt).format("yyyy")}
            </span>
            |<span>{data?.view} lượt xem</span>|<span>2 comment</span>
          </div>
          <h2 className="font-bold text-[25px]">{data?.title}</h2>
          <div
            className="text-justify flex flex-col gap-y-[25px] mt-[20px]"
            dangerouslySetInnerHTML={{ __html: data?.fullDescription }}
          ></div>
        </div>
        <div className="w-[350px] flex-none">
          <BrandWidget />
          <CategoryWidget />
          <NewProductWidget />
        </div>
      </div>
    </div>
  );
}

export default PostDetail;
