import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import { useParams } from "react-router-dom";
import { FiCamera } from "react-icons/fi";
import { MdClose } from "react-icons/md";

const schema = yup
  .object({
    content: yup.string().required("Thông tin này không được để trống").trim(),
    subContent: yup
      .string()
      .required("Thông tin này không được để trống")
      .trim(),
  })
  .required();
function BannerEdit(props) {
  const { id } = useParams();
  const [data, setData] = useState();
  const [image, setImage] = useState(null);
  const [file, setFile] = useState(null);
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
      fullDescription: "",
    },
  });
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
  const handleResetImage = () => {
    setImage(data.imageSrc);
    setFile(null);
  };
  const onSubmitHandler = async (values) => {
    const formData = new FormData();
    formData.append("content", values.content);
    formData.append("subContent", values.subContent);
    formData.append("published", values.published);
    formData.append("File", file);
    await api({
      method: "PUT",
      url: `/banner/update/${id}`,
      data: formData,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Cập nhật thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          fetchData(id);
        } else {
          toast.error(`Cập nhật thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Cập nhật thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const fetchData = async (i) => {
    await api({
      method: "GET",
      url: `/banner/getbyid`,
      params: { id: i },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
          reset({
            content: res.data.content,
            subContent: res.data.subContent,
            published: res.data.published,
          });
          setImage(res.data.imageSrc);
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(id);
  }, [id]);
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

        <div className="flex flex-col">
          <div className="relative w-fit">
            <div className="w-[200px] h-[200px] block border border-second overflow-hidden">
              <img
                src={image}
                alt=""
                className="w-full h-full object-cover object-center"
              />
              <label
                htmlFor="image"
                className="cursor-pointer absolute top-0 right-[-20px] w-[40px] h-[40px] bg-third rounded-full border border-second flex justify-center items-center text-[20px]"
              >
                <FiCamera />
              </label>
              <span
                onClick={() => handleResetImage()}
                className="cursor-pointer absolute top-[50px] right-[-20px] w-[40px] h-[40px] bg-third rounded-full border border-second flex justify-center items-center text-[20px]"
              >
                <MdClose />
              </span>
            </div>
          </div>
          <label
            htmlFor="image"
            className="block mb-2 text-sm font-medium text-gray-900 "
          >
            Chọn ảnh
          </label>
          <input
            type="file"
            onChange={handlePreviewImage}
            className="hidden"
            id="image"
          />
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

export default BannerEdit;
