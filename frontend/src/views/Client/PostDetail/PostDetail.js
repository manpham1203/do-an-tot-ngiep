import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../../../apis/api";
import * as moment from "moment";
import "moment/locale/nl";
import BrandWidget from "../../../components/Widget/BrandWidget";
import CategoryWidget from "../../../components/Widget/CategoryWidget";
import NewProductWidget from "../../../components/Widget/NewProductWidget";
import PostCmt from "../../../components/Comment/PostCmt";
import { toast } from "react-toastify";
import { useSelector } from "react-redux";
import PostSlideShow from "../../../components/PostSlideShow/PostSlideShow";

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
function PostDetail(props) {
  const [data, setData] = useState({});
  const [content, setContent] = useState("");
  const { user } = useSelector((state) => state);
  const [cmtCount, setCmtCount] = useState(0);
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
  const handleComment = async () => {
    if(user.id===null){
      toast.warn(<Abc />, {
        position: toast.POSITION.TOP_RIGHT,
        autoClose: 3000,
      });
      return;
    }
    const cmt = {
      userId: user.id,
      ObjectId: data.id,
      content: content,
    };
    await api({
      method: "POST",
      url: `/comment/createpostCmt`,
      data: cmt,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Gửi đánh giá thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        } else {
          toast.error(`Gửi đánh giá thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() => {
        toast.error(`Gửi đánh giá thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        });
      });
  };
  useEffect(() => {
    if (data?.id !== null) {
      var timer = setTimeout(async () => {
        await api({
          method: "PUT",
          url: `/post/increaseview`,
          params: { id: data.id },
        })
          .then((res) => {
            if (res.status === 200) {
              console.log("increase success");
            } else {
              console.log("increase fail");
            }
          })
          .catch(() => {
            console.log("increase fail");
          });
      }, 5000);
      return () => clearTimeout(timer);
    }
  }, [data.id]);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, [slug]);
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
            |<span>{data?.view} lượt xem</span>|<span>{cmtCount} comment</span>
          </div>
          <h2 className="font-bold text-[25px]">{data?.title}</h2>
          <div
            className="text-justify flex flex-col gap-y-[25px] mt-[20px]"
            dangerouslySetInnerHTML={{ __html: data?.fullDescription }}
          ></div>
          <h2 className="text-[25px] mb-[25px] mt-[25px]">Bình luận</h2>
          <div className="w-full relative mt-[50px] mb-[50px]">
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
              onClick={handleComment}
            >
              Gửi
            </button>
          </div>
          {data?.id && <PostCmt id={data.id} setCmtCount={setCmtCount} />}
          <PostSlideShow slideLg={3} slideMd={3} slideSm={1} slide={1} />
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
