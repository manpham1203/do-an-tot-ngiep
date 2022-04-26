import React, { useState } from "react";
import { FaTimes, FaStar } from "react-icons/fa";
function ProductCmt(props) {
  const [active, setActive] = useState(null);
  const [color, setColor] = useState({
    one: { active: false },
    two: { active: false },
    three: { active: false },
    four: { active: false },
    five: { active: false },
  });
  const handleMouseEnter = (id) => {
    switch (id) {
      case 1:
        setColor({
          ...color,
          one: { ...color.one.active, active: true },
          two: { ...color.two.active, active: false },
          three: { ...color.three.active, active: false },
          four: { ...color.four.active, active: false },
          five: { ...color.five.active, active: false },
        });
        break;
      case 2:
        setColor({
          ...color,
          one: { ...color.one.active, active: true },
          two: { ...color.two.active, active: true },
          three: { ...color.three.active, active: false },
          four: { ...color.four.active, active: false },
          five: { ...color.five.active, active: false },
        });
        break;
      case 3:
        setColor({
          ...color,
          one: { ...color.one.active, active: true },
          two: { ...color.two.active, active: true },
          three: { ...color.three.active, active: true },
          four: { ...color.four.active, active: false },
          five: { ...color.five.active, active: false },
        });
        break;
      case 4:
        setColor({
          ...color,
          one: { ...color.one.active, active: true },
          two: { ...color.two.active, active: true },
          three: { ...color.three.active, active: true },
          four: { ...color.four.active, active: true },
          five: { ...color.five.active, active: false },
        });
        break;
      case 5:
        setColor({
          ...color,
          one: { ...color.one.active, active: true },
          two: { ...color.two.active, active: true },
          three: { ...color.three.active, active: true },
          four: { ...color.four.active, active: true },
          five: { ...color.five.active, active: true },
        });
        break;
      default:
        break;
    }
  };
  const handleMouseLeave = () => {
    setColor({
      ...color,
      one: { ...color.one.active, active: false },
      two: { ...color.two.active, active: false },
      three: { ...color.three.active, active: false },
      four: { ...color.four.active, active: false },
      five: { ...color.five.active, active: false },
    });
  };
  console.log(color[0]);

  return (
    <div className="absolute w-[80%] h-[300px] bg-white rounded-xl shadow-admin left-[50%] translate-x-[-50%]">
      <div className="w-full text-right text-[30px]">
        <button onClick={() => props.setOpenModal(false)} className="m-[10px]">
          <FaTimes />
        </button>
      </div>
      <div className="flex flex-col justify-center items-center ">
        <div
          className="flex flex-row mb-[20px] text-[20px] text-gray-400 gap-x-[10px]"
          onMouseLeave={() => handleMouseLeave()}
        >
          <FaStar
            className={`cursor-pointer ${color.one.active && "text-red-200"} ${active &&"text-black"}`}
            onClick={() => setActive(true)}
            onMouseEnter={() => handleMouseEnter(1)}
          />
          <FaStar
            className={`cursor-pointer ${color.two.active && "text-red-200"}`}
            onClick={() => setActive()}
            onMouseEnter={() => handleMouseEnter(2)}
          />
          <FaStar
            className={`cursor-pointer ${color.three.active && "text-red-200"}`}
            onClick={() => setActive()}
            onMouseEnter={() => handleMouseEnter(3)}
          />
          <FaStar
            className={`cursor-pointer ${color.four.active && "text-red-200"}`}
            onClick={() => setActive()}
            onMouseEnter={() => handleMouseEnter(4)}
          />
          <FaStar
            className={`cursor-pointer ${color.five.active && "text-red-200"}`}
            onClick={() => setActive()}
            onMouseEnter={() => handleMouseEnter(5)}
          />
        </div>
        <textarea
          name=""
          id=""
          className="border border-gray-400 rounded-md w-[90%] h-[100px] resize-none p-[10px]"
        ></textarea>
        <button className="bg-second text-third px-[20px] h-[40px] mt-[20px]">
          GỬI ĐÁNH GIÁ
        </button>
      </div>
      {props.modalData.star}
      {props.modalData.userId}
      {props.modalData.ObjectId}
      {props.modalData.OrderDetailId}
      {props.modalData.content}
    </div>
  );
}

export default ProductCmt;
