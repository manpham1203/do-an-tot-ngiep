import React from "react";
import * as moment from "moment";
import "moment/locale/nl";
import { Link } from "react-router-dom";

function PostCard2(props) {
  return (
    <div className="w-full flex flex-col gap-y-[10px] bg-white rounded-xl shadow-md overflow-hidden hover:shadow-xl p-[10px]">
      <Link to={`/tin-tuc/${props.slug}`} className="w-full block h-[186px]">
        <img
          src={props.image}
          alt=""
          className="w-full h-full object-cover object-center rounded-xl"
        />
      </Link>
      <div className="flex flex-row font-light gap-x-[10px] text-[15px">
        <span>{props.createdAt}</span>|
        <span>{props.view} lượt xem</span>
      </div>
      <Link to={`/tin-tuc/${props.slug}`} className="short-desc-postcard2 font-medium text-[18px] hover:underline hover:underline-offset-4 w-fit">{props.title}</Link>
    </div>
  );
}

export default PostCard2;
