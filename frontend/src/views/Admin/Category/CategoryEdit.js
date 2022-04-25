import React, { useEffect, useReducer, useState } from "react";
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
    shortDescription: yup
      .string()
      .required("Thông tin này không được để trống")
      .trim(),
    // fullDescription: yup
    //   .string()
    //   .required("Thông tin này không được để trống")
    //   .trim(),
  })
  .required();
function CategoryEdit(props) {
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
    defaultValues: { published: true, formFile: []},
  });
  const [state, dispatch] = useReducer(reducer, initState);
  const { slug } = useParams();

  const onSubmitHandler = async (values) => {
    if(richText===""){
      return;
    }
    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("FullDescription", richText);
    formData.append("ShortDescription", values.shortDescription);
    formData.append("published", values.published);
      formData.append("File", file);
    await api({
      method: "PUT",
      url: `/category/${state.data.id}`,
      data: formData,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Chỉnh sửa thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setImage(null);
          setFile(null);
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
      url: `/category/categoryfullgetbyslug/${slug}`,
      data: null,
    })
      .then((res) => {
        dispatch(success(res.data));
        reset({
          name: res.data.name,
          shortDescription: res.data.shortDescription,
          published: res.data.published,
        });
        setRichText(res.data.fullDescription)
      })
      .catch(dispatch(fail()));
  };

  useEffect(() => {
    fetchData(slug);
  }, [slug]);

  const [image, setImage] = useState(null);
  const [file, setFile] = useState(null);
  const handlePreviewImage = (e) => {
    const tempfile = e.target.files[0];
    setFile(tempfile);
    setImage(URL.createObjectURL(tempfile));
  };
  useEffect(() => {
    return () => {
      image && URL.revokeObjectURL(image);
    };
  }, [image]);
  useEffect(()=>{
    setRichText(state.data.fullDescription)
  }, [state.data])
  const [richText, setRichText]=useState();
  console.log(state);
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
            label="Tên thương hiệu"
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
        <div className="">
          <AdminTextArea
            control={control}
            name="shortDescription"
            label="Mô tả ngắn"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.shortDescription ? null : "invisible"
            }`}
          >
            {errors?.shortDescription?.message}
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
              richText==="" ? null : "invisible"
            }`}
          >
            Thông tin này không được để trống
          </p>
        </div>

        <div className="flex flex-col">
          <label
            htmlFor="image"
            className="block mb-2 text-sm font-medium text-gray-900 "
          >
            Chọn ảnh
          </label>
          <label
            className="w-[370px] h-[246px] overflow-hidden rounded-md bg-[url('assets/postthumb.jpg')] bg-center bg-cover cursor-pointer"
            htmlFor="image"
          >
            <input
              type="file"
              onChange={handlePreviewImage}
              className="hidden"
              id="image"
            />
            {image && (
              <img
                src={image}
                alt=""
                className="w-full h-full object-cover object-center"
              />
            )}
          </label>
        </div>

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

export default CategoryEdit;
