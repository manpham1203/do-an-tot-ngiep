import React, { useState, useEffect } from "react";
import { FaTimes } from "react-icons/fa";
import api from "../../apis/api";
import { toast } from "react-toastify";
import RatingStar from "../RatingStar/RatingStar";

function ProductCmt(props) {
  const [star, setStar] = useState(0);
  const [error, setError] = useState(false);
  const [content, setContent] = useState("");

  const handleSubmit = async () => {
    if (star < 1 || star > 5) {
      setError(true);
      return;
    }
    const data = {
      userId: props.modalData.userId,
      ObjectId: props.modalData.ObjectId,
      OrderDetailId: props.modalData.OrderDetailId,
      star: star,
      content: content,
    };
    await api({
      method: "POST",
      url: `/comment/createProductCmt`,
      data: data,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Gửi đánh giá thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          props.setOpenModal(false);
          props.setFetchDetail(props.modalData.OrderDetailId);
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
    if (star === 1 || star === 2 || star === 3 || star === 4 || star === 5) {
      setError(false);
    }
  }, [star]);

  console.log(star);

  return (
    <div className="fixed w-[600px] h-[300px] top-[150px] z-[100] bg-white rounded-xl shadow-admin left-[50%] translate-x-[-50%]">
      <div className="w-full text-right text-[30px]">
        <button onClick={() => props.setOpenModal(false)} className="m-[10px]">
          <FaTimes />
        </button>
      </div>
      <div className="flex flex-col justify-center items-center ">
        <RatingStar setStar={setStar} />
        <p className="mb-[10px] font-light h-[24px] text-red-500">
          {error && "Bạn chưa chọn số sao cần đánh giá"}
        </p>
        <div className="w-[90%] h-[100px] flex flex-col">
          <label htmlFor="content">Nội dung đánh giá (không bắt buộc):</label>
          <textarea
            onChange={(e) => setContent(e.target.value)}
            value={content}
            name=""
            id="content"
            className="border border-gray-400 rounded-md w-[100%] h-[100px] resize-none p-[10px]"
          ></textarea>
        </div>

        <button
          className="bg-second text-third px-[20px] h-[40px] mt-[20px]"
          onClick={handleSubmit}
        >
          GỬI ĐÁNH GIÁ
        </button>
      </div>
    </div>
  );
}

export default ProductCmt;
