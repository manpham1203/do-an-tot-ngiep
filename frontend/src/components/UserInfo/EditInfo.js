import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import { toast } from "react-toastify";
import { useSelector } from "react-redux";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Input from "../Form/Input/Input";
import Button from "../Button/Button";
import Date from "../Form/Date/Date";
import * as moment from "moment";
import "moment/locale/nl";

const schema = yup
  .object({
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
    email: yup
      .string()
      .email("Sai định dạng email")
      .required("Thông tin này không được để trống").nullable()
      .trim(),
    phoneNumber: yup
      .string()
      .required("Thông tin này không được để trống")
      .matches(/^\d+$/, "Chỉ được phép nhật số").nullable()
      .trim(),
    address: yup.string().required("Thông tin này không được để trống").nullable(),
  })
  .required();
function EditInfo(props) {
  const preFill = {
    firstName: '',
    lastName: '',
    birthday: '',
    email: '',
    phoneNumber: '',
    address: '',
  };
  const {
    handleSubmit,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
    reset,
    watch,
  } = useForm({
    mode: "onChange",
    defaultValues: preFill,
    resolver: yupResolver(schema),
  });
  const onSubmitHandler = async (values) => {
    await api({
      method: "PUT",
      url: `/user/${user.id}`,
      data: values,
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
        toast.error(`Đăng nhập thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const store = useSelector((store) => store);
  const user = store.user;
  const [data, setData] = useState({});

  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/user/${id}`,
    })
      .then((res) => {
        console.log(res);
        reset({
          firstName: res.data.firstName,
          lastName: res.data.lastName,
          birthday: moment(res.data.birthday).format("yyyy-MM-DD"),
          email: res.data.email,
          phoneNumber: res.data.phoneNumber,
          address: res.data.address,
        });
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(user.id);
  }, [user.id]);
  return (
    <div className="w-full flex flex-col">
      <h2 className="text-lg font-semibold text-gray-700 text-center mb-[40px]">
        CẬP NHẬT THÔNG TIN
      </h2>

      <form
        onSubmit={handleSubmit(onSubmitHandler)}
        className="grid grid-cols-1 lg:grid-cols-2 lg:gap-x-[20px] gap-y-[20px]"
      >
        <div className="mt-[25px]">
          <Input name="lastName" type="text" label="Họ" control={control} />
          <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.lastName ? null : "invisible"
            }`}
          >
            {errors?.lastName?.message}
          </p>
        </div>
        <div className="mt-[25px]">
          <Input name="firstName" type="text" label="Tên" control={control} />
          <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.firstName ? null : "invisible"
            }`}
          >
            {errors?.firstName?.message}
          </p>
        </div>
        <div className="mt-[25px]">
          <Input name="email" type="email" label="Email" control={control} />
          <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.email ? null : "invisible"
            }`}
          >
            {errors?.email?.message}
          </p>
        </div>
        <div className="mt-[25px]">
          <Input
            name="phoneNumber"
            type="number"
            label="Số điện thoại"
            control={control}
          />
          <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.phoneNumber ? null : "invisible"
            }`}
          >
            {errors?.phoneNumber?.message}
          </p>
        </div>
        <div className="mt-[25px]">
          <Date label="Ngày sinh" control={control} name="birthday" />
        </div>
        <div className="mt-[25px]">
          <Input name="address" type="text" label="Địa chỉ" control={control} />
          <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.address ? null : "invisible"
            }`}
          >
            {errors?.address?.message}
          </p>
        </div>
        <div className="flex justify-start mt-6">
          <Button type="submit" outline="true" disabled={isSubmitting}>
            {isSubmitting ? (
              <div className="mx-auto w-5 h-5 border-4 border-second border-t-4 border-t-transparent rounded-full animate-spin"></div>
            ) : (
              "Lưu"
            )}
          </Button>
        </div>
      </form>
    </div>
  );
}

export default EditInfo;
