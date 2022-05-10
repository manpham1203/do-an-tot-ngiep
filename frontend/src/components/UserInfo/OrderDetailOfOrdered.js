import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../../apis/api";

function OrderDetailOfOrdered(props) {
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/OrderDetail/GetListDetailByOrderIdUserId`,
      params: { orderId: id, userId:props.userId },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(props.orderId);
  }, [props.orderId]);
  useEffect(()=>{
    fetchData(props.orderId);
  }, [props.fetchDetail])
  console.log(data);
  return (
    <>
      {data.length > 0 &&
        data.map((item) => {
          return (
            <div
              key={item.id}
              className="flex flex-col  gap-y-[10px] rounded-md bg-third "
            >
              <div className="flex flex-row  p-[10px] gap-x-[10px]">
                <div className="w-[150px] h-[150px]  rounded-md overflow-hidden">
                  <img
                    src={item.productOrderVM.imageSrc}
                    alt={item.productOrderVM.imageName}
                    className="w-full h-full object-cover object-center"
                  />
                </div>
                <div className="flex flex-col grow-[1] bg-white rounded-md p-[10px] gap-y-[2px]">
                  <Link
                    to={`/san-pham?brand=${item.productOrderVM.brandNameVM.slug}`}
                  >
                    {item.productOrderVM.brandNameVM.name}
                  </Link>
                  <Link to={`/san-pham/${item.productOrderVM.slug}`}>
                    {item.productOrderVM.name}
                  </Link>
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

              {item.productOrderVM.commented === false && (
                <button
                  onClick={() => props.handleOpenModal(item)}
                  className="border-2 border-second px-[20px] h-[40px] w-fit self-center"
                >
                  Đánh Giá
                </button>
              )}
              {item.productOrderVM.commented && (
                <div
                  className="border-2 border-gray-400 text-gray-400 px-[20px] h-[40px] w-fit self-center flex items-center"
                >
                  Đã Đánh Giá
                </div>
              )}
            </div>
          );
        })}
    </>
  );
}

export default OrderDetailOfOrdered;
