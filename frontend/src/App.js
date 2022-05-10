import "./App.css";
import { Route, Routes, useLocation, useNavigate } from "react-router-dom";
import Home from "./views/Client/Home/Home";
import About from "./views/Client/About/About";
import Contact from "./views/Client/Contact/Contact";
import Cart from "./views/Client/Cart/Cart";
import ProductDetail from "./views/Client/ProductDetail/ProductDetail";
import NotFound from "./views/Client/NotFound/NotFound";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import ProductsBrand from "./views/Client/ProductsBrand/ProductsBrand";
import ProductsCategory from "./views/Client/ProductsCategory/ProductsCategory";
import LoginPage from "./views/Client/LoginPage/LoginPage";
import Account from "./views/Client/Account/Account";
import Dashboard from "./views/Admin/Dashboard/Dashboard";
import ProductTable from "./views/Admin/Product/ProductTable";
import ProductCreate from "./views/Admin/Product/ProductCreate";
import SideBar from "./components/SideBar/SideBar";
import Layout from "./views/Client/Layout/Layout";
import { useSelector } from "react-redux";
import BrandTable from "./views/Admin/Brand/BrandTable";
import BrandCreate from "./views/Admin/Brand/BrandCreate";
import BrandEdit from "./views/Admin/Brand/BrandEdit";
import BrandTrash from "./views/Admin/Brand/BrandTrash";
import CategoryTable from "./views/Admin/Category/CategoryTable";
import CategoryEdit from "./views/Admin/Category/CategoryEdit";
import CategoryTrash from "./views/Admin/Category/CategoryTrash";
import CategoryCreate from "./views/Admin/Category/CategoryCreate";
import ProductEdit from "./views/Admin/Product/ProductEdit";
import ProductTrash from "./views/Admin/Product/ProductTrash";
import { useEffect, useState } from "react";
import Products from "./views/Client/Products/Products";
import Checkout from "./views/Client/Cart/Checkout";
import OrderTable from "./views/Admin/Order/OrderTable";
import OrderEdit from "./views/Admin/Order/OrderEdit";
import PostTable from "./views/Admin/Post/PostTable";
import PostCreate from "./views/Admin/Post/PostCreate";
import PostEdit from "./views/Admin/Post/PostEdit";
import PostTrash from "./views/Admin/Post/PostTrash";
import Posts from "./views/Client/Posts/Posts";
import PostDetail from "./views/Client/PostDetail/PostDetail";
import Wishlist from "./views/Client/Wishlist/Wishlist";
import QuickView from "./components/Modal/QuickView";
import AdminViewProduct from "./components/Modal/AdminViewProduct";
import Overlay from "./components/Overlay/Overlay";
import OverlayAdminViewProduct from "./components/Overlay/OverlayAdminViewProduct";
import BannerTable from "./views/Admin/Banner/BannerTable";
import BannerTrash from "./views/Admin/Banner/BannerTrash";
import BannerCreate from "./views/Admin/Banner/BannerCreate";
import BannerEdit from "./views/Admin/Banner/BannerEdit";
import OverlayAdminViewOrder from "./components/Overlay/OverlayAdminViewOrder";
import AdminViewOrder from "./components/Modal/AdminViewOrder";
import OrderPending from "./views/Admin/Order/OrderPending";
import OrderCancel from "./views/Admin/Order/OrderCancel";

