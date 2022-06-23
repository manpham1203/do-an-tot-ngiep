import React, { useEffect, useReducer, useState } from "react";
import AsyncSelect from "react-select/async";

import { AiOutlinePlus } from "react-icons/ai";
import { IoMdCloseCircle } from "react-icons/io";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminTextArea from "../../../components/Form/Textarea/AdminTextArea";
import { FaTimes } from "react-icons/fa";
import CKEditor from "ckeditor4-react";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import { useNavigate, useParams } from "react-router-dom";
import ToggleSwitch from "../../../components/ToggleSwitch/ToggleSwitch";
import AddListImage from "../../../components/AddListImage/AddListImage";

const initState = {
  loading: false,
  fail: false,
  data: [],
};
//action
const LOADING = "LOADING";
const SUCCESS = "SUCCESS";
const FAIL = "FAIL";
const loading = () => {
  return {
    type: LOADING,
  };
};
const success = (payload) => {
  return {
    type: SUCCESS,
    payload: payload,
  };
};
const fail = () => {
  return {
    type: FAIL,
  };
};
const reducer = (state, action) => {
  switch (action.type) {
    case LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case SUCCESS:
      return {
        ...state,
        loading: false,
        fail: false,
        data: action.payload,
      };
    case FAIL:
      return {
        ...state,
        loading: false,
        fail: true,
      };
    default:
      return state;
  }
};
const schema = yup
.object({
  name: yup.string().required("Thông tin này không được để trống").trim(),
  price: yup.string().required("Thông tin này không được để trống").trim(),
  quantityInStock: yup.string().required("Thông tin này không được để trống").trim(),
  brandId: yup.object().required("Thông tin này không được để trống"),
  categoryIds: yup.array().required("Thông tin này không được để trống"),

})
.required();
function ProductEdit(props) {
  const {
    handleSubmit,
    reset,
    setValue,
    getValues,
    watch,
    register,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: { published: true, formFile: [] },
  });
  const [state, dispatch] = useReducer(reducer, initState);
  const { slug } = useParams();
  const [image, setImage] = useState([]);
  const [files, setFiles] = useState([]);
