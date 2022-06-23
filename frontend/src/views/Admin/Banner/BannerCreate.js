import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import imgthumb from "../../../assets/imgthumb.jpg";

const schema = yup
  .object({
    content: yup.string().required("Thông tin này không được để trống").trim(),
    subContent: yup
      .string()
      .required("Thông tin này không được để trống")
      .trim(),
    order: yup.string().required("Thông tin này không được để trống"),
  })
  .required();
function BannerCreate(props) {
  const [image, setImage] = useState(null);
  const [file, setFile] = useState(null);
  const [imgValid, setImgValid] = useState(true);
  const {
    handleSubmit,
    reset,
    setValue,
    getValues,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: {
      published: true,
      content: "",
      subContent:"",
      order: 0,
      linkTo: "/san-pham",
    },
  });
  const handlePreviewImage = (e) => {
    const tempfile = e.target.files[0];

    setFile(tempfile);
    setImage(URL.createObjectURL(tempfile));
    setImgValid(true);
  };
  useEffect(() => {
    return () => {
      image && URL.revokeObjectURL(image);
    };
  }, [image]);
  const onClickResetForm = () => {
    reset({
      content: "",
      subContent: "",
      published: true,
      linkTo: "/san-pham",
      order: 0,
    });
    setImage(null);
    setFile(null);
  };
  const onSubmitHandler = async (values) => {
    if (file === null) {
      setImgValid(false);
      return;
    }
    const formData = new FormData();
    formData.append("content", values.content);
    formData.append("subContent", values.subContent);
    formData.append("published", values.published);
    formData.append("File", file);
    formData.append("order", values.order);
    showLinkTo && formData.append("linkTo", values.linkTo);
    await api({
      method: "POST",
      url: `/banner/create`,
      data: formData,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Thêm thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setImage(null);
          setFile(null);
          setImgValid(true);
          reset({
            content: "",
            subContent: "",
            published: true,
            linkTo: "/san-pham",
          });
        } else {
          toast.error(`Thêm thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thêm thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const [showLinkTo, setShowLinkTo] = useState(false);
  console.log(showLinkTo);
  return (
    <div>
      <form
        onSubmit={handleSubmit(onSubmitHandler)}
        className="flex flex-col gap-y-[20px]"
        autoComplete="off"
      >
        <div className="">
          <AdminInput
            control={control}
            name="content"
            label="Nội dung"
            type="text"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.content ? null : "invisible"
            }`}
          >
            {errors?.content?.message}
          </p>
        </div>
        <div className="">
          <AdminInput
            control={control}
            name="subContent"
            label="Nội dung phụ"
            type="text"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.subContent ? null : "invisible"
            }`}
          >
            {errors?.subContent?.message}
          </p>
        </div>
        <div className="">
          <AdminCheckbox control={control} name="published" label="Phát hành" />
        </div>
        <div>
          <label
            htmlFor="showLinkTo"
            className="inline-flex items-center cursor-pointer"
          >
            <input
              // checkedP
              type="checkbox"
              id="showLinkTo"
              className="form-checkbox hidden"
              value={showLinkTo}
              onChange={(e) => setShowLinkTo(e.target.checked)}
            />
            <div className="checkbox-box bg-white box-content w-[18px] h-[18px] p-[1px] border border-second flex items-center justify-center mr-[10px] rounded-[3px]"></div>
            <span className="block text-sm font-medium text-gray-900 dark:text-gray-300">
              Thêm liên kết
            </span>
          </label>
        </div>
        {showLinkTo && (
          <div className="">
            <AdminInput
              control={control}
              name="linkTo"
              label="Liên kết"
              type="text"
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.linkTo ? null : "invisible"
              }`}
            >
              {errors?.linkTo?.message}
            </p>
          </div>
        )}

        <div className="">
          <AdminInput
            control={control}
            name="order"
            label="Thứ tự"
            type="number"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.order ? null : "invisible"
            }`}
          >
            {errors?.order?.message}
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
            className="w-[300px] h-[300px] overflow-hidden rounded-md cursor-pointer bg-gray-200"
            htmlFor="image"
          >
            <input
              type="file"
              onChange={handlePreviewImage}
              className="hidden"
              id="image"
            />

            <img
              src={image || imgthumb}
              alt=""
              className="w-full h-full object-cover object-center"
            />
          </label>
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              imgValid === false ? null : "invisible"
            }`}
          >
            Thông tin này không được để trống
          </p>
        </div>

        <div className="flex justify-center gap-x-[25px]">
          <button
            className="text-white min-w-[150px] bg-gray-700 hover:bg-gray-800 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center "
            type="button"
            onClick={onClickResetForm}
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

export default BannerCreate;
