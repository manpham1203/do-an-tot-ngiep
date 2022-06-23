import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import api from "../../apis/api";
import { FaTimes } from "react-icons/fa";
import { setCloseadminViewCmt } from "../../redux/adminViewCmt/adminViewCmtActions";
import defaultuser from "../../assets/defaultuser.png";
import ShowStarCmt from "../ShowStar/ShowStarCmt";
import * as moment from "moment";
import "moment/locale/nl";
import { toast } from "react-toastify";

function AdminViewCmt(props) {
  const [content, setContent] = useState("");
  const dispatch = useDispatch();
  const { adminViewCmt, user } = useSelector((s) => s);
  const [data, setData] = useState();
  const [visible, setVisible] = useState(1);
  const showMore = () => {
    setVisible((prev) => prev + 3);
  };
  const handleRep = async () => {
    if (content === "") {
      return;
    }
    await api({
      method: "POST",
      url: `/comment/repcmtproduct`,
      params: { parentId: data.id, content: content, userId: user.id },
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Gửi phản hồi thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setContent("");
          fetchData(adminViewCmt.id);
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
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/comment/ProductCmtItem`,
      params: { id: id },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(adminViewCmt.id);
  }, [adminViewCmt.id]);
  return (
    <div className="flex  p-[30px] bg-third fixed w-[80%] h-[500px] rounded-xl top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] z-[2000]">
      <div className="w-fit absolute right-0 top-0">
        <FaTimes
          onClick={() => dispatch(setCloseadminViewCmt())}
          className="inline-block text-[30px] text-second cursor-pointer"
        />
      </div>
      <div className=" w-full h-full">
        <div className="overflow-auto w-full h-full flex flex-row gap-x-[20px] bg-white rounded-[8px] shadow-admin p-[10px]">
          <div className="w-[110px] h-fit flex flex-col">
            <img
              src={data?.imageSrc || defaultuser}
              alt=""
              className="h-full w-full object-cover object-center"
            />
            <h2 className="text-center my-[10px]">{data?.fullName}</h2>
            {data?.star != null && <ShowStarCmt star={data?.star} />}
          </div>
          <div className="flex-[1]">
            <p className="text-gray-400 font-light italic ">
              {moment(data?.createdAt).format("DD-MM-yyyy")}
            </p>
            <p className="min-h-[50px]">{data?.content}</p>
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
            <div className="mt-[30px]">
              {data?.children?.slice(0, visible).map((item) => {
                return (
                  <div
                    className="flex flex-row gap-x-[20px] mb-[20px]"
                    key={item.id}
                  >
                    <div className="w-[50px] h-fit">
                      <img
                        src={item.imageSrc || defaultuser}
                        alt=""
                        className="h-full w-[50px] object-cover object-center flex-none"
                      />
                    </div>
                    <div>
                      <p className="italic text-gray-400 font-light">
                        {moment(item.createdAt).format("DD-MM-yyyy")}
                      </p>
                      <h2 className="font-normal">{item.fullName}</h2>
                      <p>{item.content}</p>
                    </div>
                  </div>
                );
              })}
              {data?.children?.length < 3 ? null : visible >=
                data?.children?.length ? null : (
                <span
                  className="hover:underline underline-offset-4 cursor-pointer w-fit"
                  onClick={showMore}
                >
                  Xem thêm...
                </span>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default AdminViewCmt;
