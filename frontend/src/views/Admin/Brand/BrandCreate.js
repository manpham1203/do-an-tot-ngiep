import React, { useEffect, useState } from "react";
import { Editor } from "react-draft-wysiwyg";
import { EditorState, convertToRaw } from "draft-js";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import draftToHtml from "draftjs-to-html";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import Input from "../../../components/Form/Input/Input";
import Checkbox from "../../../components/Form/Checkbox/Checkbox";
import RichText from "../../../components/Form/RichText/RichText";
import File from "../../../components/Form/File/File";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import AdminTextArea from "../../../components/Form/Textarea/AdminTextArea";
import { AiOutlinePlus } from "react-icons/ai";
import { IoMdCloseCircle } from "react-icons/io";

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
function BrandCreate(props) {
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

  const onSubmitHandler = async (values) => {
    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("FullDescription", values.fullDescription);
    formData.append("ShortDescription", values.shortDescription);
    formData.append("Pulished", values.pulished);
    for (var i = 0; i < files.length; i++) {
      formData.append("formFile", files[i]);
    }
    reset();
    await api({
      method: "POST",
      url: `/brand`,
      data: formData,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Đăng ký thành công`, {
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

  const [editorState, setEditorState] = useState(() =>
    EditorState.createEmpty()
  );
  useEffect(() => {
    console.log(draftToHtml(convertToRaw(editorState.getCurrentContent())));
  }, [editorState]);

  const [image, setImage] = useState([]);
  const [files, setFiles] = useState([]);
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

  const onClickResetForm = () => {
    reset({
      name: "",
      shortDescription: "",
      fullDescription: "",
      pulished: true,
    });
    setImage([]);
    setFiles([]);
  };

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
        <div>
          {/* <File control={control} name="image"  /> */}
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
            // {...register("image")}
            multiple
            onChange={selectFile}
            className="hidden"
          />
          <div className="grid grid-cols-6 gap-x-[20px] mt-[25px]">
            {image &&
              image.map((item, index) => {
                return (
                  <div
                    className="w-[full] h-[full] flex flex-col relative"
                    key={index}
                  >
                    <img
                      src={item}
                      alt={item}
                      className="w-[full] h-[full] object-cover object-center"
                    />
                    <IoMdCloseCircle
                      onClick={() => DeleteImg(item, index)}
                      className="absolute top-0 right-0 text-white text-[25px] hover:text-danger cursor-pointer transition-all duration-200"
                    />
                  </div>
                );
              })}
          </div>
        </div>

        <div className="flex justify-center gap-x-[25px]">
          <button
            className="text-white min-w-[150px] bg-gray-700 hover:bg-gray-800 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center "
            type="button"
            onClick={()=>reset()}
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
