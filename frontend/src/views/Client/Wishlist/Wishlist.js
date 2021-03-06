import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import api from "../../../apis/api";
import ProductCard from "../../../components/Product/ProductCard";
import ProductRow from "../../../components/Skeleton/ProductRow";

function Wishlist(props) {
  document.title = "Sản phẩm yêu thích";
  const { user } = useSelector((store) => store);
  const [data, setData] = useState([]);
  const fetchData = async () => {
    await api({
      method: "GET",
      url: `/product/ProductWishlist`,
      params: { userId: user.id, productId: props.id },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, []);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return data.length > 0 ? (
    <div className="mt-[40px] container mx-auto">
      {data.length > 0 && (
        <h2 className="text-center text-[30px] mb-[30px]">Sản Phẩm Đã Thích</h2>
      )}
      {data.length === 0 && (
        <h2 className="text-center text-[30px] mb-[30px]">
          Bạn Chưa Thích Sản Phẩm Nào
        </h2>
      )}
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-[25px]">
        {data.map((item) => {
          return (
            <ProductCard
              key={item.id}
              id={item.id}
              name={item.name}
              slug={item.slug}
              brandName={item.brandNameVM.name}
              price={item.price}
              priceDiscount={item.priceDiscount}
              image={item.imageSrc}
              star={item.star}
            />
          );
        })}
      </div>
    </div>
  ) : (
    <ProductRow />
  );
}

export default Wishlist;
