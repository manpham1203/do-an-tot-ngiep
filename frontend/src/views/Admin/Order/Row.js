import React, { useEffect, useReducer, useState } from "react";
import { FaRegEdit, FaRegEye } from "react-icons/fa";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import Select, { StylesConfig } from "react-select";
import { setOpenAdminViewOrder } from "../../../redux/adminViewOrder/adminViewOrderActions";
import { cursorDefault, cursorWait } from "../../../redux/cursor/cursorActions";
import { useDispatch } from "react-redux";
import Tr from "../../../components/Table/Tr";
import Td from "../../../components/Table/Td";

const initState = {
  loading: false,
  fail: false,
  data: {},
};
//action
const LOADING = "LOADING";
const SUCCESS = "SUCCESS";
const FAIL = "FAIL";
const loading = () => {
  return {
    type: LOADING,
  };
};
const success = (payload) => {
  return {
    type: SUCCESS,
    payload: payload,
  };
};
const fail = () => {
  return {
    type: FAIL,
  };
};
const reducer = (state, action) => {
  switch (action.type) {
    case LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case SUCCESS:
      return {
        ...state,
        loading: false,
        fail: false,
        data: action.payload,
      };
    case FAIL:
      return {
        ...state,
        loading: false,
        fail: true,
      };
    default:
      return state;
  }
};
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
  const [state, dispatch] = useReducer(reducer, initState);
  const dispatchRedux = useDispatch();
  //   const statusOptions = [
  //       { value: 1, label: "Chờ xác nhận" },
  //       { value: 2, label: "Đang chuẩn bị hàng" },
  //       { value: 3, label: "Đang vận chuyển" },
  //       { value: 4, label: "Đã nhận hàng" },
  //       { value: 0, label: "Đã huỷ" },
  //   ];
  const [orderStatus, setOrderStatus] = useState({});
  const navigate = useNavigate();

  const fetchData = async (id) => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/Order/getbyid`,
      params: { id: id },
    })
      .then((res) => {
        dispatch(success(res.data));
        // setOrderStatus(orderOptions[res.data.status]);
        orderOptions.map(
          (item) => item.value === res.data.state && setOrderStatus(item)
        );
      })
      .catch(() => dispatch(fail()));
  };
  useEffect(() => {
    fetchData(props.id);
  }, [props.id]);

  const handleEdit = (id) => {
    // navigate(`/admin/don-hang/chinh-sua/${id}`);
  };

  const handleChangeStatus = async (e) => {
    dispatchRedux(cursorWait());
    await api({
      method: "PUT",
      url: `/Order/changestate/${props.id}`,
      params: { state: e.value },
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Thay đổi trạng thái đơn hàng thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setOrderStatus(e);
          fetchData(props.id);
          props.fetchData();
          dispatchRedux(cursorDefault());
        }
      })
      .catch(() => {
        dispatch(fail());
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
      <Td>
        <div className="w-full flex justify-center">
          <input
            className="w-5 h-5 border-gray-200 rounded"
            type="checkbox"
            id="row_1"
          />
        </div>
      </Td>
      <Td className="px-[20px]">{state.data.id}</Td>
      <Td>
        <div className="w-full flex justify-center">
          <Select
            className="cursor-pointer "
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
        <div className="flex flex-row justify-center text-[25px] gap-x-[20px]">
          <FaRegEye
            onClick={() => handleQuickView()}
            className="cursor-pointer"
          />
          <FaRegEdit
            onClick={() => handleEdit(state.data.id)}
            className="cursor-pointer"
          />
        </div>
      </Td>
    </Tr>
  );
}

export default Row;
