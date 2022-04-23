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

function App() {
  const { user } = useSelector((store) => store);
  const navigate = useNavigate();
  const location = useLocation();
  useEffect(() => {
    if (user.id === null && location.pathname === "/tai-khoan") {
      navigate("/dang-nhap");
    }
  }, [user.id, location.pathname]);
  console.log("abc");
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
          <Route path="/dang-nhap" element={<LoginPage />}></Route>
          <Route path="/tai-khoan" element={<Account />} />

          {/* admin */}
          <Route path="/san-pham" element={<Products />}></Route>
          <Route path="/san-pham/:slug" element={<ProductDetail />}></Route>
          <Route path="/tin-tuc" element={<Posts />}></Route>
          <Route path="/tin-tuc/:slug" element={<PostDetail />}></Route>
        </Route>

        <Route path="/admin" element={<SideBar />}>
          <Route path="/admin/dashboard" element={<Dashboard />} />

          <Route
            path="/admin/san-pham/chinh-sua/:slug"
            element={<ProductEdit />}
          />
          <Route path="/admin/san-pham/tao-moi" element={<ProductCreate />} />
          <Route path="/admin/san-pham/thung-rac" element={<ProductTrash />} />
          <Route path="/admin/san-pham/danh-sach" element={<ProductTable />} />
          <Route path="/admin/thuong-hieu/danh-sach" element={<BrandTable />} />
          <Route path="/admin/thuong-hieu/tao-moi" element={<BrandCreate />} />
          <Route
            path="/admin/thuong-hieu/chinh-sua/:slug"
            element={<BrandEdit />}
          />
          <Route path="/admin/thuong-hieu/thung-rac" element={<BrandTrash />} />
          <Route path="/admin/danh-muc/danh-sach" element={<CategoryTable />} />
          <Route path="/admin/danh-muc/tao-moi" element={<CategoryCreate />} />
          <Route
            path="/admin/danh-muc/chinh-sua/:slug"
            element={<CategoryEdit />}
          />
          <Route path="/admin/danh-muc/thung-rac" element={<CategoryTrash />} />
          <Route path="/admin/don-hang/danh-sach" element={<OrderTable />} />
          <Route
            path="/admin/don-hang/chinh-sua/:slug"
            element={<OrderEdit />}
          />
          <Route path="/admin/tin-tuc/danh-sach" element={<PostTable />} />
          <Route path="/admin/tin-tuc/tao-moi" element={<PostCreate />} />
          <Route path="/admin/tin-tuc/chinh-sua/:id" element={<PostEdit />} />
          <Route path="/admin/tin-tuc/da-xoa" element={<PostTrash />} />
        </Route>
        <Route path="*" element={<NotFound />}></Route>
      </Routes>
      <ToastContainer />
    </div>
  );
}

export default App;
