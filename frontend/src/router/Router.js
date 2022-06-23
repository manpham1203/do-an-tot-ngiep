import React from "react";
import { Route, Routes } from "react-router-dom";
import About from "../views/About/About";
import Cart from "../views/Cart/Cart";
import Contact from "../views/Contact/Contact";
import Home from "../views/Home/Home";
import NotFound from "../views/NotFound/NotFound";
import ProductDetail from "../views/ProductDetail/ProductDetail";

function Router(props) {
  return (
    <Routes>
      <Route path="/" element={<Home />}></Route>
      <Route path="/gioi-thieu" element={<About />}></Route>
      <Route path="/lien-he" element={<Contact />}></Route>
      <Route path="/gio-hang" element={<Cart />}></Route>
      {/* <Route path="/brands" element={<Contact />}></Route> */}
      <Route path="/san-pham/:productId" element={<ProductDetail />}></Route>
      <Route path="*" element={<NotFound />}></Route>
    </Routes>
  );
}

export default Router;
