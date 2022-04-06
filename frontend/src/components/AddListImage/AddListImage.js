import React from 'react';
import { AiOutlinePlus } from "react-icons/ai";
import { IoMdCloseCircle } from "react-icons/io";

function AddListImage(props) {
    return (
        <div>
        {/* <File control={control} name="image"  /> */}
        <label
          htmlFor="imageUpload"
          className="cursor-pointer flex flex-row w-[300px] h-[50px] bg-gray-300 rounded-xl border-2 border-dashed border-gray-500 justify-center items-center "
        >
          <AiOutlinePlus /> Thêm hình ảnh
        </label>
        <input
          id="imageUpload"
          type="file"
          name="image"
          multiple
          onChange={props.onChange}
          className="hidden"
        />
        <div className="grid grid-cols-6 gap-x-[20px] mt-[25px]">
          {props.image &&
            props.image.map((item, index) => {
              return (
                <div
                  className="w-[full] h-[full] flex flex-col relative"
                  key={index}
                >
                  <img
                    src={item}
                    alt={item}
                    className="w-[full] h-[full] object-cover object-center"
                  />
                  <IoMdCloseCircle
                    onClick={() => props.DeleteImg(item, index)}
                    className="absolute top-0 right-0 text-white text-[25px] hover:text-danger cursor-pointer transition-all duration-200"
                  />
                </div>
              );
            })}
        </div>
      </div>
    );
}

export default AddListImage;