function App() {
  const { user, quickView, adminViewProduct,adminViewOrder } = useSelector((store) => store);
  useEffect(() => {
    if (quickView.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [quickView.show]);
  useEffect(() => {
    if (adminViewProduct.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [adminViewProduct.show]);
  const navigate = useNavigate();
  const location = useLocation();
  useEffect(() => {
    if (user.id === null && location.pathname === "/tai-khoan") {
      navigate("/dang-nhap");
    }
  }, [user.id, location.pathname]);
  useEffect(() => {
    if (user.id === null && location.pathname === "/tai-khoan") {
      navigate("/dang-nhap");
    }
  }, [user.id, location.pathname]);
  useEffect(() => {
    if (user.role !== 1 && location.pathname.slice(0, 6) === "/admin") {
      navigate("/admin-login");
    }
  }, [user.role, location.pathname]);

  console.log("av",adminViewProduct)
  return (
    <div className="w-[100%] min-h-screen">
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/" element={<Home />}></Route>
          <Route path="/gioi-thieu" element={<About />}></Route>
          <Route path="/lien-he" element={<Contact />}></Route>
          <Route path="/gio-hang" element={<Cart />}></Route>
          <Route path="/thanh-toan" element={<Checkout />}></Route>
          <Route
            path="/thuong-hieu/:brandId"
            element={<ProductsBrand />}
          ></Route>
          <Route
            path="/danh-muc/:categoryId"
            element={<ProductsCategory />}
          ></Route>

          {/* {user.id===null&&location.pathname==="/tai-khoan"?()=>navigate("/dang-nhap"):null} */}
          {user.id === null && (
            <Route path="/dang-nhap" element={<LoginPage />}></Route>
          )}
          {user.id !== null && (
            <Route path="/tai-khoan" element={<Account />} />
          )}

          {/* admin */}
          <Route path="/san-pham" element={<Products />}></Route>
          <Route path="/san-pham/:slug" element={<ProductDetail />}></Route>
          <Route path="/tin-tuc" element={<Posts />}></Route>
          <Route path="/tin-tuc/:slug" element={<PostDetail />}></Route>
          <Route path="/danh-sach-yeu-thich" element={<Wishlist />}></Route>
        </Route>

        {user.role !== 1 && (
          <Route path="/admin-login" element={<div>haha</div>}></Route>
        )}

        {user.role === 1 && (
          <Route path="/admin" element={<SideBar />}>
            <Route path="/admin/dashboard" element={<Dashboard />} />

            <Route
              path="/admin/san-pham/chinh-sua/:slug"
              element={<ProductEdit />}
            />
            <Route path="/admin/san-pham/tao-moi" element={<ProductCreate />} />
            <Route
              path="/admin/san-pham/thung-rac"
              element={<ProductTrash />}
            />
            <Route
              path="/admin/san-pham/danh-sach"
              element={<ProductTable />}
            />
            <Route
              path="/admin/thuong-hieu/danh-sach"
              element={<BrandTable />}
            />
            <Route
              path="/admin/thuong-hieu/tao-moi"
              element={<BrandCreate />}
            />
            <Route
              path="/admin/thuong-hieu/chinh-sua/:slug"
              element={<BrandEdit />}
            />
            <Route
              path="/admin/thuong-hieu/thung-rac"
              element={<BrandTrash />}
            />
            <Route
              path="/admin/danh-muc/danh-sach"
              element={<CategoryTable />}
            />
            <Route
              path="/admin/danh-muc/tao-moi"
              element={<CategoryCreate />}
            />
            <Route
              path="/admin/danh-muc/chinh-sua/:slug"
              element={<CategoryEdit />}
            />
            <Route
              path="/admin/danh-muc/thung-rac"
              element={<CategoryTrash />}
            />
            <Route path="/admin/don-hang/danh-sach" element={<OrderTable />} />
            <Route path="/admin/don-hang/chua-xu-li" element={<OrderPending />} />
            <Route path="/admin/don-hang/da-huy" element={<OrderCancel />} />
            <Route
              path="/admin/don-hang/chinh-sua/:slug"
              element={<OrderEdit />}
            />
            <Route path="/admin/tin-tuc/danh-sach" element={<PostTable />} />
            <Route path="/admin/tin-tuc/tao-moi" element={<PostCreate />} />
            <Route path="/admin/tin-tuc/chinh-sua/:id" element={<PostEdit />} />
            <Route path="/admin/tin-tuc/da-xoa" element={<PostTrash />} />
            <Route path="/admin/banner/danh-sach" element={<BannerTable />} />
            <Route path="/admin/banner/da-xoa" element={<BannerTrash />} />
            <Route path="/admin/banner/tao-moi" element={<BannerCreate />} />
            <Route
              path="/admin/banner/chinh-sua/:id"
              element={<BannerEdit />}
            />
          </Route>
        )}

        <Route path="*" element={<NotFound />}></Route>
      </Routes>
      <ToastContainer />
      {quickView.show && (
        <>
          <Overlay />
          <QuickView />
        </>
      )}
      {adminViewProduct.show && (
        <>
          <OverlayAdminViewProduct />
          <AdminViewProduct />
        </>
      )}
      {adminViewOrder.show && (
        <>
          <OverlayAdminViewOrder />
          <AdminViewOrder />
        </>
      )}
    </div>
  );
}

export default App;
