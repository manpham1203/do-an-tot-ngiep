import React, { useEffect, useState } from "react";
import api from "../../apis/api";
import { toast } from "react-toastify";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Button from "../../components/Button/Button";
import Input from "../../components/Form/Input/Input";
import useDebounce from "../../hooks/useDebounce";

const schema = yup
  .object({
    firstname: yup
      .string()
      .required("Thông tin này không được để trống")
      .matches(
        /^([a-zA-ZáàãảạăắằẵẳặâấầẫẩậóòõỏọôốồỗổộơớờỡởợíìĩỉịúùũủụưứừữửựéèẽẻẹêếềễểệÁÀÃẢẠĂẮẰẴẲẶÂẤẦẪẨẬÓÒÕỎỌÔỐỒỖỔỘƠỚỜỠỞỢÍÌĨỈỊÚÙŨỦỤƯỨỪỮỬỰÉÈẼẺẸÊẾỀỄỂỆ '])+$/,
        "Họ chỉ được phép các kí tự a-z, A-Z, khoảng trắng, không chứa số và các kí tự đặc biệt"
      )
      .trim(),
    lastname: yup
      .string()
      .required("Thông tin này không được để trống")
      .matches(
        /^([a-zA-ZáàãảạăắằẵẳặâấầẫẩậóòõỏọôốồỗổộơớờỡởợíìĩỉịúùũủụưứừữửựéèẽẻẹêếềễểệÁÀÃẢẠĂẮẰẴẲẶÂẤẦẪẨẬÓÒÕỎỌÔỐỒỖỔỘƠỚỜỠỞỢÍÌĨỈỊÚÙŨỦỤƯỨỪỮỬỰÉÈẼẺẸÊẾỀỄỂỆ '])+$/,
        "Họ chỉ được phép các kí tự a-z, A-Z, khoảng trắng, không chứa số và các kí tự đặc biệt"
      )
      .trim(),
    username: yup
      .string()
      .required("Thông tin này không được để trống")
      .matches(
        /^[a-zA-Z0-9]+$/,
        "Tên đăng nhập không phân biệt chữ hoa, chữ thường, không được chứa kí tự đặc biệt và khoảng trắng"
      )
      .trim(),
    password: yup
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
      .oneOf([yup.ref("password"), null], "Mật khẩu không khớp")
      .trim(),
  })
  .required();
function Register(props) {
  const [date, setDate] = useState(null);
  const [dateFocus, setDateFocus] = useState(false);
  const {
    handleSubmit,
    reset,
    watch,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
  });
  const [check, setCheck] = useState(false);
  const watchUsername = watch("username");
  const query=useDebounce(watchUsername, 800);

  //   check username
  useEffect(() => {
    if (query !== undefined) {
      const fetchData = async (w) => {
        await api({
          method: "POST",
          url: `/Account/findUsername?username=${w}`,
        })
          .then((res) => {
            setCheck(res.data);
          })
          .catch();
      };

      fetchData(query);
    }
  }, [query]);

  // submit form
  const onSubmitHandler = async (values) => {
    console.log(values);
    await api({
      method: "POST",
      url: `/Account/register`,
      data: values,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success(`Đăng ký thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          reset({
            firstname: "",
            lastname: "",
            username: "",
            password: "",
            repassword: "",
          });
          //   navigate(`/dang-nhap`);
        } else {
          toast.error(`Đăng ký thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
        props.setTab(0);
      })
      .catch(() =>
        toast.error(`Đăng nhập thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };

  console.log(isSubmitting);

  return (
    <form onSubmit={handleSubmit(onSubmitHandler)} autoComplete="off">
      <div className="mt-[25px] flex flex-row gap-x-[20px]">
        {/* lastname */}
        <div className="w-full">
          <Input name="lastname" type="text" label="Họ" control={control} />
          <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.lastname ? null : "invisible"
            }`}
          >
            {errors?.lastname?.message}
          </p>
        </div>
        {/* firstname */}
        <div className="w-full">
          <Input name="firstname" type="text" label="Tên" control={control} />
          <p
            className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
              errors?.firstname ? null : "invisible"
            }`}
          >
            {errors?.firstname?.message}
          </p>
        </div>
      </div>
      {/* username */}
      <div className="mt-[18px]">
        <Input
          name="username"
          type="text"
          label="Tên đăng nhập"
          control={control}
        />

        {check === true && (
          <p className="text-red-500 text-sm mt-[2px]">
            Tên đăng nhập đã tồn tại
          </p>
        )}
        <p
          className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
            errors?.username ? null : "invisible"
          }`}
        >
          {errors?.username?.message}
        </p>
      </div>
      {/* password */}
      <div className="mt-[18px]">
        <Input
          name="password"
          type="password"
          label="Mật khẩu"
          control={control}
        />
        <p
          className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
            errors?.password ? null : "invisible"
          }`}
        >
          {errors?.password?.message}
        </p>
      </div>
      {/* password */}
      <div className="mt-[18px]">
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
      
      <div className="mt-[18px] w-full flex justify-center">
        <Button type="submit" outline="true" disabled={isSubmitting}>
          {isSubmitting ? (
            <div className="mx-auto w-5 h-5 border-4 border-main-color border-t-4 border-t-transparent rounded-full animate-spin"></div>
          ) : (
            "Đăng Ký"
          )}
        </Button>
      </div>
    </form>
  );
}

export default Register;
