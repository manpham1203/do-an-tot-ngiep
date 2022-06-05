import React from "react";
import { Link } from "react-router-dom";

function FooterListLink(props) {
  return (
    <div>
      <h2 className="font-medium text-[18px]">{props.title}</h2>
      <ul className="font-light ">
        {props.data.map((item) => {
          return (
            <li key={item.id}>
              <Link to={`san-pham?${props.type}=${item.slug}`} className="hover:underline hover:underline-offset-4 text-[#777]">
                - {item.name}
              </Link>
            </li>
          );
        })}
      </ul>
    </div>
  );
}

export default FooterListLink;
