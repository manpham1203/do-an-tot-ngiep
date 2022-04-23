import React, { useEffect, useReducer, useState } from "react";
import { FaRegEdit, FaRegTrashAlt } from "react-icons/fa";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import Select, { StylesConfig } from "react-select";

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
const dot = (color = "transparent") => ({
  alignItems: "center",
  display: "flex",

  ":before": {
    backgroundColor: color,
    borderRadius: 10,
    content: '" "',
    display: "block",
    marginRight: 8,
    height: 10,
    width: 10,
  },
});
const colourStyles = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: data.color,
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
    color: data.color,
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
    color: data.color,
  }),
  singleValue: (styles, { data }) => {
    return {
      ...styles,
      color: data.color,
    };
  },
};

function Row(props) {
  const [state, dispatch] = useReducer(reducer, initState);
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
          (item) => item.value === res.data.status && setOrderStatus(item)
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
    await api({
      method: "PUT",
      url: `/Order/changestatus/${props.id}`,
      params: { status: e.value },
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Thay đổi trạng thái đơn hàng thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setOrderStatus(e);
          fetchData(props.id);
        }
      })
      .catch(() => dispatch(fail()));
  };

  return (
    <tr>
      <td className="sticky left-0 px-4 py-2 ">
        <input
          className="w-5 h-5 border-gray-200 rounded"
          type="checkbox"
          id="row_1"
        />
      </td>
      <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
        {state.data.id}
      </td>
      <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
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
      </td>
      <td className="px-4 py-2 text-gray-700 whitespace-nowrap text-[25px]">
        <div className="flex flex-row items-center gap-x-[20px]">
          <FaRegEdit
            onClick={() => handleEdit(state.data.id)}
            className="cursor-pointer"
          />
        </div>
      </td>
    </tr>
  );
}

export default Row;
