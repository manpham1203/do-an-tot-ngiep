import React, { useEffect, useState } from "react";
import Td from "../../../components/DataTable/Td";
import Tr from "../../../components/DataTable/Tr";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import { FaRegEdit, FaTrashRestoreAlt, FaTimes } from "react-icons/fa";
import { useNavigate } from "react-router-dom";

function RowTrash(props) {
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
  return (
    <Tr>
      <Td>
        <input
          className="w-5 h-5 border-gray-200 rounded"
          type="checkbox"
          id={data.id}
          value={data.id}
          onChange={props.handleProductSelect}
          checked={props.productSelect.some((x) => x === data.id)}
        />
      </Td>
      <Td>
        <img
          src={data.imageSrc}
          alt=""
          className="w-full h-full object-cover object-center"
        />
      </Td>
      <Td>
        <h2 className="productCard2Name">{data.title}</h2>
      </Td>
      <Td>
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
      </Td>
      <Td>
        <div className="flex flex-row items-center gap-x-[20px] text-[25px]">
          <FaRegEdit
            onClick={() => navigate(`/admin/tin-tuc/chinh-sua/${data.id}`)}
            className="cursor-pointer"
          />
          <FaTrashRestoreAlt
            onClick={() => props.handleTrash(data.id)}
            className="cursor-pointer"
          />
          <button
            onClick={() => props.handleDelete(data.id)}
            type="button"
            className="bg-danger text-white p-[2px] rounded-md"
          >
            <FaTimes />
          </button>
        </div>
      </Td>
    </Tr>
  );
}

export default RowTrash;