console.log(state);
  const onSubmitHandler = async (values) => {
    if(values.priceDiscount===null){
      values.priceDiscount=""
    }
    console.log((values.priceDiscount));
    if (richText === "") {
      return;
    }
    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("description", richText);
    formData.append("published", values.published);
    formData.append("price", parseFloat(values.price));
    if (values.priceDiscount !== "") {
      // formData.append("priceDiscount", null);
      formData.append("priceDiscount", parseFloat(values.priceDiscount));
    }
    formData.append("quantityInStock", parseInt(values.quantityInStock));
    formData.append("brandId", values.brandId.id);
    for (var j = 0; j < values.categoryIds.length; j++) {
      formData.append("categoryIds", values.categoryIds[j].id);
    }
    for (var i = 0; i < files.length; i++) {
      formData.append("Files", files[i]);
    }
    await api({
      method: "PUT",
      url: `/product/${state.data.id}`,
      data: formData,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Chỉnh sửa thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setImage([]);
          setFiles([]);
          fetchData(state.data.slug);
        } else {
          toast.error(`Chỉnh sửa thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Chỉnh sửa thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };

  const fetchData = async (slug) => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/product/productfullgetbyslug/${slug}`,
      data: null,
    })
      .then((res) => {
        dispatch(success(res.data));
        reset({
          name: res.data.name,
          published: res.data.published,
          brandId: res.data.brandVM,
          categoryIds: res.data.categoryVMs,
          price: res.data.price,
          priceDiscount: res.data.priceDiscount,
          quantityInStock: res.data.quantityInStock,
        });
        // setCategorySelected(res.data.categoryVMs)
        setRichText(res.data.description);
      })
      .catch(dispatch(fail()));
  };

  useEffect(() => {
    fetchData(slug);
  }, [slug]);

  const selectFile = (e) => {
    const file = e.target.files;
    const fileArr = Array.from(file);
    const imgArr = fileArr.map((item) => {
      return URL.createObjectURL(item);
    });
    setImage((prevImg) => prevImg.concat(imgArr));
    setFiles((prevImg) => prevImg.concat(fileArr));
  };
  const DeleteImg = (item, index) => {
    setImage(image.filter((e) => e !== item));
    const a1 = files.slice(0, index);
    const a2 = files.slice(index + 1, files.length);
    setFiles(a1.concat(a2));
  };
  const handlepublished = async (id) => {
    await api({
      method: "POST",
      url: `/picture/published/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          fetchData(state.data.slug);
        } else {
          toast.error(`Thao tác thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thao tác thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const handleDelete = async (id) => {
    await api({
      method: "Delete",
      url: `/picture/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Xoá thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(state.data.slug);
        } else {
          toast.error(`Xoá thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Xoá thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  useEffect(() => {
    setRichText(state.data.description);
  }, [state.data]);
  const [richText, setRichText] = useState();
  const fetchDataCategory = async () => {
    return await api({
      method: "GET",
      url: `/category/allcategorynamedeleted`,
      params: { deleted: false },
    }).then((res) => {
      return res.data;
    });
  };
  const fetchDataBrand = async () => {
    return await api({
      method: "GET",
      url: `/brand/allbrandnamedeleted`,
      params: { deleted: false },
    }).then((res) => {
      return res.data;
    });
  };
  const [brandSelected, setBrandSelected] = useState(null);
  const [categorySelected, setCategorySelected] = useState([]);
  console.log(state.data);
  return (
    <div className="">
      <form
        onSubmit={handleSubmit(onSubmitHandler)}
        className="flex flex-col gap-y-[20px]"
        autoComplete="off"
      >
        <div className="">
          <AdminInput
            control={control}
            name="name"
            label="Tên sản phẩm"
            type="text"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.name ? null : "invisible"
            }`}
          >
            {errors?.name?.message}
          </p>
        </div>
        <div className="flex flex-row gap-x-[25px] w-full">
          <div className="w-full">
            <AdminInput
              control={control}
              name="price"
              label="Giá"
              type="text"
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.price ? null : "invisible"
              }`}
            >
              {errors?.price?.message}
            </p>
          </div>
          <div className="w-full">
            <AdminInput
              control={control}
              name="priceDiscount"
              label="Giảm giá"
              type="text"
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.priceDiscount ? null : "invisible"
              }`}
            >
              {errors?.priceDiscount?.message}
            </p>
          </div>
        </div>
        <div className="flex flex-row w-full gap-x-[25px]">
          <div className="w-full">
            <label
              htmlFor="description"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Mô tả đầy đủ
            </label>
            <Controller
              control={control}
              name="categoryIds"
              value={categorySelected}
              onChange={(e) => setCategorySelected(e)}
              render={({
                field: { onChange, onBlur, value, name, ref },
                fieldState: { invalid, isTouched, isDirty, error },
                formState,
              }) => (
                <AsyncSelect
                  cacheOptions
                  name={name}
                  defaultOptions
                  loadOptions={fetchDataCategory}
                  getOptionLabel={(e) => e.name}
                  getOptionValue={(e) => e.id}
                  value={value}
                  onChange={onChange}
                  isMulti
                  className="basic-multi-select"
                  classNamePrefix="select"
                />
              )}
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.categoryIds ? null : "invisible"
              }`}
            >
              {errors?.categoryIds?.message}
            </p>
          </div>
          <div className="w-full">
            <label
              htmlFor="description"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Mô tả đầy đủ
            </label>
            <Controller
              control={control}
              name="brandId"
              value={brandSelected}
              onChange={(e) => setBrandSelected(e)}
              render={({
                field: { onChange, onBlur, value, name, ref },
                fieldState: { invalid, isTouched, isDirty, error },
                formState,
              }) => (
                <AsyncSelect
                  name={name}
                  cacheOptions
                  defaultOptions
                  loadOptions={fetchDataBrand}
                  getOptionLabel={(e) => e.name}
                  getOptionValue={(e) => e.id}
                  value={value}
                  onChange={onChange}
                  className="basic-multi-select"
                  classNamePrefix="select"
                />
              )}
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.brandId ? null : "invisible"
              }`}
            >
              {errors?.brandId?.message}
            </p>
          </div>
        </div>

        <div className="">
          <AdminInput
            control={control}
            name="quantityInStock"
            label="Số lượng"
            type="text"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.quantityInStock ? null : "invisible"
            }`}
          >
            {errors?.quantityInStock?.message}
          </p>
        </div>
        <div className="">
          <AdminCheckbox control={control} name="published" label="Phát hành" />
        </div>

        <div>
          <CKEditor
            onChange={(e) => setRichText(e.editor.getData())}
            data={richText}
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              richText === "" ? null : "invisible"
            }`}
          >
            Thông tin này không được để trống
          </p>
        </div>
        <div className="flex flex-col">
          <div>
            <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
              <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                  <tr>
                    <th scope="col" className="p-4">
                      <div className="flex items-center">
                        <input
                          id="checkbox-all"
                          type="checkbox"
                          className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                        />
                        <label htmlFor="checkbox-all" className="sr-only">
                          checkbox
                        </label>
                      </div>
                    </th>
                    <th scope="col" className="px-6 py-3">
                      Hình
                    </th>
                    <th scope="col" className="px-6 py-3 ">
                      Link
                    </th>
                    <th scope="col" className="px-6 py-3">
                      Phát hành
                    </th>
                    <th scope="col" className="px-6 py-3">
                      Xoá
                    </th>
                  </tr>
                </thead>
                <tbody>
                  {state.data?.pictureVMs?.map((item) => {
                    return (
                      <tr
                        className="bg-white border-b  hover:bg-gray-50 "
                        key={item.id}
                      >
                        <td className="w-4 p-4">
                          <div className="flex items-center">
                            <input
                              id="checkbox-table-1"
                              type="checkbox"
                              className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                            />
                            <label
                              htmlFor="checkbox-table-1"
                              className="sr-only"
                            >
                              checkbox
                            </label>
                          </div>
                        </td>
                        <td className="px-6 py-4 font-medium text-gray-900 dark:text-white whitespace-nowrap">
                          <img
                            src={item.imageSrc}
                            alt=""
                            className="w-[100px] h-[100px]"
                          />
                        </td>
                        <td className="px-6 py-4 ">
                          {item.imageSrc}
                        </td>
                        <td className="px-6 py-4">
                          <div
                            className={`w-[50px] h-[25px]  flex items-center rounded-full relative
                            ${item.published ? "bg-blue-600 " : "bg-gray-300"}
                            transition-all duration-200 cursor-pointer
                            `}
                            onClick={() => handlepublished(item.id)}
                          >
                            <div
                              className={`w-[18px] h-[18px] bg-white rounded-full  absolute
                              ${item.published ? "ml-[28px]" : "ml-[4px]"}
                              transition-all duration-200
                              `}
                            ></div>
                          </div>
                        </td>
                        <td className="px-6 py-4 text-[25px]">
                          <button
                            onClick={() => handleDelete(item.id)}
                            type="button"
                            className="bg-danger text-white p-[2px] rounded-md"
                          >
                            <FaTimes />
                          </button>
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          </div>
        </div>
        <AddListImage
          onChange={selectFile}
          image={image}
          DeleteImg={DeleteImg}
        />

        <div className="flex justify-center gap-x-[25px]">
          <button
            className="text-white min-w-[150px] bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center "
            type="submit"
          >
            LƯU
          </button>
        </div>
      </form>
    </div>
  );
}

export default ProductEdit;
