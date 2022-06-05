import "./App.css";
import { Route, Routes, useLocation, useNavigate } from "react-router-dom";
import Home from "./views/Client/Home/Home";
import About from "./views/Client/About/About";
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
import { useEffect } from "react";
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
import OverlayAdminViewCmt from "./components/Overlay/OverlayAdminViewCmt";
import OverlayAdminViewBrand from "./components/Overlay/OverlayAdminViewBrand";
import OverlayAdminViewPost from "./components/Overlay/OverlayAdminViewPost";
import OverlayAdminViewUser from "./components/Overlay/OverlayAdminViewUser";
import AdminViewCmt from "./components/Modal/AdminViewCmt";
import AdminViewBrand from "./components/Modal/AdminViewBrand";
import AdminViewPost from "./components/Modal/AdminViewPost";
import AdminViewUser from "./components/Modal/AdminViewUser";
import UserTable from './views/Admin/User/UserTable'
import UserEdit from './views/Admin/User/UserEdit'
import UserTrash from './views/Admin/User/UserTrash'
import QuestionTable from './views/Admin/Question/QuestionTable'
import LoginAdmin from "./views/Admin/Login/LoginAdmin";
import Question from "./views/Client/Question/Question";
import ContactCreate from "./views/Admin/Contact/ContactCreate";
import ContactTable from "./views/Admin/Contact/ContactTable";
import ContactTrash from "./views/Admin/Contact/ContactTrash";
import ContactEdit from "./views/Admin/Contact/ContactEdit";
import PageCreate from "./views/Admin/Page/PageCreate";
import PageTable from "./views/Admin/Page/PageTable";
import PageEdit from "./views/Admin/Page/PageEdit";
import PageTrash from "./views/Admin/Page/PageTrash";
import Page from "./views/Client/Page/Page";


function App() {
  const {
    user,
    quickView,
    adminViewProduct,
    adminViewOrder,
    adminViewCmt,
    adminViewBrand,
    adminViewPost,
    adminViewUser,
    cursor
  } = useSelector((store) => store);
  useEffect(() => {
    if (adminViewUser.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [adminViewUser.show]);
  useEffect(() => {
    if (adminViewBrand.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [adminViewBrand.show]);
  useEffect(() => {
    if (adminViewPost.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [adminViewPost.show]);
  useEffect(() => {
    if (quickView.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [quickView.show]);
  useEffect(() => {
    if (adminViewOrder.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [adminViewOrder.show]);
  useEffect(() => {
    if (adminViewProduct.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [adminViewProduct.show]);
  useEffect(() => {
    if (adminViewCmt.show) {
      document.body.style.overflowY = "hidden";
    } else {
      document.body.style.overflowY = "scroll";
    }
  }, [adminViewCmt.show]);
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

  return (
    <div className={`w-[100%] min-h-screen ${cursor.wait && "cursor-wait"}`}>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/" element={<Home />}></Route>
          <Route path="/gioi-thieu" element={<About />}></Route>
          <Route path="/lien-he" element={<Question />}></Route>
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
          <Route path="/trang/:slug" element={<Page />}></Route>
        </Route>

        {user.role !== 1 && (
          <Route path="/admin-login" element={<LoginAdmin />}></Route>
        )}

        {user.role === 1 && (
          <Route path="/admin" element={<SideBar />}>
            <Route path="/admin" element={<Dashboard />} />
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
            <Route
              path="/admin/don-hang/chua-xu-li"
              element={<OrderPending />}
            />
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
            <Route path="/admin/nguoi-dung/danh-sach" element={<UserTable />} />
            <Route path="/admin/nguoi-dung/chinh-sua/:id" element={<UserEdit />} />
            <Route path="/admin/nguoi-dung/da-xoa" element={<UserTrash />} />
            <Route path="/admin/cau-hoi/danh-sach" element={<QuestionTable />} />
            <Route path="/admin/lien-he/tao-moi" element={<ContactCreate />} />
            <Route path="/admin/lien-he/danh-sach" element={<ContactTable />} />
            <Route path="/admin/lien-he/da-xoa" element={<ContactTrash />} />
            <Route path="/admin/lien-he/chinh-sua/:id" element={<ContactEdit />} />
            <Route path="/admin/trang/tao-moi" element={<PageCreate />} />
            <Route path="/admin/trang/danh-sach" element={<PageTable />} />
            <Route path="/admin/trang/chinh-sua/:id" element={<PageEdit />} />
            <Route path="/admin/trang/da-xoa" element={<PageTrash />} />
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
      {adminViewCmt.show && (
        <>
          <OverlayAdminViewCmt />
          <AdminViewCmt />
        </>
      )}
      {adminViewBrand.show && (
        <>
          <OverlayAdminViewBrand />
          <AdminViewBrand />
        </>
      )}
      {adminViewPost.show && (
        <>
          <OverlayAdminViewPost />
          <AdminViewPost />
        </>
      )}
      {adminViewUser.show && (
        <>
          <OverlayAdminViewUser />
          <AdminViewUser />
        </>
      )}
    </div>
  );
}

export default App;
