import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import api from "../../apis/api";
import { FaTimes } from "react-icons/fa";
import { setCloseAdminViewProduct } from "../../redux/adminViewProduct/adminViewProductActions";

function AdminViewProduct(props) {
  const dispatch=useDispatch();
  const { adminViewProduct } = useSelector((s) => s);
  const [data, setData] = useState();
  const [dataArr, setDataArr] = useState([]);
  const fetchData = async (slug) => {
    await api({
      method: "GET",
      url: `/Product/ProductDetailAdmin/${slug}`,
      data: null,
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(adminViewProduct.id);
  }, [adminViewProduct.id]);
  useEffect(() => {
    if (data?.id != null) {
      setDataArr(Object.entries(data));
    }
  }, [data]);
  const row = () => {
    for (var item in data) {
      return (
        <tr>
          <th className="text-left border border-slate-300">{item}</th>
          <td className="border border-slate-300">{data[item]}</td>
        </tr>
      );
    }
  };
  console.log(dataArr);
  return (
    <div className="flex  p-[30px] bg-third fixed w-[80%] h-[500px] rounded-xl top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] z-[2000]">
     <div className="w-fit absolute right-0 top-0">
        <FaTimes
          onClick={() => dispatch(setCloseAdminViewProduct())}
          className="inline-block text-[30px] text-second cursor-pointer"
        />
      </div>
      <div className="overflow-auto w-full h-full border border-gray-200">
        <table className="border-separate w-full">
          <tr>
            <th className="text-left border border-slate-300 w-[300px]">Id</th>
            <td className="border border-slate-300 w-[70%]">{data?.id}</td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Tên sản phẩm
            </th>
            <td className="border border-slate-300 w-[70%]">{data?.name}</td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Slug
            </th>
            <td className="border border-slate-300 w-[70%]">{data?.slug}</td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Thương hiệu
            </th>
            <td className="border border-slate-300 w-[70%]">
              {data?.brandNameVM?.name}
            </td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Danh mục
            </th>
            <td className="border border-slate-300 w-[70%]">
              {data?.categoryNameVMs?.map((item, i) =>
                i + 1 === data.categoryNameVMs.length ? (
                  <span>{item.name}</span>
                ) : (
                  <span>{item.name}, </span>
                )
              )}
            </td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">Giá</th>
            <td className="border border-slate-300 w-[70%]">{data?.price}</td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Giảm giá
            </th>
            <td className="border border-slate-300 w-[70%]">
              {data?.priceDiscount}
            </td>
          </tr>

          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Lượt xem
            </th>
            <td className="border border-slate-300 w-[70%]">{data?.view}</td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Lượt thích
            </th>
            <td className="border border-slate-300 w-[70%]">{data?.like}</td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Lượt đánh giá
            </th>
            <td className="border border-slate-300 w-[70%]">{data?.star}</td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Số lượng tồn
            </th>
            <td className="border border-slate-300 w-[70%]">
              {data?.quantityInStock}
            </td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Phát hành
            </th>
            <td className="border border-slate-300 w-[70%]">
              {data?.published === true ? "true" : "false"}
            </td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Thùng rác
            </th>
            <td className="border border-slate-300 w-[70%]">
              {data?.deleted === true ? "true" : "false"}
            </td>
          </tr>
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Mô tả
            </th>
            <td className="border border-slate-300 w-[70%]">
              <div dangerouslySetInnerHTML={{__html:data?.description}}>

              </div>
            </td>
          </tr>
          
          <tr>
            <th className="text-left border border-slate-300 w-[200px]">
              Hình
            </th>
            <td className="border border-slate-300 w-[70%]">
              <div className="flex flex-row gap-x-[20px]">
                {data?.pictureVMs.map((item) => {
                  return (
                    <div
                      key={item.id}
                      className="h-[100px] w-[100px] inline-block border border-gray-200"
                    >
                      <img
                        src={item.imageSrc}
                        alt=""
                        className="h-full w-full object-cover object-center"
                      />
                    </div>
                  );
                })}
              </div>
            </td>
          </tr>
        </table>
      </div>
    </div>
  );
}

export default AdminViewProduct;
