import "./App.css";
import NavBar from "./components/NavBar/NavBar";
import Footer from "./components/Footer/Footer";
import { Route, Routes, useNavigate } from "react-router-dom";
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

function App() {
  const {user}=useSelector(store=>store);
  const navigate=useNavigate();
  return (
    <div className="w-[100%]">
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/" element={<Home />}></Route>
          <Route path="/gioi-thieu" element={<About />}></Route>
          <Route path="/lien-he" element={<Contact />}></Route>
          <Route path="/gio-hang" element={<Cart />}></Route>
          <Route
            path="/thuong-hieu/:brandId"
            element={<ProductsBrand />}
          ></Route>
          <Route
            path="/danh-muc/:categoryId"
            element={<ProductsCategory />}
          ></Route>
          
          {user.id!==null?null:<Route path="/dang-nhap" element={<LoginPage />}></Route>}
          {user.id!==null?<Route path="/tai-khoan" element={<Account />}/>:null}
          
          {/* admin */}
          <Route
            path="/san-pham/:slug"
            element={<ProductDetail />}
          ></Route>
          <Route path="/" element={<Footer />} />
        </Route>

        <Route path="/admin" element={<SideBar />}>
        <Route
            path="/admin/dashboard"
            element={<Dashboard />}
          />
          <Route
            path="/admin/danh-sach-san-pham"
            element={<ProductTable />}
          />
          <Route
            path="/admin/chinh-sua-san-pham/:slug"
            element={<ProductEdit />}
          />
          <Route
            path="/admin/them-san-pham"
            element={<ProductCreate />}
          />
           <Route
            path="/admin/san-pham-da-xoa"
            element={<ProductTrash />}
          />
          <Route
            path="/admin/danh-sach-thuong-hieu"
            element={<BrandTable />}
          />
          <Route
            path="/admin/them-thuong-hieu"
            element={<BrandCreate />}
          />
          <Route
            path="/admin/chinh-sua-thuong-hieu/:slug"
            element={<BrandEdit />}
          />
           <Route
            path="/admin/thuong-hieu-da-xoa"
            element={<BrandTrash />}
          />
          <Route
            path="/admin/danh-sach-danh-muc"
            element={<CategoryTable />}
          />
          <Route
            path="/admin/them-danh-muc"
            element={<CategoryCreate />}
          />
          <Route
            path="/admin/chinh-sua-danh-muc/:slug"
            element={<CategoryEdit />}
          />
           <Route
            path="/admin/danh-muc-da-xoa"
            element={<CategoryTrash />}
          />

        </Route>
        <Route path="*" element={<NotFound />}></Route>
      </Routes>
      <ToastContainer theme={"colored"} />
    </div>
  );
}

export default App;
