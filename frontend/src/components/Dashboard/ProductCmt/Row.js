import React, { useEffect, useState } from "react";
import { setOpenadminViewCmt } from "../../../redux/adminViewCmt/adminViewCmtActions";
import { useDispatch, useSelector } from "react-redux";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import Select from "react-select";
import { FaRegEdit, FaRegEye } from "react-icons/fa";
import Tr from "../../Table/Tr";
import Td from "../../Table/Td";
import defaultuser from "../../../assets/defaultuser.png";
import ShowStarCmt from "../../ShowStar/ShowStarCmt";

function Row(props) {
  const [data, setData] = useState();
  const dispatchQV = useDispatch();
  const [orderStatus, setOrderStatus] = useState({});
  const {adminViewCmt}=useSelector(store=>store);

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
    fetchData(props.id);
  }, [props.id]);
  const handleQuickView = () => {
    const obj = {
      show: true,
      data: data,
    };
    dispatchQV(setOpenadminViewCmt(obj));
  };
  // useEffect(()=>{
  //   const obj = {
  //     data: data,
  //   };
  //   dispatchQV(setCmtData(obj));
  // }, [data])
  useEffect(()=>{
    fetchData(props.id);
  },[adminViewCmt])
  return (
    <Tr>
      <Td className="py-[10px]">
        <div className="w-full flex justify-center">
          <div className="w-[60px] h-[60px]">
            <img
              src={data?.imageSrc || defaultuser}
              alt=""
              className="w-full h-full object-cover object-center"
            />
          </div>
        </div>
      </Td>
      <Td className="full">
        <div className="w-full px-[20px] short-desc-postcard2">
          {data?.content}
        </div>
      </Td>
      <Td>
        <ShowStarCmt star={data?.star} />
      </Td>
      <Td>
        <div className="flex flex-row justify-center text-[25px] gap-x-[20px] w-full">
          <FaRegEye
            onClick={() => handleQuickView()}
            className="cursor-pointer"
          />
          <FaRegEdit
            // onClick={() => handleEdit(state.data.id)}
            className="cursor-pointer"
          />
        </div>
      </Td>
    </Tr>
  );
}

export default Row;
