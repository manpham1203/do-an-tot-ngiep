import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import { toast } from "react-toastify";
import * as moment from "moment";
import "moment/locale/nl";
import { BsPlusLg, BsDashLg } from "react-icons/bs";
import { Link } from "react-router-dom";

function PurchasedProduct(props) {
  const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/Order/PurchasedProduct`,
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
  console.log(data);
  return (
    <div className="grid grid-cols-1 lg:grid-cols-2 gap-x-[20px] gap-y-[20px] w-full">
      {data.length > 0 &&
        data.map((item) => {
          return (
            <div
              key={item.id}
              className="flex flex-row  p-[10px] gap-x-[10px] rounded-md bg-third shadow-md h-fit"
            >
              <div className="w-[150px] h-[150px] flex-none rounded-md overflow-hidden">
                <img
                  src={item.cartRowVM.imageSrc}
                  alt={item.cartRowVM.imageName}
                  className="w-full h-full object-cover object-center"
                />
              </div>
              <div className="flex flex-col grow-[1] bg-white rounded-md p-[10px] gap-y-[2px]">
                <Link to={`/san-pham?brand=${item.cartRowVM.brandNameVM.slug}`}>
                  {item.cartRowVM.brandNameVM.name}
                </Link>
                <Link to={`/san-pham/${item.cartRowVM.slug}`} className="productCard2Name">
                  {item.cartRowVM.name}
                </Link>
                <p>
                  Giá hiện tại:{" "}
                  {new Intl.NumberFormat("vi-VN", {
                    style: "currency",
                    currency: "VND",
                  }).format(item.cartRowVM.currentPrice)}
                </p>
                <Link to={`/san-pham/${item.cartRowVM.slug}`} className="border-2 border-second px-[20px] py-[5px] w-fit mt-[20px]">Đặt lại</Link>
              </div>
              
            </div>
          );
        })}
    </div>
  );
}

export default PurchasedProduct;
