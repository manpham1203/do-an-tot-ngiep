import React from "react";
import { Link } from "react-router-dom";
import postthumb from "../../assets/postthumb.jpg";
import * as moment from "moment";
import "moment/locale/nl";

function PostCard(props) {
  return (
    <div className="flex flex-col md:flex-row items-center gap-x-[40px] ">
      <Link to={`/tin-tuc/${props.slug}`} className="flex-none w-[376px] h-[235px] block">
        <img
          src={props.image}
          alt=""
          className="w-full h-full object-cover object-center"
        />
      </Link>
      <div className="grow-[1]">
        <div className="flex flex-row font-light gap-x-[20px]">
          <span>
            {moment(props.createdAt).format("DD") +
              "-" +
              moment(props.createdAt).format("MM") +
              "-" +
              moment(props.createdAt).format("yyyy")}
          </span>
          |<span>{props.view} lượt xem</span>|<span>2 comment</span>
        </div>
        <Link to={`/tin-tuc/${props.slug}`} className="font-bold text-[20px] hover:underline hover:underline-offset-4">
          {props.title}
        </Link>
        <p className="short-desc-postcard w-full flex-none mb-[10px]">
          {props.shortDescription}
        </p>
        <Link
          to={`/tin-tuc/${props.slug}`}
          className="italic hover:underline hover:underline-offset-4"
        >
          Đọc tiếp ...
        </Link>
      </div>
    </div>
  );
}

export default PostCard;
