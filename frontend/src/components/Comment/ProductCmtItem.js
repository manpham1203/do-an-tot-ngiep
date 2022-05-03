import React, { useEffect, useState } from "react";
import api from "../../apis/api";

import * as moment from "moment";
import "moment/locale/nl";
import defaultuser from "../../assets/defaultuser.png";
import ShowStarCmt from "../ShowStar/ShowStarCmt";
import { toast } from "react-toastify";
import { useSelector } from "react-redux";
import ProductCmtChildren from "./ProductCmtChildren";

function ProductCmtItem(props) {
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/comment/ProductCmtItem`,
      params: { id: id },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(props.id);
  }, [props.id]);

  const [openRep, setOpenRep] = useState(null);
  const [content, setContent] = useState("");
  const [openRepList, setOpenRepList] = useState(null);

  const { user } = useSelector((state) => state);
  const handleRep = async () => {
    if (content === "") {
      return;
    }
    await api({
      method: "POST",
      url: `/comment/repcmtproduct`,
      params: { parentId: props.id, content: content, userId: user.id },
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Gửi phản hồi thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(props.id);
          setOpenRep(null);
          setOpenRepList(props.id);
          setContent("");
        } else {
          toast.error(`Gửi phản hồi thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Gửi phản hồi thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  console.log("u", user.id);
  return (
    <div className={`flex flex-row gap-x-[20px] ${props.className} pb-[20px]`}>
      <div className="w-[150px] flex flex-col items-center gap-y-[10px]">
        <div className="w-[100px] h-[100px]">
          <img
            src={data.imageSrc || defaultuser}
            alt=""
            className="w-full h-full object-cover object-center "
          />
        </div>
        <h2 className="text-center">{data.fullName}</h2>
        {data?.star && <ShowStarCmt star={data.star} />}
      </div>

      <div className="font-light w-full flex flex-col">
        <div className="flex flex-row">
          <div className="flex-[1]">
            <p className="italic text-gray-400 mb-[5px]">
              {moment(data.createdAt).format("DD-MM-yyyy")}
            </p>
            {data.content === null ? (
              <p className="italic">"Không có nội dung"</p>
            ) : (
              <p>{data.content}</p>
            )}
          </div>
          <div className="w-fit" onClick={() => setOpenRep(props.id)}>
            Trả lời
          </div>
        </div>
        {openRep === props.id && (
          <div className="w-full relative mt-[50px]">
            <textarea
              value={content}
              onChange={(e) => setContent(e.target.value)}
              placeholder=" "
              className="h-[100px] py-[20px] form-input border border-input-border text-input-color font-normal rounded-[4px] w-[100%] px-[20px] transition-all duration-[0.25s] focus:border-second outline-none bg-third"
            />
            <label className="form-label absolute left-[20px] top-[20%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-input-label">
              Gửi phản hồi
            </label>
            <button
              className="bg-second px-[30px] h-[40px] text-third"
              onClick={handleRep}
            >
              Gửi
            </button>
          </div>
        )}
        {data?.children?.length > 0 && (
          <ProductCmtChildren
            data={data.children}
            openRepList={openRepList}
            setOpenRepList={setOpenRepList}
            id={props.id}
          />
        )}
      </div>
    </div>
  );
}

export default ProductCmtItem;
