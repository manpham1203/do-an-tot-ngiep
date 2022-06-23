import React, { useEffect, useState } from "react";
import api from "../../../apis/api";
import PostCard from "../../../components/Post/PostCard";
import BrandWidget from "../../../components/Widget/BrandWidget";
import CategoryWidget from "../../../components/Widget/CategoryWidget";
import NewProductWidget from "../../../components/Widget/NewProductWidget";
import Pagination from "../../../components/Pagination/Pagination";
import MostBoughtWidget from "../../../components/Widget/MostBoughtWidget";
import OnSaleWidget from "../../../components/Widget/OnSaleWidget";
import PageContent from "../../../components/Skeleton/PageContent";

function Posts(props) {
  document.title = "Tin tức";
  const [data, setData] = useState();
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true);
  const fetchData = async (id) => {
    setLoading(true);
    await api({
      method: "GET",
      url: `/Post/PostPagination`,
      params: { limit: 6, currentPage: currentPage },
    })
      .then((res) => {
        if (res.status === 200) {
          setLoading(false);
          setData(res.data);
        } else {
          setLoading(true);
        }
      })
      .catch(() => setLoading(true));
  };
  useEffect(() => {
    fetchData();
  }, [currentPage]);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  console.log(data)
  return loading ? (
    <PageContent />
  ) : (
    <div className="container px-[10px] sm:px-[20px] mx-auto mt-[20px] text-second dark:text-third ">
      <div className="flex flex-row gap-x-[25px]">
        <div className="flex flex-col gap-y-[25px] w-full">
          {data?.postCardVMs.map((item) => {
            return (
              <PostCard
                key={item.id}
                title={item.title}
                slug={item.slug}
                image={item.imageSrc}
                createdAt={item.createdAt}
                shortDescription={item.shortDescription}
                view={item.view}
              />
            );
          })}
          <div className="flex justify-center mt-[30px] ">
            {loading ? (
              ""
            ) : (
              <Pagination
                forcePage={currentPage}
                setCurrentPage={setCurrentPage}
                totalPage={data?.totalPage}
                itemsPerPage={data?.postCardVMs?.length}
              />
            )}
          </div>
        </div>

        <div className="w-[300px] xl:w-[350px] hidden lg:block flex-none">
          <BrandWidget />
          <CategoryWidget />
          {/* <NewProductWidget /> */}
          <MostBoughtWidget />
          <OnSaleWidget />
        </div>
      </div>
    </div>
  );
}

export default Posts;
