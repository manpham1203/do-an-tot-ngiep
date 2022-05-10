import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../Pagination/Pagination";
import Row from "./Row";
import Select from "react-select";
import Table from "../../Table/Table";
import Thead from "../../Table/Thead";
import Tr from "../../Table/Tr";
import Th from "../../Table/Th";
import Tbody from "../../Table/Tbody";

const dateOptions = [
  { value: 1, label: "Sinh nhật vừa qua" },
  { value: 2, label: "Sinh nhật hôm nay" },
  { value: 3, label: "Sinh nhật sắp tới" },
];
const colourStyles = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    left: "0%",
    lineHeight: "1.3rem",
    marginLeft: "0rem",
  }),
  control: () => ({
    color: "#202121",
    display: "flex",
    border: "1px solid #202121",
    height: "30px",
    width: "250px",
    borderRadius: "0px",
    background: "#fcfcfc",
    fontSize: "14px",
  }),
  option: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: "#202121",
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
  }),
  menu: (styles) => ({
    ...styles,
    borderRadius: 0,
    width: "250px",
    border: "1px solid #202121",
    paddingTop: 0,
  }),
};
function CustomerBirthday(props) {
  const [data, setData] = useState([]);
  const [limit, setLimit] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);
  const [dateType, setDateType] = useState(null);

  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/user/GetListBirthday`,
      //   params: {
      //     currentPage: currentPage,
      //     limit: limit,
      //     type: dateType.value,
      //   },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, [currentPage, limit, dateType]);
  return (
    <div className="w-full p-[20px] bg-white shadow-admin rounded-[8px]">
      <div className="flex flex-row justify-between items-center  mb-[20px]">
        <h2 className="text-[20px]">Khách Hàng Có Sinh Nhật Gần Đây</h2>
        <div className="flex flex-row gap-x-[20px] items-center">
          <span>Lọc Sinh Nhật: </span>
          <Select
            className=" cursor-pointer "
            classNamePrefix="select"
            // defaultValue={orderOptions[0]}
            isClearable={true}
            isSearchable={false}
            name="orderStatus"
            value={dateType}
            onChange={(e) => setDateType(e)}
            options={dateOptions}
            styles={colourStyles}
            placeholder="Lọc sinh nhật"
          />
        </div>
      </div>

      <Table className="w-full">
        <Thead>
          <Tr>
            <Th>Tên khách hàng</Th>
            <Th>Ngày sinh</Th>
            <Th>Hành động</Th>
          </Tr>
        </Thead>
        <Tbody>
          {data?.map((item) => {
            return <Row key={item.id} id={item.id} />;
          })}
        </Tbody>
      </Table>
    </div>
  );
}

export default CustomerBirthday;
