import { combineReducers } from "redux";
import { productsReducer } from "./products/productsReducer";
import { productDetailReducer } from "./productDetail/productDetailReducer";
import { brandsReducer } from "./brands/brandsReducer";
import { cartReducer } from "./cart/cartReducer";
import { productsOfBrandReducer } from "./productsOfBrand/productsOfBrandReducer";
import { productsOfCategoryReducer } from "./productsOfCategory/productsOfCategoryReducer";
import { categoryReducer } from "./category/categoryReducer";
import { userReducer } from "./user/userReducer";
import { quickViewReducer } from "./quickView/quickViewReducer";
import { adminViewProductReducer } from "./adminViewProduct/adminViewProductReducer";
import { adminViewBrandReducer } from "./adminViewBrand/adminViewBrandReducer";
import { adminViewOrderReducer } from "./adminViewOrder/adminViewOrderReducer";
import { adminViewCmtReducer } from "./adminViewCmt/adminViewCmtReducer";
import { adminViewPostCmtReducer } from "./adminViewPostCmt/adminViewPostCmtReducer";
import { adminViewPostReducer } from "./adminViewPost/adminViewPostReducer";
import { adminViewUserReducer } from "./adminViewUser/adminViewUserReducer";
import { menuReducer } from "./menu/menuReducer";
import { cursorReducer } from "./cursor/cursorReducer";
import { adminRepQuestionReducer } from "./adminRepQuestion/adminRepQuestionReducer";

const rootReducer = combineReducers({
  products: productsReducer,
  productDetail: productDetailReducer,
  brands: brandsReducer,
  cart: cartReducer,
  productsOfBrand: productsOfBrandReducer,
  productsOfCategory: productsOfCategoryReducer,
  category: categoryReducer,
  user: userReducer,
  quickView: quickViewReducer,
  adminViewProduct: adminViewProductReducer,
  adminViewBrand: adminViewBrandReducer,
  adminViewOrder: adminViewOrderReducer,
  adminViewCmt: adminViewCmtReducer,
  adminViewPostCmt: adminViewPostCmtReducer,
  adminViewPost: adminViewPostReducer,
  adminViewUser: adminViewUserReducer,
  menu: menuReducer,
  cursor: cursorReducer,
  adminRepQuestion: adminRepQuestionReducer,
});

export default rootReducer;
