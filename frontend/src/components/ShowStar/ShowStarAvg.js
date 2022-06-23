import React from "react";
import { BsStar, BsStarFill, BsStarHalf } from "react-icons/bs";

function ShowStarAvg({ star }) {
  return (
    <>
      {star === 0 && (
        <>
          <BsStar />
          <BsStar />
          <BsStar />
          <BsStar />
          <BsStar />
        </>
      )}
      {star > 0 && star <= 0.5 && (
        <>
          <BsStarHalf />
          <BsStar />
          <BsStar />
          <BsStar />
          <BsStar />
        </>
      )}
      {star >= 0.6 && star <= 1 && (
        <>
          <BsStarFill />
          <BsStar />
          <BsStar />
          <BsStar />
          <BsStar />
        </>
      )}
      {star > 1 && star <= 1.5 && (
        <>
          <BsStarFill />
          <BsStarHalf />
          <BsStar />
          <BsStar />
          <BsStar />
        </>
      )}
      {star > 1.5 && star <= 2 && (
        <>
          <BsStarFill />
          <BsStarFill />
          <BsStar />
          <BsStar />
          <BsStar />
        </>
      )}
      {star > 2 && star <= 2.5 && (
        <>
          <BsStarFill />
          <BsStarFill />
          <BsStarHalf />
          <BsStar />
          <BsStar />
        </>
      )}
      {star > 2.5 && star <= 3 && (
        <>
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStar />
          <BsStar />
        </>
      )}
      {star > 3 && star <= 3.5 && (
        <>
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStarHalf />
          <BsStar />
        </>
      )}
      {star > 3.5 && star <= 4 && (
        <>
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStar />
        </>
      )}
      {star > 4 && star <= 4.5 && (
        <>
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStarHalf />
        </>
      )}
      {star > 4.5 && star <= 5 && (
        <>
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
          <BsStarFill />
        </>
      )}
    </>
  );
}

export default ShowStarAvg;
