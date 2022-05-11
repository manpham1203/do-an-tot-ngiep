import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import { FaRegEdit, FaRegTrashAlt, FaRegEye } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import Tr from "../../../components/Table/Tr";
import Td from "../../../components/Table/Td";
import { setOpenAdminViewPost } from "../../../redux/adminViewPost/adminViewPostActions";
import { useDispatch } from "react-redux";

function Row(props) {
  const [data, setData] = useState({});
  const navigate = useNavigate();
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/Post/RowAdminById`,
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
  const handlePublished = async (id) => {
    await api({
      method: "PUT",
      url: `/post/published/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
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
    };
    dispatchQV(setOpenAdminViewPost(obj));
  };
  return (
    <Tr>
      <Td style={{ height: "60px" }}>
        <div className="w-full flex justify-center">
          <input
            className="w-5 h-5 border-gray-200 rounded"
            type="checkbox"
            id={data.id}
            value={data.id}
            onChange={props.handleProductSelect}
            checked={props.productSelect.some((x) => x === data.id)}
          />
        </div>
      </Td>
      <Td style={{ height: "60px" }}>
        <img
          src={data.imageSrc}
          alt=""
          className="w-full h-full object-cover object-center"
        />
      </Td>
      <Td style={{ height: "60px" }}>
        <h2 className="short-desc-postcard2 px-[20px]">{data.title}</h2>
      </Td>
      <Td className="w-[150px]" style={{ height: "60px" }}>
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
      <Td className="w-[200px]" style={{ height: "60px" }}>
        <div className="w-full flex justify-center gap-x-[20px] text-[25px]">
          <FaRegEye
            onClick={() => handleQuickView()}
            className="cursor-pointer"
          />
          <FaRegEdit
            onClick={() => navigate(`/admin/tin-tuc/chinh-sua/${data.id}`)}
            className="cursor-pointer"
          />
          <FaRegTrashAlt
            onClick={() => props.handleTrash(data.id)}
            className="cursor-pointer"
          />
        </div>
      </Td>
    </Tr>
  );
}

export default Row;
