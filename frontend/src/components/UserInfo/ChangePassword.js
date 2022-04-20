import React, { useState } from "react";
import { useSelector } from "react-redux";
import { toast } from "react-toastify";
import api from "../../apis/api";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Input from "../Form/Input/Input";
import Button from "../Button/Button";

const schema = yup
  .object({
    oldpassword: yup.string().required("Thông tin này không được để trống").trim(),
    newpassword: yup
      .string()
      .required("Thông tin này không được để trống")
      .matches(
        /^[a-zA-Z0-9]+$/,
        "Mật khẩu không phân biệt chữ hoa, chữ thường, không được chứa kí tự đặc biệt và khoảng trắng"
      )
      .trim(),
    repassword: yup
      .string()
      .required("Thông tin này không được để trống")
      .oneOf([yup.ref("newpassword"), null], "Mật khẩu không khớp")
      .trim(),
  })
  .required();
function ChangePassword(props) {
  const {
    handleSubmit,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
    reset,
    watch,
  } = useForm({
    mode: "onChange",
    resolver: yupResolver(schema),
  });
  const onSubmitHandler = async (values) => {
    console.log("submit", values);
    await api({
      method: "PUT",
      url: `/user/changepass/${user.id}`,
      data: values,
    })
      .then((res) => {
        console.log(res);
        if (res.status === 200) {
          toast.success(`Đổi mật khẩu thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        } else {
          toast.error(`Đổi mật khẩu thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Đổi mật khẩu thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const { user } = useSelector((store) => store);
  return (
    <div className="flex flex-col w-[100%]  items-center">
      <h2 className="text-lg font-semibold text-gray-700 text-center mb-[40px]">
        ĐỔI MẬT KHẨU
      </h2>
      <div className="w-[400px] gap-y-[20px] flex flex-col">
        <form onSubmit={handleSubmit(onSubmitHandler)}>
          <div className="mt-[25px]">
            <Input
              name="oldpassword"
              type="password"
              label="Mật khẩu cũ"
              control={control}
            />
            <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.oldpassword ? null : "invisible"
            }`}
          >
            {errors?.oldpassword?.message}
          </p>
          </div>
          <div className="mt-[25px]">
            <Input
              name="newpassword"
              type="password"
              label="Mật khẩu mới"
              control={control}
            />
            <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.newpassword ? null : "invisible"
            }`}
          >
            {errors?.newpassword?.message}
          </p>
          </div>
          <div className="mt-[25px]">
            <Input
              name="repassword"
              type="password"
              label="Nhập lại mật khẩu"
              control={control}
            />
            <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.repassword ? null : "invisible"
            }`}
          >
            {errors?.repassword?.message}
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
    </div>
  );
}

export default ChangePassword;
