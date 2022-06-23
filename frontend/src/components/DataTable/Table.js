import React from "react";

function Table(props) {
  return (
    <div className={`overflow-hidden overflow-x-auto border border-gray-600 rounded-[8px] ${props.className}`}>
      <table className="min-w-full text-sm divide-y divide-gray-600">
        {props.children}
      </table>
    </div>
  );
}

export default Table;
