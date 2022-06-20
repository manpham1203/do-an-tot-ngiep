import React from "react";

function FooterListItem(props) {
  return (
    <div>
      <h2 className="font-medium text-[18px]">{props.title}</h2>
      <ul className="font-light ">
        {props.data.map((item) => {
          return (
            <li key={item.id} className="text-[#777]">
              {item.name}
            </li>
          );
        })}
      </ul>
    </div>
  );
}

export default FooterListItem;
