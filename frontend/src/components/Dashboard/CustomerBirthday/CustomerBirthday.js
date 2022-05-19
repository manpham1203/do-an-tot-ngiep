import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../Pagination/Pagination";
import Select from "react-select";
import Table from "../../Table/Table";
import Thead from "../../Table/Thead";
import Tr from "../../Table/Tr";
import Th from "../../Table/Th";
import Tbody from "../../Table/Tbody";
import Td from "../../Table/Td";
import * as moment from "moment";
import "moment/locale/nl";
import { BsPlusLg, BsDashLg } from "react-icons/bs";

const dateOptions = [
  { value: 1, label: "Sinh nhật vừa qua" },
  { value: 2, label: "Sinh nhật hôm nay" },
  { value: 3, label: "Sinh nhật sắp tới" },
];
const colourStyles = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: "#202121",
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
  const [data, setData] = useState({ totalPage: 0, totalResult: 0, data: [] });
  const [currentPage, setCurrentPage] = useState(1);
  const [dateType, setDateType] = useState(null);

  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/user/GetListBirthday`,
      params: {
        currentPage: currentPage,
        type: dateType?.value,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          setData({
            ...data,
            totalPage: res.data.totalPage,
            totalResult: res.data.totalResult,
            data: res.data.data,
          });
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, [currentPage, dateType]);
  const [tab, setTab] = useState(true);
  return (
    <div className="w-full  bg-white shadow-admin rounded-[8px]">
      <div
        className={`p-[20px] flex flex-row justify-between items-center cursor-pointer rounded-[8px] ${
          tab && "shadow-admin"
        }`}
        onClick={() => setTab(!tab)}
      >
        <h2 className="text-[20px]">
          Khách hàng có sinh nhật gần đây{" "}
          <span className="text-red-500">({data.totalResult})</span>
        </h2>{" "}
        {tab ? <BsDashLg /> : <BsPlusLg />}
      </div>
      <div className={`w-full p-[20px] ${!tab && "hidden"}`}>
        <div className="flex flex-row gap-x-[20px] items-center mb-[20px]">
          <span>Lọc sinh nhật: </span>
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
        <Table className="w-full">
          <Thead>
            <Tr>
              <Th>Tên khách hàng</Th>
              <Th>Email</Th>
              <Th>Ngày sinh</Th>
              <Th>Hành động</Th>
            </Tr>
          </Thead>
          <Tbody>
            {data?.data?.map((item) => {
              return (
                <Tr>
                  <Td className="pl-[20px]">
                    {item.lastName + " " + item.firstName}
                  </Td>
                  <Td className="text-center">{item.email}</Td>
                  <Td className="text-center">
                    {moment(item.birthday).format("DD-MM-yyyy")}
                  </Td>
                  <Td></Td>
                </Tr>
              );
            })}
          </Tbody>
        </Table>
        <div className="mt-[20px]">
          <Pagination
            setCurrentPage={setCurrentPage}
            totalPage={data?.totalPage}
            itemsPerPage={data?.data?.length}
          />
        </div>
      </div>
    </div>
  );
}

export default CustomerBirthday;
