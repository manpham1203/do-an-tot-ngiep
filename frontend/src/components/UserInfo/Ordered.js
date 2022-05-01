import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import { toast } from "react-toastify";
import OrderItem from "./OrderItem";
import * as moment from "moment";
import "moment/locale/nl";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { Link } from "react-router-dom";
import ProductCmt from "../Modal/ProductCmt";
import OrderDetailOfOrdered from "./OrderDetailOfOrdered";

function Ordered(props) {
  const orderOptions = [
    { value: 1, label: "Chờ xác nhận", color: "#2F4B60" },
    { value: 2, label: "Đang chuẩn bị hàng", color: "#2DCCBF" },
    { value: 3, label: "Đang vận chuyển", color: "#1A9487" },
    { value: 4, label: "Đã nhận hàng", color: "#9EBC4B" },
    { value: 0, label: "Đã huỷ", color: "#ED553B" },
  ];
  const [data, setData] = useState([]);
  const [openModal, setOpenModal] = useState(false);
  const [fetchDetail, setFetchDetail] = useState();
  const [modalData, setModalData] = useState({
    userId: props.userId,
    content: "",
    ObjectId: "",
    OrderDetailId: "",
    star: 0,
  });
  const handleOpenModal = (item) => {
    console.log(item);
    setModalData({
      ...modalData,
      userId: props.userId,
      ObjectId: item.productOrderVM.id,
      OrderDetailId: item.id,
    });
    setOpenModal(true);
  };
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/Order/GetOrderByUserIdState4`,
      params: { userId: id },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(props.userId);
  }, [props.userId]);
  const [itemOrder, setItemOrder] = useState(null);
  const handlerOpenItemOrder = (id) => {
    if (id === itemOrder) {
      setItemOrder(null);
    } else {
      setItemOrder(id);
    }
  };
  const orderStatusText = (status) =>
    orderOptions.map((s) =>
      s.value === status ? (
        <span key={s.value} style={{ color: `${s.color}` }}>
          {s.label}
        </span>
      ) : null
    );
  useEffect(() => {
    if (openModal) {
      document.body.style.overflowY = "hidden";
    }
    else{
      document.body.style.overflowY = "scroll";
    }
  }, [openModal]);
  return (
    <>
      <div className="flex flex-col gap-y-[20px] w-full relative">
        {data.length > 0 &&
          data.map((item) => {
            return (
              <div key={item.id} className="bg-white shadow-md rounded-md">
                <div
                  onClick={() => handlerOpenItemOrder(item.id)}
                  className="bg-white shadow-md w-full rounded-md flex flex-row items-center justify-between px-[20px] cursor-pointer"
                >
                  <div className="flex flex-row gap-x-[20px] rounded-md  py-[15px]">
                    <div>Mã đơn hàng: {item.id}</div>
                    <div>|</div>
                    <div>
                      Trạng thái đơn hàng: {orderStatusText(item.state)}
                    </div>
                  </div>
                  {itemOrder === item.id ? <BsDashLg /> : <BsPlusLg />}
                </div>
                {itemOrder === item.id && (
                  <div className="py-[20px] px-[20px] relative">
                    {openModal && (
                      <ProductCmt
                        modalData={modalData}
                        setOpenModal={setOpenModal}
                        setFetchDetail={setFetchDetail}
                      />
                    )}
                    <div>
                      <div>
                        Tên người nhận: {item.lastName + " " + item.firstName}
                      </div>
                      <div>Địa chỉ nhận hàng: {item.deliveryAddress}</div>
                      <div>Chiết khấu: {item.discount}</div>
                      <div>
                        Tổng hoá đơn:{" "}
                        {new Intl.NumberFormat("vi-VN", {
                          style: "currency",
                          currency: "VND",
                        }).format(item.amount)}
                      </div>
                      <div>
                        Ngày tạo: {moment(item.createdAt).format("DD-MM-yyyy")}
                      </div>
                    </div>
                    <div className="">
                      <h2 className="mb-[20px]">Sản phẩm đã mua:</h2>
                      <div className="grid grid-cols-2 gap-x-[20px] gap-y-[20px] ">
                        <OrderDetailOfOrdered
                          orderId={item.id}
                          handleOpenModal={handleOpenModal}
                          userId={props.userId}
                          fetchDetail={fetchDetail}
                        />
                      </div>
                    </div>
                  </div>
                )}
              </div>
            );
          })}
      </div>
      <div
        onClick={() => setOpenModal(false)}
        className={`${
          openModal ? null : "hidden"
        }  w-100% h-screen bg-[black] opacity-[0.5] fixed top-0 left-0 right-0 bottom-0 z-[7] transition-all duration-[0.3s]`}
      ></div>
    </>
  );
}

export default Ordered;
