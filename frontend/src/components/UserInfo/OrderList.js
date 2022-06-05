import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import { toast } from "react-toastify";
import OrderItem from "./OrderItem";
import * as moment from "moment";
import "moment/locale/nl";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { Link } from "react-router-dom";

function OrderList(props) {
  const orderOptions = [
    { value: 1, label: "Chờ xác nhận", color: "#2F4B60" },
    { value: 2, label: "Đang chuẩn bị hàng", color: "#2DCCBF" },
    { value: 3, label: "Đang vận chuyển", color: "#1A9487" },
    { value: 4, label: "Đã nhận hàng", color: "#9EBC4B" },
    { value: 0, label: "Đã huỷ", color: "#ED553B" },
  ];
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/Order`,
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
        <span key={s.value} style={{ color: `${s.color}` }}>{s.label}</span>
      ) : null
    );

  return (
    <div className="flex flex-col gap-y-[20px] w-full">
      {data.length > 0 &&
        data.map((item) => {
          return (
            <div key={item.id} className="bg-white shadow-md rounded-md">
              <div
                onClick={() => handlerOpenItemOrder(item.id)}
                className="bg-white shadow-md w-full rounded-md flex flex-row items-center justify-between px-[20px] cursor-pointer"
              >
                <div className="flex flex-col md:flex-row gap-x-[20px] rounded-md  py-[15px]">
                  <div>Mã đơn hàng: {item.id}</div>
                  <div className="hidden md:block">|</div>
                  {/* <div>
                    Ngày tạo: {moment(item.createdAt).format("DD-MM-yyyy")}
                  </div>
                  <div>|</div> */}
                  <div>Trạng thái đơn hàng: {orderStatusText(item.state)}</div>
                </div>
                {itemOrder === item.id ? <BsDashLg /> : <BsPlusLg />}
              </div>
              {itemOrder === item.id && (
                <div className="py-[20px] px-[20px]">
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
                    <div className="grid grid-cols-1 lg:grid-cols-2 gap-x-[20px] gap-y-[20px]">
                      {item.orderDetailVMs.map((detail) => {
                        return (
                          <div key={detail.id} className="flex flex-row  lg:p-[10px] gap-x-[10px] rounded-md bg-third">
                            <div className="w-[80px] h-[80px] lg:w-[150px] lg:h-[150px] flex-none rounded-md overflow-hidden">
                              <img
                                src={detail.productOrderVM.imageSrc}
                                alt={detail.productOrderVM.imageName}
                                className="w-full h-full object-cover object-center"
                              />
                            </div>
                            <div className="flex flex-col grow-[1] bg-white rounded-md p-[10px] gap-y-[2px]">
                              <Link
                                to={`/san-pham?brand=${detail.productOrderVM.brandNameVM.slug}`}
                              >
                                {detail.productOrderVM.brandNameVM.name}
                              </Link>
                              <Link
                                to={`/san-pham/${detail.productOrderVM.slug}`}
                                className="productCard2Name"
                              >
                                {detail.productOrderVM.name}
                              </Link>
                              <p>Số lượng: {detail.quantity}</p>
                              <p>
                                Đơn giá:{" "}
                                {new Intl.NumberFormat("vi-VN", {
                                  style: "currency",
                                  currency: "VND",
                                }).format(detail.unitPrice)}
                              </p>
                              <p>
                                Thành tiền:{" "}
                                {new Intl.NumberFormat("vi-VN", {
                                  style: "currency",
                                  currency: "VND",
                                }).format(detail.unitPrice * detail.quantity)}
                              </p>
                            </div>
                          </div>
                        );
                      })}
                    </div>
                  </div>
                </div>
              )}
            </div>
          );
        })}
    </div>
  );
}

export default OrderList;
