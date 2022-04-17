import React, { useEffect, useState } from "react";
import ReactPaginate from "react-paginate";

function Pagination(props) {
  const [itemsPerPage, setItemsPerPage] = useState();
  const [pageCount, setPageCount] = useState(0);
  const [itemOffset, setItemOffset] = useState(0);

  useEffect(() => {
    setPageCount(props.totalPage);
    setItemsPerPage(props.itemsPerPage);
  }, [itemOffset, props.totalPage, props.itemsPerPage]);
  const handlePageClick = (event) => {
    const newOffset = (event.selected * itemsPerPage) % props.totalPage;
    setItemOffset(newOffset);
    props.setCurrentPage(event.selected + 1);
  };
  return (
    <ReactPaginate
      breakLabel="..."
      nextLabel="next >"
      onPageChange={handlePageClick}
      pageRangeDisplayed={5}
      pageCount={pageCount}
      previousLabel="< previous"
      renderOnZeroPageCount={null}
      className="pagination"
    />
  );
}

export default Pagination;
