import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import FooterNav from "./FooterNav";

function Footer(props) {
  const [brand, setBrand] = useState([]);
  const [category, setCategory] = useState([]);
  const fetchBrand = async (id) => {
    await api({
      method: "GET",
      url: `/category/allcategoryname`,
      params: { id: id },
    })
      .then((res) => {
        setCategory(res.data);
      })
      .catch(() => console.log("fail"));
  };
  const fetchCategory = async (id) => {
    await api({
      method: "GET",
      url: `/brand/allbrandname`,
      params: { id: id },
    })
      .then((res) => {
        setBrand(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchBrand();
    fetchCategory();
  }, []);
  return (
    <div className="border-t border-t-[#161a2133] mt-[40px]">
      <div className="mx-auto container">
        <div className="flex flex-row mt-[40px] gap-x-[40px]">
          <div className="flex-1">
            <h2>Logo</h2>
            <p className="font-light text-[#777]">Nhà tôi 3 đời bán đồng hồ</p>
          </div>
          <div className="flex-1">
            <FooterNav title="Thương Hiệu" data={brand} type="brand" />
          </div>
          <div className="flex-1">
            <FooterNav title="Danh Mục" data={category} type="category" />
          </div>
          <div className="flex-1">Địa chỉ</div>
        </div>
        <div className="border-t border-t-[#161a2133] h-[70px] flex flex-row items-center mt-[40px] gap-x-[40px] font-light">
          <span>ĐỒ ÁN TỐT NGHIỆP</span>
          <span>Phạm Minh Mẫn</span>
        </div>
      </div>
    </div>
  );
}

export default Footer;
