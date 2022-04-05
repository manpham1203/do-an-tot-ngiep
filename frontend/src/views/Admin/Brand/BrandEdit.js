import React, { useEffect, useReducer } from "react";
import { AiOutlinePlus } from "react-icons/ai";
import { IoMdCloseCircle } from "react-icons/io";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminTextArea from "../../../components/Form/Textarea/AdminTextArea";

import { Editor } from "react-draft-wysiwyg";
import { EditorState, convertToRaw } from "draft-js";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import draftToHtml from "draftjs-to-html";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import RichText from "../../../components/Form/RichText/RichText";
import { useParams } from "react-router-dom";

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
    fullDescription: yup
      .string()
      .required("Thông tin này không được để trống")
      .trim(),
  })
  .required();
function BrandEdit(props) {
  const {
    handleSubmit,
    reset,
    watch,
    register,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: { pulished: true, formFile: [] },
  });
  const [state, dispatch] = useReducer(reducer, initState);
  const {slug} =useParams();
  console.log(slug);

  const onSubmitHandler = async (values) => {
    // const formData = new FormData();
    // formData.append("name", values.name);
    // formData.append("FullDescription", values.fullDescription);
    // formData.append("ShortDescription", values.shortDescription);
    // formData.append("Pulished", values.pulished);
    // for (var i = 0; i < files.length; i++) {
    //   formData.append("formFile", files[i]);
    // }
    reset();
    await api({
      method: "PUT",
      url: `/brand`,
      data: values,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Sửa thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          reset({
            name: "",
            shortDescription: "",
            fullDescription: "",
            pulished: true,
          });
        } else {
          toast.error(`Đăng ký thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Đăng nhập thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };

  const fetchData = async (slug) => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/Brand/brandfullgetbyslug/${slug}`,
      data:null,
    })
      .then((res) => {
        console.log(res);
        dispatch(success(res.data));
        reset({
          name:res.data.name,
          shortDescription:res.data.shortDescription,
          fullDescription:res.data.fullDescription,
        })
      })
      .catch(dispatch(fail()));
  };

  useEffect(()=>{
    fetchData(slug)
  }, [slug])


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
            type="text"
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
          <AdminCheckbox control={control} name="pulished" label="Phát hành" />
        </div>
        <div>
          <Controller
            control={control}
            name="fullDescription"
            defaultValue=""
            render={({ field }) => <RichText label="Mô tả đầy đủ" {...field} />}
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.fullDescription ? null : "invisible"
            }`}
          >
            {errors?.fullDescription?.message}
          </p>
        </div>
        <div className="flex flex-col">
          <div>
          {state.data.brandImageVMs.map((item)=>{
            return(
               <img key={item.id} src={item.imageSrc} alt="" className="w-[100px] h-[100px]" />
            )
          })}
           
          </div>
        </div>
        <div>
          <label
            htmlFor="imageUpload"
            className="cursor-pointer flex flex-row w-[300px] h-[50px] bg-gray-300 rounded-xl border-2 border-dashed border-gray-500 justify-center items-center "
          >
            <AiOutlinePlus /> Thêm hình ảnh
          </label>
          <input
            id="imageUpload"
            type="file"
            name="image"
            multiple
            // onChange={selectFile}
            className="hidden"
          />
          <div className="grid grid-cols-6 gap-x-[20px] mt-[25px]"></div>
        </div>

        <div className="flex justify-center gap-x-[25px]">
          <button
            className="text-white min-w-[150px] bg-gray-700 hover:bg-gray-800 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center "
            type="button"
          >
            LÀM LẠI
          </button>
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

export default BrandEdit;
