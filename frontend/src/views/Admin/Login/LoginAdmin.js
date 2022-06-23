import React, { useEffect } from "react";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useNavigate } from "react-router-dom";
import Button from "../../../components/Button/Button";
import Input from "../../../components/Form/Input/Input";
import { login } from "../../../redux/user/userActions";
import { useDispatch } from "react-redux";

const schema = yup
  .object({
    username: yup.string().required("Thông tin này không được để trống"),
    password: yup.string().required("Thông tin này không được để trống"),
  })
  .required();
function LoginAdmin(props) {
  const {
    handleSubmit,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
  });
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const onSubmitHandler = async (values) => {
    await api({
      method: "POST",
      url: `/user/LoginAdmin`,
      data: values,
    })
      .then((res) => {
        if (res.status === 200 && typeof res.data !== "string") {
          toast.success(`Đăng nhập thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          const obj = { id: res.data.id, role: res.data.role };
          dispatch(login(obj));
          navigate(`/admin`);
        } else {
          toast.warn(`Sai tên đăng nhập hoặc mật khẩu`, {
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
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return (
    <div className="w-[500px] mx-auto pt-[30px]">
    <h2 className="text-center text-[30px]">ĐĂNG NHẬP</h2>
      <form onSubmit={handleSubmit(onSubmitHandler)}>
        <div className="mt-[25px]">
          <Input
            name="username"
            type="text"
            label="Tên đăng nhập"
            control={control}
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.username ? null : "invisible"
            }`}
          >
            {errors?.username?.message}
          </p>
        </div>
        <div className="mt-[18px]">
          <Input
            name="password"
            type="password"
            label="Mật khẩu"
            control={control}
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.password ? null : "invisible"
            }`}
          >
            {errors?.password?.message}
          </p>
        </div>
        <div className="mt-[18px] w-full flex justify-center">
          <Button type="submit" outline="true" disabled={isSubmitting}>
            {isSubmitting ? (
              <div className="mx-auto w-5 h-5 border-4 border-second border-t-4 border-t-transparent rounded-full animate-spin"></div>
            ) : (
              "Đăng Nhập"
            )}
          </Button>
        </div>
      </form>
    </div>
  );
}

export default LoginAdmin;
