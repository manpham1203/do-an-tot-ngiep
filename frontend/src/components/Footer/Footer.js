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
        <div className="flex flex-col items-center lg:items-start lg:flex-row mt-[40px] gap-x-[40px] gap-y-[30px]">
          <div className="w-[400px] lg:w-[300px]">
            <h2>Logo</h2>
            <p className="font-light text-[#777]">Nhà tôi 3 đời bán đồng hồ</p>
          </div>
          <div className="flex flex-row">
            <div className="w-[200px]">
              <FooterNav title="Thương Hiệu" data={brand} type="brand" />
            </div>
            <div className="w-[200px]">
              <FooterNav title="Danh Mục" data={category} type="category" />
            </div>
          </div>

          <div className="w-full lg:flex-1">
            <iframe
              src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3918.7516950361432!2d106.77279011411686!3d10.830304561160043!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752701a34a5d5f%3A0x30056b2fdf668565!2zVHLGsOG7nW5nIENhbyDEkOG6s25nIEPDtG5nIFRoxrDGoW5nIFRQLkhDTQ!5e0!3m2!1svi!2s!4v1652254342771!5m2!1svi!2s"
              width="100%"
              height="200px"
              allowfullscreen=""
              loading="lazy"
              referrerpolicy="no-referrer-when-downgrade"
            ></iframe>
          </div>
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
