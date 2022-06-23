import React, { useEffect, useState } from "react";
import { setOpenAdminViewOrder } from "../../../redux/adminViewOrder/adminViewOrderActions";
import { useDispatch } from "react-redux";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import Select from "react-select";
import { FaRegEdit, FaRegEye } from "react-icons/fa";
import Tr from "../../Table/Tr";
import Td from "../../Table/Td";
import { cursorDefault, cursorWait } from "../../../redux/cursor/cursorActions";

const orderOptions = [
  { value: 1, label: "Chờ xác nhận", color: "#2F4B60" },
  { value: 2, label: "Đang chuẩn bị hàng", color: "#2DCCBF" },
  { value: 3, label: "Đang vận chuyển", color: "#1A9487" },
  { value: 4, label: "Đã nhận hàng", color: "#9EBC4B" },
  { value: 0, label: "Đã huỷ", color: "#ED553B" },
];

const colourStyles = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: data?.color,
    borderLeft: "0.1rem solid black",
    left: "0%",
    lineHeight: "1.3rem",
    paddingLeft: "0.5rem",
    marginLeft: "0rem",
  }),
  control: () => ({
    display: "flex",
    border: "1px solid #202121",
    height: "30px",
    width: "200px",
    borderRadius: "0px",
    background: "#fcfcfc",
    fontSize: "14px",
  }),
  option: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    fontSize: "14px",
    textAlign: "left",
    background: "white",
    borderBottom: "0.1rem solid #103D56",
    ":last-of-type": {
      borderBottom: "none",
    },
    ":hover": {
      background: "#f7f7f7",
      cursor: "pointer",
    },
    color: data?.color,
  }),
  menu: (styles) => ({
    ...styles,
    borderRadius: 0,
    width: "200px",
    border: "1px solid #202121",
    paddingTop: 0,
  }),
  input: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: data?.color,
  }),
  singleValue: (styles, { data }) => {
    return {
      ...styles,
      color: data?.color,
    };
  },
};

function Row(props) {
  const [data, setData] = useState();
  const dispatchRedux = useDispatch();
  const [orderStatus, setOrderStatus] = useState({});

  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/Order/getbyid`,
      params: { id: id },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
          orderOptions.map(
            (item) => item.value === res.data.state && setOrderStatus(item)
          );
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(props.id);
  }, [props.id]);

  const handleChangeStatus = async (e) => {
    dispatchRedux(cursorWait());
    await api({
      method: "PUT",
      url: `/Order/changestate/${props.id}`,
      params: { state: e.value },
    })
      .then((res) => {
        if (res.status === 200) {
          dispatchRedux(cursorDefault());
          toast.success(`Thay đổi trạng thái đơn hàng thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setOrderStatus(e);
          fetchData(props.id);
          props.fetchData();
        }
      })
      .catch(() => {
        console.log("fail");
        dispatchRedux(cursorDefault());
      });
  };
  const handleQuickView = () => {
    const obj = {
      show: true,
      id: props.id,
    };
    dispatchRedux(setOpenAdminViewOrder(obj));
  };
  return (
    <Tr>
      <Td className="max-w-[200px]">
        <div className="truncate w-full pl-[20px]">{data?.id}</div>
      </Td>
      <Td className="h-[40px]">
        <div className="w-full flex justify-center">
          <Select
            className="w-[200px] cursor-pointer "
            classNamePrefix="select"
            // defaultValue={orderOptions[0]}
            isClearable={false}
            isSearchable={false}
            name="orderStatus"
            value={orderStatus}
            onChange={(e) => handleChangeStatus(e)}
            options={orderOptions}
            styles={colourStyles}
          />
        </div>
      </Td>
      <Td>
        <div className="flex flex-row justify-center text-[25px] gap-x-[20px] w-full">
          <FaRegEye
            onClick={() => handleQuickView()}
            className="cursor-pointer"
          />
        </div>
      </Td>
    </Tr>
  );
}

export default Row;
