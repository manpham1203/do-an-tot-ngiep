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
import MostBoughtWidget from "../../../components/Widget/MostBoughtWidget";
import OnSaleWidget from "../../../components/Widget/OnSaleWidget";
import PageContent from "../../../components/Skeleton/PageContent";

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
  document.title = "Tin tức: " + data?.title || "Tin tức";
  const [content, setContent] = useState("");
  const [valid, setValid] = useState(false);
  const { user } = useSelector((state) => state);
  const [cmtCount, setCmtCount] = useState(0);
  const { slug } = useParams();
  const [loadCmt, setLoadCmt] = useState(true);
  const [loading, setLoading] = useState(true);
  const fetchData = async (slug) => {
    await api({
      method: "GET",
      url: `/Post/postdetail`,
      params: { slug: slug },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
          setLoading(false);
          setLoadCmt(false);
        }
      })
      .catch(() => {
        setLoading(true);
        setLoadCmt(true);
      });
  };
  useEffect(() => {
    fetchData(slug);
  }, [slug]);
  const handleComment = async () => {
    setLoadCmt(true);
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
          setValid(false);
          toast.success(`Gửi đánh giá thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          setLoadCmt(false);
          setContent("")
        } else {
          setValid(false);
          setLoadCmt(false);
          toast.error(`Gửi đánh giá thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() => {
        setValid(false);
        setLoadCmt(false);
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
  useEffect(() => {
    if (content !== "") {
      setValid(false);
    }
  }, [content]);
  return loading ? (
    <PageContent />
  ) : (
    <div className="container px-[10px] sm:px-[20px] mx-auto mt-[20px] text-second dark:text-third">
      <div className="flex flex-row gap-x-[25px]">
        <div className="w-full overflow-hidden">
          <div className="w-full h-[200px] sm:h-[300px] lg:h-[420px]">
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
          <h2 className="font-bold text-[20px] md:text-[22px] lg:text-[25px]">
            {data?.title}
          </h2>
          <div
            className="text-justify flex flex-col gap-y-[25px] mt-[20px]"
            dangerouslySetInnerHTML={{ __html: data?.fullDescription }}
          ></div>
          <h2 className="text-[25px] mb-[25px] mt-[25px]">Bình luận</h2>
          <div className="w-full relative mt-[50px] mb-[50px]">
            <div className="w-full relative">
              <textarea
                value={content}
                onChange={(e) => setContent(e.target.value)}
                placeholder=" "
                className="h-[100px] py-[20px] form-input border border-second dark:border-third text-second dark:text-third font-normal rounded-[4px] w-[100%] px-[20px] transition-all duration-[0.25s] focus:border-second outline-none bg-third dark:bg-darkMode"
              />
              <label className="form-label absolute left-[20px] top-[20%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-second dark:text-third bg-third dark:bg-darkMode">
                Gửi phản hồi
              </label>
            </div>

            {valid && <p className="text-red-500">Chưa nhập nội dung</p>}
            <button
              className="bg-second px-[30px] h-[40px] text-third"
              onClick={handleComment}
            >
              Gửi
            </button>
          </div>
          {data?.id && loadCmt ? (
            ""
          ) : (
            <PostCmt id={data.id} setCmtCount={setCmtCount} />
          )}
          <PostSlideShow slideLg={3} slideMd={3} slideSm={1} slide={1} />
        </div>
        <div className="w-[300px] xl:w-[350px] hidden lg:block flex-none">
          <BrandWidget />
          <CategoryWidget />
          {/* <NewProductWidget /> */}
          <MostBoughtWidget />
          <OnSaleWidget />
        </div>
      </div>
    </div>
  );
}

export default PostDetail;
