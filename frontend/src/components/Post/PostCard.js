import React from "react";
import { Link } from "react-router-dom";
import postthumb from "../../assets/postthumb.jpg";
import * as moment from "moment";
import "moment/locale/nl";

function PostCard(props) {
  return (
    <div className="container mx-auto mt-[20px] ">
      <div className="flex flex-row gap-x-[20px] ">
        <div className="grow-[1]">
          <div className="flex flex-row gap-x-[40px] ">
            <div className="flex-none w-[376px] h-[235px]">
              <img
                src={props.image}
                alt=""
                className="w-full h-full object-cover object-center"
              />
            </div>
            <div className="grow-[1] relative">
              <div className="flex flex-row font-light gap-x-[20px]">
                <span>{moment(props.createdAt).format('DD')+"-"+moment(props.createdAt).format('MM')+"-"+moment(props.createdAt).format('yyyy')}</span>|<span>2 comment</span>
              </div>
              <h2 className="font-bold text-[20px]">{props.title}</h2>
              <p className="short-desc-postcard w-full flex-none">
                {props.shortDescription}
              </p>
              {/* <Link to={`tin-tuc/${props.slug}`}>Đọc tiếp ...</Link> */}
            </div>
          </div>
        </div>
        <div className="w-[300px] flex-none"></div>
      </div>
    </div>
  );
}

export default PostCard;
