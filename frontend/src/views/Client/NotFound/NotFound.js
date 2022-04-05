import React from "react";
import { useNavigate } from "react-router-dom";

function NotFound(props) {
  document.title="Trang Không Tồn Tại";
    const navigate=useNavigate();
  return (
    <div className="w-screen h-screen flex justify-center items-center">
      <div className="container h-[500px] rounded-md shadow-lg flex flex-col justify-center items-center text-[50px]">
        <h1>404</h1>
        <h1>NotFound</h1>
        <h1 className="cursor-pointer" onClick={() => navigate("/")}>Go Back Home</h1>
      </div>
    </div>
  );
}

export default NotFound;
