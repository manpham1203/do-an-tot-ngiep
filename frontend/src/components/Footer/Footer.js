import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../../apis/api";
import FooterListItem from "./FooterListItem";
import FooterListLink from "./FooterListLink";

function Footer(props) {
  const [brand, setBrand] = useState([]);
  const [category, setCategory] = useState([]);
  const [contact, setContact] = useState([]);
  const [page, setPage] = useState([]);
  const fetchBrand = async (id) => {
    await api({
      method: "GET",
      url: `/category/allcategoryname`,
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
    })
      .then((res) => {
        setBrand(res.data);
      })
      .catch(() => console.log("fail"));
  };
  const fetchContact = async (id) => {
    await api({
      method: "GET",
      url: `/contact/GetList`,
    })
      .then((res) => {
        setContact(res.data);
      })
      .catch(() => console.log("fail"));
  };
  const fetchPage = async (id) => {
    await api({
      method: "GET",
      url: `/page/GetList`,
    })
      .then((res) => {
        setPage(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchBrand();
    fetchCategory();
    fetchContact();
    fetchPage();
  }, []);
  return (
    <div className="border-t border-t-[#161a2133] mt-[40px]">
      <div className="mx-auto container">
        <div className="flex flex-col items-center lg:items-start lg:flex-row mt-[40px] gap-x-[30px] gap-y-[30px]">
          <div className="w-[100%] lg:w-[300px]">
            <h2 className="text-[25px]">WatchStore</h2>
            <p className="font-light text-[#777]">Nhà tôi 3 đời bán đồng hồ</p>
          </div>
          <div className="flex flex-row flex-1 gap-x-[30px] w-full">
            <div className="flex-1">
              <FooterListLink title="Thương Hiệu" data={brand} type="brand" />
            </div>
            <div className="flex-1">
              <FooterListLink
                title="Danh Mục"
                data={category}
                type="category"
              />
            </div>
          </div>

          <div className="flex flex-row flex-1 gap-x-[30px] w-full">
            <div className="flex-1">
              <div>
                <h2 className="font-medium text-[18px]">Thông tin</h2>
                <ul className="font-light ">
                  {page.map((item) => {
                    return (
                      <li key={item.id}>
                        <Link
                          to={`/trang/${item.slug}`}
                          className="hover:underline hover:underline-offset-4 text-[#777]"
                        >
                          {item.title}
                        </Link>
                      </li>
                    );
                  })}
                </ul>
              </div>
            </div>
            <div className="flex-1">
              <div>
                <h2 className="font-medium text-[18px]">Liên hệ</h2>
                <ul className="font-light ">
                  {contact.map((item) => {
                    return (
                      <li key={item.id} className="text-[#777]">
                        <span>{item.name}</span>: <span>{item.content}</span>
                      </li>
                    );
                  })}
                </ul>
              </div>
            </div>
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
