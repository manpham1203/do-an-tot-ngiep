import { MENU } from "./menuConstants";

const initialState = [
  {
    id: 1,
    title: "TRANG CHỦ",
    slug: "/",
  },
  // {
  //   id: 5,
  //   title: "GIỚI THIỆU",
  //   slug: "/gioi-thieu",
  // },
  {
    id: 6,
    title: "SẢN PHẨM",
    slug: "/san-pham",
  },
  {
    id: 10,
    title: "LIÊN HỆ",
    slug: "/lien-he",
  },
  {
    id: 15,
    title: "TIN TỨC",
    slug: "/tin-tuc",
  },
];

export const menuReducer = (state = initialState, action) => {
  switch (action.type) {
    case MENU:
      return state;
    default:
      return state;
  }
};
