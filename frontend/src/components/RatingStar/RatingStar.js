import React, { useState, memo } from "react";
import { FaStar } from "react-icons/fa";

function RatingStar(props) {
  const [hover, setHover] = useState({
    one: { active: false },
    two: { active: false },
    three: { active: false },
    four: { active: false },
    five: { active: false },
  });
  const [click, setClick] = useState({
    one: { active: false },
    two: { active: false },
    three: { active: false },
    four: { active: false },
    five: { active: false },
  });
  const handleMouseEnter = (id) => {
    switch (id) {
      case 1:
        setHover({
          ...hover,
          one: { ...hover.one.active, active: true },
          two: { ...hover.two.active, active: false },
          three: { ...hover.three.active, active: false },
          four: { ...hover.four.active, active: false },
          five: { ...hover.five.active, active: false },
        });
        break;
      case 2:
        setHover({
          ...hover,
          one: { ...hover.one.active, active: true },
          two: { ...hover.two.active, active: true },
          three: { ...hover.three.active, active: false },
          four: { ...hover.four.active, active: false },
          five: { ...hover.five.active, active: false },
        });
        break;
      case 3:
        setHover({
          ...hover,
          one: { ...hover.one.active, active: true },
          two: { ...hover.two.active, active: true },
          three: { ...hover.three.active, active: true },
          four: { ...hover.four.active, active: false },
          five: { ...hover.five.active, active: false },
        });
        break;
      case 4:
        setHover({
          ...hover,
          one: { ...hover.one.active, active: true },
          two: { ...hover.two.active, active: true },
          three: { ...hover.three.active, active: true },
          four: { ...hover.four.active, active: true },
          five: { ...hover.five.active, active: false },
        });
        break;
      case 5:
        setHover({
          ...hover,
          one: { ...hover.one.active, active: true },
          two: { ...hover.two.active, active: true },
          three: { ...hover.three.active, active: true },
          four: { ...hover.four.active, active: true },
          five: { ...hover.five.active, active: true },
        });
        break;
      default:
        break;
    }
  };
  const handleMouseLeave = () => {
    setHover({
      ...hover,
      one: { ...hover.one.active, active: false },
      two: { ...hover.two.active, active: false },
      three: { ...hover.three.active, active: false },
      four: { ...hover.four.active, active: false },
      five: { ...hover.five.active, active: false },
    });
  };
  const handleClick = (id) => {
    switch (id) {
      case 1:
        setClick({
          ...click,
          one: { ...click.one.active, active: true },
          two: { ...click.two.active, active: false },
          three: { ...click.three.active, active: false },
          four: { ...click.four.active, active: false },
          five: { ...click.five.active, active: false },
        });
        props.setStar(1);
        break;
      case 2:
        setClick({
          ...click,
          one: { ...click.one.active, active: true },
          two: { ...click.two.active, active: true },
          three: { ...click.three.active, active: false },
          four: { ...click.four.active, active: false },
          five: { ...click.five.active, active: false },
        });
        props.setStar(2);
        break;
      case 3:
        setClick({
          ...click,
          one: { ...click.one.active, active: true },
          two: { ...click.two.active, active: true },
          three: { ...click.three.active, active: true },
          four: { ...click.four.active, active: false },
          five: { ...click.five.active, active: false },
        });
        props.setStar(3);
        break;
      case 4:
        setClick({
          ...click,
          one: { ...click.one.active, active: true },
          two: { ...click.two.active, active: true },
          three: { ...click.three.active, active: true },
          four: { ...click.four.active, active: true },
          five: { ...click.five.active, active: false },
        });
        props.setStar(4);
        break;
      case 5:
        setClick({
          ...click,
          one: { ...click.one.active, active: true },
          two: { ...click.two.active, active: true },
          three: { ...click.three.active, active: true },
          four: { ...click.four.active, active: true },
          five: { ...click.five.active, active: true },
        });
        props.setStar(5);
        break;
      default:
        break;
    }
  };
  return (
    <div
      className="flex flex-row text-[20px]  gap-x-[10px]"
      onMouseLeave={() => handleMouseLeave()}
    >
      <FaStar
        className={`cursor-pointer ${
          hover.one.active
            ? "text-[rgb(212,159,74)]"
            : click.one.active
            ? "text-[#F7BF63]"
            : "text-gray-400"
        }  `}
        onMouseEnter={() => handleMouseEnter(1)}
        onClick={() => handleClick(1)}
      />
      <FaStar
        className={`cursor-pointer ${
          hover.two.active
            ? "text-[rgb(212,159,74)]"
            : click.two.active
            ? "text-[#F7BF63]"
            : "text-gray-400"
        }`}
        onMouseEnter={() => handleMouseEnter(2)}
        onClick={() => handleClick(2)}
      />
      <FaStar
        className={`cursor-pointer ${
          hover.three.active
            ? "text-[rgb(212,159,74)]"
            : click.three.active
            ? "text-[#F7BF63]"
            : "text-gray-400"
        }`}
        onMouseEnter={() => handleMouseEnter(3)}
        onClick={() => handleClick(3)}
      />
      <FaStar
        className={`cursor-pointer ${
          hover.four.active
            ? "text-[rgb(212,159,74)]"
            : click.four.active
            ? "text-[#F7BF63]"
            : "text-gray-400"
        }`}
        onMouseEnter={() => handleMouseEnter(4)}
        onClick={() => handleClick(4)}
      />
      <FaStar
        className={`cursor-pointer ${
          hover.five.active
            ? "text-[rgb(212,159,74)]"
            : click.five.active
            ? "text-[#F7BF63]"
            : "text-gray-400"
        }`}
        onMouseEnter={() => handleMouseEnter(5)}
        onClick={() => handleClick(5)}
      />
    </div>
  );
}

export default memo(RatingStar);
