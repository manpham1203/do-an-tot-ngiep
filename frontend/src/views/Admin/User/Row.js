import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import Tr from "../../../components/Table/Tr";
import Td from "../../../components/Table/Td";
import { FaRegEdit, FaRegTrashAlt, FaRegEye } from "react-icons/fa";
import defaultuser from "../../../assets/defaultuser.png";
import { toast } from "react-toastify";
import { useDispatch } from "react-redux";
import { setOpenAdminViewUser } from "../../../redux/adminViewUser/adminViewUserActions";
import { useNavigate } from "react-router-dom";

function Row(props) {
  const [data, setData] = useState();
  const navigate=useNavigate();
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/user/userdetail`,
      params: {
        id: id,
      },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(props.id);
  }, [props.id]);
  const handlePublished = async (id) => {
    await api({
      method: "PUT",
      url: `/user/published`,
      params:{
          id:id
      }
    })
      .then((res) => {
        if (res.status === 200) {
            console.log("vo day")
          fetchData(id);
        } else {
          toast.error(`Thao tác thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thao tác thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const dispatchQV = useDispatch();
  const handleQuickView = () => {
    const obj = {
      show: true,
      id: props.id,
      data:data
    };
    dispatchQV(setOpenAdminViewUser(obj));
  };
  return (
    <Tr>
      <Td style={{ height: "50px" }}>
        <div className="w-full flex justify-center">
          <input
            className="w-5 h-5 border-gray-200 rounded"
            type="checkbox"
            disabled
          />
        </div>
      </Td>
      <Td style={{ height: "50px" }}>
        <img
          src={data?.imageSrc || defaultuser}
          alt=""
          className="w-full h-full object-cover object-center"
        />
      </Td>
      <Td style={{ height: "50px" }}>
        <h2 className="short-desc-postcard2 px-[20px]">{data?.lastName+" "+data?.firstName}</h2>
      </Td>
      <Td className="w-[150px]" style={{ height: "50px" }}>
        <div className="w-full flex justify-center">
          <div
            className={`w-[50px] h-[25px]  flex items-center rounded-full relative
                  ${data?.published ? "bg-blue-600 " : "bg-gray-300"}
                  transition-all duration-200 cursor-pointer
                  `}
            onClick={() => handlePublished(data?.id)}
          >
            <div
              className={`w-[18px] h-[18px] bg-white rounded-full  absolute
                    ${data?.published ? "ml-[28px]" : "ml-[4px]"}
                    transition-all duration-200
                    `}
            ></div>
          </div>
        </div>
      </Td>
      <Td className="w-[200px]" style={{ height: "50px" }}>
        <div className="w-full flex justify-center gap-x-[20px] text-[25px]">
          <FaRegEye
            onClick={() => handleQuickView()}
            className="cursor-pointer"
          />
          {/* <FaRegEdit
            onClick={() => navigate(`/admin/nguoi-dung/chinh-sua/${data.id}`)}
            className="cursor-pointer"
          /> */}
          {/* <FaRegTrashAlt
            onClick={() => props.handleTrash(data.id)}
            className="cursor-pointer"
          /> */}
        </div>
      </Td>
    </Tr>
  );
}

export default Row;
