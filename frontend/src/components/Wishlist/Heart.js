import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import { toast } from "react-toastify";
import { useSelector } from "react-redux";
import { AiFillStar, AiFillHeart } from "react-icons/ai";


function Heart(props) {
  const [data, setData] = useState(false);
  const { user } = useSelector((store) => store);

  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/Wishlist`,
      params: { userId: user.id, productId: props.id },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
        }
      })
      .catch(() => console.log("fail"));
  };
  const create = async () => {
    await api({
      method: "POST",
      url: `/Wishlist`,
      params: { userId: user.id, productId: props.id },
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Thêm vào danh sách yêu thích thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          fetchData();
        } else {
          toast.error(`Thêm vào danh sách yêu thích thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thêm vào danh sách yêu thích thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        })
      );
  };
  const deleteWishlist = async () => {
    await api({
      method: "DELETE",
      url: `/Wishlist`,
      params: { userId: user.id, productId: props.id },
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Xoá khỏi danh sách yêu thích thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          fetchData();
        } else {
          toast.error(`Xoá khỏi danh sách yêu thích thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() =>
        toast.error(`Xoá khỏi danh sách yêu thích thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        })
      );
  };
  useEffect(() => {
    fetchData();
  }, [props.id]);
  return (
    <>
      {data && (
        <div
          onClick={() => deleteWishlist()}
          className="flex justify-center items-center w-[40px] h-[40px] text-red-600 hover:bg-white bg-gray-50 rounded-[500px] cursor-pointer shadow-md"
        >
          <AiFillHeart />
        </div>
      )}
      {!data && (
        <div
          onClick={() => create()}
          className="flex justify-center items-center w-[40px] h-[40px] text-gray-400 hover:bg-white bg-gray-50 rounded-[500px] cursor-pointer shadow-md"
        >
          <AiFillHeart />
        </div>
      )}
    </>
  );
}

export default Heart;
