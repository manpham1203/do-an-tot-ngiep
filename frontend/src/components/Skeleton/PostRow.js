import React from "react";
import PostCard from "./PostCard";

function PostRow(props) {
  return (
    <div className="container px-[10px] sm:px-[20px] mx-auto flex flex-row gap-x-[25px] my-[20px]">
      <div className=" w-full">
        <PostCard />
      </div>
      <div className="hidden sm:block w-full">
        <PostCard />
      </div>
      <div className="hidden md:block w-full">
        <PostCard />
      </div>
      <div className="hidden lg:block w-full">
        <PostCard />
      </div>
    </div>
  );
}

export default PostRow;
