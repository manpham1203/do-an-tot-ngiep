import React, { useEffect, useState } from "react";
import api from "../../apis/api";

import * as moment from "moment";
import "moment/locale/nl";
import defaultuser from "../../assets/defaultuser.png";
import ShowStarCmt from "../ShowStar/ShowStarCmt";
import { toast } from "react-toastify";
import { useSelector } from "react-redux";
import PostCmtChildren from "./PostCmtChildren";
import { useNavigate } from "react-router-dom";

function Abc() {
  const navigate = useNavigate();
  return (
    <div className="cursor-default">
      <h2>Bạn cần phải đăng nhập để thực hiện chức năng này</h2>
      <button
        className="hover:underline underline-offset-4 font-semibold"
        onClick={() => navigate("/dang-nhap")}
      >
        Tiến hành đăng nhập...
      </button>
    </div>
  );
}
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
  const [valid, setValid] = useState(false);

  const { user } = useSelector((state) => state);
  const handleRep = async () => {
    if (content === "") {
      setValid(true);
      return;
    }
    if (user.id === null) {
      toast.warn(<Abc />, {
        position: toast.POSITION.TOP_RIGHT,
        autoClose: 3000,
      });
      return;
    }
    setValid(false);
    await api({
      method: "POST",
      url: `/comment/repcmtpost`,
      params: { parentId: props.id, content: content, userId: user.id },
    })
      .then((res) => {
        if (res.status === 201) {
          setValid(false);
          toast.success(`Gửi phản hồi thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(props.id);
          setOpenRep(null);
          setOpenRepList(props.id);
          setContent("");
        } else {
          setValid(false);
          toast.error(`Gửi phản hồi thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() => {
        setValid(false);
        toast.error(`Gửi phản hồi thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        });
      });
  };
  useEffect(() => {
    if (content !== "") {
      setValid(false);
    }
  }, [content]);
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
          <div
            className="w-fit cursor-pointer"
            onClick={() => setOpenRep(props.id)}
          >
            Trả lời
          </div>
        </div>
        {openRep === props.id && (
          <div className="w-full relative mt-[50px]">
            <div className="w-full relative">
              <textarea
                value={content}
                onChange={(e) => setContent(e.target.value)}
                placeholder=" "
                className="h-[100px] py-[20px] form-input border border-second dark:border-third font-normal rounded-[4px] w-[100%] px-[20px] transition-all duration-[0.25s] outline-none bg-third dark:bg-darkMode"
              />
              <label className="form-label absolute left-[20px] top-[20%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-second dark:text-third bg-third dark:bg-darkMode">
                Gửi phản hồi
              </label>
            </div>

            {valid && <p className="text-red-500">Chưa nhập nội dung</p>}
            <button
              className="bg-second px-[30px] h-[40px] text-third dark:bg-third dark:text-second"
              onClick={handleRep}
            >
              Gửi
            </button>
          </div>
        )}
        {data?.children?.length > 0 && (
          <PostCmtChildren data={data.children} id={props.id} />
        )}
      </div>
    </div>
  );
}

export default ProductCmtItem;
