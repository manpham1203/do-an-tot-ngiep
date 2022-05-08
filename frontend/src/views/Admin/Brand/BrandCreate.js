import React, { useEffect, useState } from "react";
import CKEditor from "ckeditor4-react";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import Input from "../../../components/Form/Input/Input";
import Checkbox from "../../../components/Form/Checkbox/Checkbox";
// import RichText from "../../../components/Form/RichText/RichText";
import File from "../../../components/Form/File/File";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import AdminTextArea from "../../../components/Form/Textarea/AdminTextArea";
import imgthumb from "../../../assets/imgthumb.jpg";

const schema = yup
  .object()
  .shape({
    name: yup.string().required("Thông tin này không được để trống").trim(),
    shortDescription: yup
      .string()
      .required("Thông tin này không được để trống")
      .trim(),
    fullDescription: yup
      .string()
      .required("Thông tin này không được để trống")
      .trim(),
    // image: yup.mixed().required("Thông tin này không được để trống"),
    // .test("fileType", "Định dạng ảnh không hợp lệ", (value) =>
    //   ["image/jpeg", "image/png", "image/jpg"].includes(value.type)
    // ),
  })
  .required();
function BrandCreate(props) {
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
    defaultValues: { published: true, fullDescription: "" },
  });

  const onSubmitHandler = async (values) => {
    if(file==null){
      setValidImg(false);
      return;
    }
    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("FullDescription", values.fullDescription);
    formData.append("ShortDescription", values.shortDescription);
    formData.append("published", values.published);
    formData.append("File", file);
    await api({
      method: "POST",
      url: `/brand`,
      data: formData,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Thêm thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setRichText("");
          setImage(null);
          setFile(null);
          setValidImg(true);
          reset({
            name: "",
            shortDescription: "",
            published: true,
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

  const [image, setImage] = useState();
  const [file, setFile] = useState();
  const handlePreviewImage = (e) => {
    const tempfile = e.target.files[0];
    setFile(tempfile);
    setImage(URL.createObjectURL(tempfile));
    setValidImg(true);
  };
  useEffect(() => {
    return () => {
      image && URL.revokeObjectURL(image);
    };
  }, [image]);

  const onClickResetForm = () => {
    reset({
      name: "",
      shortDescription: "",
      published: true,
    });
    setRichText("");
    setFile(null);
    setImage(null);
    setValidImg(true);
  };
  const [richText, setRichText] = useState();
  useEffect(() => {
    setValue("fullDescription", richText);
  }, [richText]);

  // useEffect(() => {
  //   if (watchImage) {
  //     if (watchImage[0]) {
  //       setImage(URL.createObjectURL(watchImage[0]));
  //     }
  //   }
  // }, [watchImage]);
  const [validImg, setValidImg]=useState(true);
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
          <AdminCheckbox control={control} name="published" label="Phát hành" />
        </div>

        <div>
          {/* <Controller
            control={control}
            name="fullDescription"
            defaultValue=""
            render={({ field }) => <RichText label="Mô tả đầy đủ" {...field} />}
          /> */}
          <div>
            <label
              htmlFor="fullDescription"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Mô tả đầy đủ
            </label>
            <CKEditor
              onChange={(e) => setRichText(e.editor.getData())}
              data={richText}
            />
          </div>
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.shortDescription ? null : "invisible"
            }`}
          >
            {errors?.shortDescription?.message}
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
            className="w-[300px] h-[300px] overflow-hidden rounded-md bg-gray-200 cursor-pointer"
            htmlFor="image"
          >
            <input
              type="file"
              onChange={handlePreviewImage}
              className="hidden"
              // {...register("image")}
              // name="image"
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
              !validImg ? null : "invisible"
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

export default BrandCreate;
