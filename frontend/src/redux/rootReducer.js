import { combineReducers } from "redux";
import { productsReducer } from "./products/productsReducer";
import { productDetailReducer } from "./productDetail/productDetailReducer";
import { brandsReducer } from "./brands/brandsReducer";
import { cartReducer } from "./cart/cartReducer";
import { productsOfBrandReducer } from "./productsOfBrand/productsOfBrandReducer";
import { productsOfCategoryReducer } from "./productsOfCategory/productsOfCategoryReducer";
import { categoryReducer } from "./category/categoryReducer";
import { userReducer } from "./user/userReducer";


const rootReducer=combineReducers({
    products:productsReducer,
    productDetail:productDetailReducer,
    brands:brandsReducer,
    cart:cartReducer,
    productsOfBrand:productsOfBrandReducer,
    productsOfCategory:productsOfCategoryReducer,
    category:categoryReducer,
    user:userReducer,
})

export default rootReducer;