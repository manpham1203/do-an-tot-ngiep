import React from "react";
import { useNavigate } from "react-router-dom";

function NotFound(props) {
  document.title = "Trang Không Tồn Tại";
  const navigate = useNavigate();
  return (
    <div className="w-[100%] h-screen flex justify-center items-center">
      <div className="flex flex-col justify-center items-center">
        <h1 className="text-[40px]">404 NotFound</h1>
        <h1>
          Địa chỉ bạn tìm kiếm không thuộc hệ thống website của chúng tôi.
        </h1>
        <button
          type="button"
          className="border-2 border-second text-second px-[20px] py-[10PX] mt-[20px]"
          onClick={() => navigate("/")}
        >
          VỀ TRANG CHỦ
        </button>
      </div>
    </div>
  );
}

export default NotFound;
