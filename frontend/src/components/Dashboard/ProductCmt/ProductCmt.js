import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import Pagination from "../../Pagination/Pagination";
import Select from "react-select";
import Table from "../../Table/Table";
import Thead from "../../Table/Thead";
import Tr from "../../Table/Tr";
import Th from "../../Table/Th";
import Tbody from "../../Table/Tbody";
import { FaRegEdit, FaRegEye } from "react-icons/fa";
import Td from "../../Table/Td";
import defaultuser from "../../../assets/defaultuser.png";
import ShowStarCmt from "../../ShowStar/ShowStarCmt";
import { setOpenadminViewCmt } from "../../../redux/adminViewCmt/adminViewCmtActions";
import { useDispatch } from "react-redux";
import { BsPlusLg, BsDashLg } from "react-icons/bs";

const starOptions = [
  { value: 1, label: "1 Sao" },
  { value: 2, label: "2 Sao" },
  { value: 3, label: "3 Sao" },
  { value: 4, label: "4 Sao" },
  { value: 5, label: "5 Sao" },
];

const colourStyles = {
  dropdownIndicator: (styles) => ({ ...styles, color: "#202121" }),
  placeholder: (styles, { data, isDisabled, isFocused, isSelected }) => ({
    ...styles,
    color: data?.color,
    left: "0%",
    lineHeight: "1.3rem",
    marginLeft: "0rem",
  }),
  control: () => ({
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
    width: "250px",
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
function ProductCmt(props) {
  const [data, setData] = useState();
  const [currentPage, setCurrentPage] = useState(1);
  const [star, setStar] = useState(null);
  const [loading, setLoading] = useState(true);
  const fetchData = async () => {
    setLoading(true);
    await api({
      method: "GET",
      url: `/comment/CmtPagination`,
      params: {
        currentPage: currentPage,
        limit: 5,
        star: star?.value,
        objectType: "product",
      },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
          setLoading(false);
        }
      })
      .catch(() => setLoading(true));
  };
  useEffect(() => {
    fetchData();
  }, [currentPage, star]);
  const dispatchQV = useDispatch();
  const handleQuickView = (id) => {
    const obj = {
      show: true,
      id: id,
    };
    dispatchQV(setOpenadminViewCmt(obj));
  };
  const [show, setShow] = useState(true);
  return (
    <div className="w-full bg-white shadow-admin rounded-[8px]">
      <div
        className={`p-[20px] flex flex-row justify-between items-center cursor-pointer rounded-[8px] ${
          show && "shadow-admin"
        }`}
        onClick={() => setShow(!show)}
      >
        <h2 className="text-[20px]">Đánh giá sản phẩm</h2>
        {show ? <BsDashLg /> : <BsPlusLg />}
      </div>

      <div className={`w-full p-[20px] ${!show && "hidden"}`}>
        <div className="flex flex-row gap-x-[20px] items-center mb-[20px]">
          <span>Lọc Đánh Giá: </span>
          <Select
            className=" cursor-pointer "
            classNamePrefix="select"
            // defaultValue={orderOptions[0]}
            isClearable={true}
            isSearchable={false}
            name="orderStatus"
            value={star}
            onChange={(e) => setStar(e)}
            options={starOptions}
            styles={colourStyles}
            placeholder="Lọc đánh giá"
          />
        </div>
        <Table className="border-collapse w-full ">
          <Thead>
            <Tr>
              <Th className="w-[80px]">Avatar</Th>
              <Th className="full">Bình luận</Th>
              <Th className="w-[120px]">Đánh giá</Th>
              {/* <Th className="w-[150px]">Hành động</Th> */}
            </Tr>
          </Thead>
          <Tbody>
            {data?.list?.map((item) => {
              return (
                <Tr key={item.id}>
                  <Td className="py-[10px]">
                    <div className="w-full flex justify-center">
                      <div className="w-[60px] h-[60px]">
                        <img
                          src={item?.imageSrc || defaultuser}
                          alt=""
                          className="w-full h-full object-cover object-center"
                        />
                      </div>
                    </div>
                  </Td>
                  <Td className="full">
                    <div className="w-full px-[20px] short-desc-postcard2">
                      {item?.content}
                    </div>
                  </Td>
                  <Td>
                    <ShowStarCmt star={item?.star} />
                  </Td>
                  {/* <Td>
                    <div className="flex flex-row justify-center text-[25px] gap-x-[20px] w-full">
                      <FaRegEye
                        // onClick={() => handleQuickView(item?.id)}
                        className="cursor-pointer"
                      />
                      <FaRegEdit
                        // onClick={() => handleEdit(state.data.id)}
                        className="cursor-pointer"
                      />
                    </div>
                  </Td> */}
                </Tr>
              );
            })}
          </Tbody>
        </Table>
        <div className="mt-[20px]">
          {loading ? (
            "loading"
          ) : (
            <Pagination
              forcePage={currentPage}
              setCurrentPage={setCurrentPage}
              totalPage={data?.totalPage}
              itemsPerPage={data?.list?.length}
            />
          )}
        </div>
      </div>
    </div>
  );
}

export default ProductCmt;
