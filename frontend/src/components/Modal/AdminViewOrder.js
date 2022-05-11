import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import api from "../../apis/api";
import { FaTimes } from "react-icons/fa";
import { setCloseAdminViewOrder } from "../../redux/adminViewOrder/adminViewOrderActions";
import * as moment from "moment";
import "moment/locale/nl";

function AdminViewOrder() {
  const dispatch = useDispatch();
  const { adminViewOrder } = useSelector((s) => s);
  const [data, setData] = useState();

  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/order/GetFullById`,
      params: { id: id },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(adminViewOrder.id);
  }, [adminViewOrder.id]);

  console.log("dt", data);

  return (
    <div className="flex  p-[30px] bg-third fixed w-[80%] h-[500px] rounded-xl top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] z-[2000]">
      <div className="w-fit absolute right-0 top-0">
        <FaTimes
          onClick={() => dispatch(setCloseAdminViewOrder())}
          className="inline-block text-[30px] text-second cursor-pointer"
        />
      </div>
      <div className="overflow-auto w-full h-full border border-gray-200">
        <div className="py-[20px] px-[20px] relative">
          <div>
            <div>Tên người nhận: {data?.lastName + " " + data?.firstName}</div>
            <div>Địa chỉ nhận hàng: {data?.deliveryAddress}</div>
            <div>Chiết khấu: {data?.discount}</div>
            <div>
              Tổng hoá đơn:{" "}
              {new Intl.NumberFormat("vi-VN", {
                style: "currency",
                currency: "VND",
              }).format(data?.amount)}
            </div>
            <div>Ngày tạo: {moment(data?.createdAt).format("DD-MM-yyyy")}</div>
          </div>
          <div className="">
            <h2 className="mb-[20px]">Sản phẩm đã mua:</h2>
            <div className="grid grid-cols-2 gap-x-[20px] gap-y-[20px] ">
              {data?.orderDetailVMs.map((item) => {
                return (
                  <div
                    key={item.id}
                    className="flex flex-col  gap-y-[10px] rounded-md bg-third "
                  >
                    <div className="flex flex-row  p-[10px] gap-x-[10px]">
                      <div className="w-[150px] h-[150px] flex-none rounded-md overflow-hidden">
                        <img
                          src={item.productOrderVM.imageSrc}
                          alt={item.productOrderVM.imageName}
                          className="w-full h-full object-cover object-center"
                        />
                      </div>
                      <div className="flex flex-col grow-[1] bg-white rounded-md p-[10px] gap-y-[2px]">
                        <p>{item.productOrderVM.brandNameVM.name}</p>
                        <p className="productCard2Name">{item.productOrderVM.name}</p>
                        <p>Số lượng: {item.quantity}</p>
                        <p>
                          Đơn giá:{" "}
                          {new Intl.NumberFormat("vi-VN", {
                            style: "currency",
                            currency: "VND",
                          }).format(item.unitPrice)}
                        </p>
                        <p>
                          Thành tiền:{" "}
                          {new Intl.NumberFormat("vi-VN", {
                            style: "currency",
                            currency: "VND",
                          }).format(item.unitPrice * item.quantity)}
                        </p>
                      </div>
                    </div>
                  </div>
                );
              })}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default AdminViewOrder;
