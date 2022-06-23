import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../../../apis/api";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import defaultuser from "../../../assets/defaultuser.png";
import { FiCamera } from "react-icons/fi";
import { MdClose } from "react-icons/md";
import * as moment from "moment";
import "moment/locale/nl";
import { toast } from "react-toastify";

const schema = yup.object({
  firstName: yup
    .string()
    .required("Thông tin này không được để trống")
    .matches(
      /^([a-zA-ZáàãảạăắằẵẳặâấầẫẩậóòõỏọôốồỗổộơớờỡởợíìĩỉịúùũủụưứừữửựéèẽẻẹêếềễểệÁÀÃẢẠĂẮẰẴẲẶÂẤẦẪẨẬÓÒÕỎỌÔỐỒỖỔỘƠỚỜỠỞỢÍÌĨỈỊÚÙŨỦỤƯỨỪỮỬỰÉÈẼẺẸÊẾỀỄỂỆ '])+$/,
      "Họ chỉ được phép các kí tự a-z, A-Z, khoảng trắng, không chứa số và các kí tự đặc biệt"
    )
    .trim(),
  lastName: yup
    .string()
    .required("Thông tin này không được để trống")
    .matches(
      /^([a-zA-ZáàãảạăắằẵẳặâấầẫẩậóòõỏọôốồỗổộơớờỡởợíìĩỉịúùũủụưứừữửựéèẽẻẹêếềễểệÁÀÃẢẠĂẮẰẴẲẶÂẤẦẪẨẬÓÒÕỎỌÔỐỒỖỔỘƠỚỜỠỞỢÍÌĨỈỊÚÙŨỦỤƯỨỪỮỬỰÉÈẼẺẸÊẾỀỄỂỆ '])+$/,
      "Họ chỉ được phép các kí tự a-z, A-Z, khoảng trắng, không chứa số và các kí tự đặc biệt"
    )
    .trim(),
  email: yup.string().email("Sai định dạng email").nullable().trim(),
  // birthday: yup
  //   .string("Thông tin này không được để trống")
  //   .required("Thông tin này không được để trống")
  //   .nullable(),
  phoneNumber: yup.string().nullable().trim(),
  address: yup
    .string()
    .required("Thông tin này không được để trống")
    .nullable(),
});
function UserEdit(props) {
  const [data, setData] = useState();
  const { id } = useParams();
  const [image, setImage] = useState();
  const [file, setFile] = useState();
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
      phoneNumber: "",
      email: "",
      address: "",
      birthday: "",
      firstName: "",
      lastName: "",
    },
  });
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/user/userdetail`,
      params: {
        id: id,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          setData(res.data);
          reset({
            firstName: res.data.firstName,
            lastName: res.data.lastName,
            email: res.data.email || "",
            phoneNumber: res.data.phoneNumber || "",
            address: res.data.address || "",
            published: res.data.published || "",
            birthday: moment(res.data.birthday).format("yyyy-MM-DD") || "",
          });
          setImage(res.data.imageSrc);
        }
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(id);
  }, [id]);
  const onSubmitHandler = async (values) => {
    console.log(values);
    const formData = new FormData();
    formData.append("firstName", values.firstName);
    formData.append("lastName", values.lastName);
    formData.append("email", values.email);
    formData.append("published", values.published);
    formData.append("address", values.address);
    formData.append("phoneNumber", values.phoneNumber);
    values.birthday instanceof Date &&
      formData.append("birthday", moment(values.birthday).format("yyyy-MM-DD"));

    formData.append("File", file);
    await api({
      method: "PUT",
      url: `/user/update/${data.id}`,
      data: formData,
    })
      .then((res) => {
        console.log(res);
        if (res.status === 200) {
          toast.success(`Chỉnh sửa thông tin thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        } else {
          toast.error(`Chỉnh sửa thông tin thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Chỉnh sửa thông tin thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
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
    setFile(undefined);
  };
  return (
    <div>
      <form
        onSubmit={handleSubmit(onSubmitHandler)}
        className="flex flex-col gap-y-[20px]"
        autoComplete="off"
      >
        <div className="flex flex-col items-center w-full">
          <div className="relative w-fit">
            <div className="w-[200px] h-[200px] block border border-second overflow-hidden">
              <img
                src={image || defaultuser}
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
        <div className="flex flex-row gap-x-[20px] w-full">
          <div className="flex-[1]">
            <AdminInput
              control={control}
              name="lastName"
              label="Họ"
              type="text"
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.lastName ? null : "invisible"
              }`}
            >
              {errors?.lastName?.message}
            </p>
          </div>
          <div className="flex-[1]">
            <AdminInput
              control={control}
              name="firstName"
              label="Tên"
              type="text"
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.firstName ? null : "invisible"
              }`}
            >
              {errors?.firstName?.message}
            </p>
          </div>
        </div>
        <div className="">
          <AdminInput
            control={control}
            name="email"
            label="Email"
            type="text"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.email ? null : "invisible"
            }`}
          >
            {errors?.email?.message}
          </p>
        </div>
        <div className="">
          <AdminInput
            control={control}
            name="phoneNumber"
            label="Số điện thoại"
            type="number"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.phoneNumber ? null : "invisible"
            }`}
          >
            {errors?.phoneNumber?.message}
          </p>
        </div>
        <div className="">
          <AdminInput
            control={control}
            name="birthday"
            label="Ngày sinh"
            type="date"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.birthday ? null : "invisible"
            }`}
          >
            {errors?.birthday?.message}
          </p>
        </div>
        <div className="">
          <AdminCheckbox control={control} name="published" label="Phát hành" />
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

export default UserEdit;
