import React, { useEffect, useState } from "react";
import CKEditor from "ckeditor4-react";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import AdminTextArea from "../../../components/Form/Textarea/AdminTextArea";
import AddListImage from "../../../components/AddListImage/AddListImage";

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
function CategoryCreate(props) {
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
    defaultValues: { pulished: true, formFile: [], fullDescription: "" },
  });

  const onSubmitHandler = async (values) => {
    console.log(values);
    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("FullDescription", values.fullDescription);
    formData.append("ShortDescription", values.shortDescription);
    formData.append("Pulished", values.pulished);
    for (var i = 0; i < files.length; i++) {
      formData.append("Files", files[i]);
    }
    await api({
      method: "POST",
      url: `/category`,
      data: formData,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Thêm thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          setRichText("");
          setImage([]);
          setFiles([]);
          reset({
            name: "",
            shortDescription: "",
            pulished: true,
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
      pulished: true,
    });
    setImage([]);
    setFiles([]);
    setRichText("");
  };
  const [richText, setRichText] = useState();
  useEffect(() => {
    setValue("fullDescription", richText);
  }, [richText]);

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
            label="Tên danh mục"
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
        <AddListImage
          onChange={selectFile}
          image={image}
          DeleteImg={DeleteImg}
        />

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

export default CategoryCreate;
