import React, { useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { toast } from "react-toastify";
import api from "../../../apis/api";

const schema = yup
  .object({
    name: yup
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
      .required("Thông tin này không được để trống")
      .nullable()
      .trim(),
    content: yup
      .string()
      .required("Thông tin này không được để trống")
      .nullable()
      .trim(),
  })
  .required();
function Question(props) {
  document.title = "Liên Hệ Với Chúng Tôi";
  const {
    handleSubmit,
    register,
    reset,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
  } = useForm({
    mode: "onChange",
    resolver: yupResolver(schema),
  });
  const onSubmitHandler = async (values) => {
    console.log(values);
    await api({
      method: "POST",
      url: `question/create`,
      data: values,
    })
      .then((res) => {
        console.log(res);
        if (res.status === 200) {
          toast.success(`Gửi liên hệ thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
          reset({
            name:"",
            email:"",
            content:"",
          })
        } else {
          toast.error(`Thao tác thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 1000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thao tác thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 1000,
        })
      );
  };
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  return (
    <div className="mt-[50px] container mx-auto">
      <div className="w-full flex flex-row gap-x-[25px]">
        <div className="flex-[1]">
          <iframe
            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1003449.6852539204!2d106.13469869341657!3d10.755639034903153!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x317529292e8d3dd1%3A0xf15f5aad773c112b!2zSOG7kyBDaMOtIE1pbmgsIFRow6BuaCBwaOG7kSBI4buTIENow60gTWluaCwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1652457388181!5m2!1svi!2s"
            width="100%"
            height="500px"
            loading="lazy"
            referrerPolicy="no-referrer-when-downgrade"
          ></iframe>
        </div>
        <div className="flex-[1]">
          <h2 className="text-[20px] text-center mb-[50px]">
            GỬI THẮC MẮC CỦA BẠN ĐẾN CHÚNG TÔI
          </h2>
          <form onSubmit={handleSubmit(onSubmitHandler)}>
            <div className="flex flex-row gap-x-[25px]">
              <div className="w-full">
                <div className="w-full relative">
                  <input
                    placeholder=" "
                    {...register("name")}
                    className={`form-input border border-input-border text-input-color font-normal rounded-[4px] w-[100%] h-[50px] px-[20px] transition-all duration-[0.25s] focus:border-second outline-none bg-white`}
                  />
                  <label
                    htmlFor="name"
                    className="form-label absolute left-[20px] top-[50%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-input-label"
                  >
                    Họ và Tên
                  </label>
                </div>
                <p
                  className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                    errors?.name ? null : "invisible"
                  }`}
                >
                  {errors?.name?.message}
                </p>
              </div>
              <div className="w-full">
                <div className="w-full relative">
                  <input
                    placeholder=" "
                    {...register("email")}
                    className={`form-input border border-input-border text-input-color font-normal rounded-[4px] w-[100%] h-[50px] px-[20px] transition-all duration-[0.25s] focus:border-second outline-none bg-white`}
                  />
                  <label
                    htmlFor="email"
                    className="form-label absolute left-[20px] top-[50%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-input-label"
                  >
                    Email
                  </label>
                </div>
                <p
                  className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                    errors?.email ? null : "invisible"
                  }`}
                >
                  {errors?.email?.message}
                </p>
              </div>
            </div>
            <div className="w-full">
              <div className="w-full relative mt-[25px]">
                <textarea
                  placeholder=" "
                  {...register("content")}
                  className="h-[100px] py-[20px] form-input border border-input-border text-input-color font-normal rounded-[4px] w-[100%] px-[20px] transition-all duration-[0.25s] focus:border-second outline-none bg-third"
                />
                <label className="form-label absolute left-[20px] top-[20%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-input-label">
                  Đặt câu hỏi
                </label>
              </div>
              <p
                className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                  errors?.content ? null : "invisible"
                }`}
              >
                {errors?.content?.message}
              </p>
            </div>
            <button
              type="submit"
              className="bg-second text-third px-[20px] h-[40px] mt-[25px]"
            >
              GỬI
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default Question;
