import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import * as moment from "moment";
import "moment/locale/nl";
import defaultuser from "../../assets/defaultuser.png";

function ProductCmtChildren(props) {
  const [visible, setVisible] = useState(1);
  const showMore = () => {
    setVisible((prev) => prev + 3);
  };
  return (
    <div className="">
      <div className="border-b border-gray-200 my-[50px]"></div>
      <div className="flex flex-col gap-y-[20px] mb-[20px]">
        {props?.data?.slice(0, visible).map((item) => {
          return (
            <div className="flex flex-row gap-x-[20px]" key={item.id}>
              <div className="w-[50px] h-[50px] border border-gray-600">
                <img
                  src={item.imageSrc || defaultuser}
                  alt=""
                  className="h-full w-full object-cover object-center"
                />
              </div>
              <div>
                <h2 className="font-normal flex-[1]">{item.fullName}</h2>
                <p className="italic text-gray-400">
                  {moment(item.createdAt).format("DD-MM-yyyy")}
                </p>
                <p>{item.content}</p>
              </div>
            </div>
          );
        })}
        {props.data.length < 2 ? null : visible >= props.data.length ? null : (
          <span
            className="hover:underline underline-offset-4 cursor-pointer w-fit"
            onClick={showMore}
          >
            Xem thêm...
          </span>
        )}
      </div>
    </div>
  );
}

export default ProductCmtChildren;
