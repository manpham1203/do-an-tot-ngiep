import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import Select from "react-select";
import CKEditor from "ckeditor4-react";

const schema = yup
  .object({
    title: yup.string().required("Thông tin này không được để trống").trim(),
    content: yup.string().required("Thông tin này không được để trống").nullable(),
    type: yup.object().required("Thông tin này không được để trống").nullable(),
  })
  .required();
function PageCreate(props) {
  const {
    handleSubmit,
    reset,
    setValue,
    getValues,
    register,
    trigger,
    resetField,
    watch,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: {
      published: true,
    },
  });
  const typeOptions = [
    { value: "about", label: "Giới thiệu" },
    { value: "policy", label: "Chính sách" },
    { value: "guide", label: "Hướng dẫn" },
  ];
  const onClickResetForm = () => {
    reset({
      title: "",
      content: "",
      type: null,
      published: true,
    });
  };
  const onSubmitHandler = async (values) => {
    await api({
      method: "POST",
      url: `/page`,
      data: {
        title: values.title,
        content: values.content,
        type: values.type.value,
        published: values.published,
      },
    })
      .then((res) => {
        if (res.status === 201) {
          reset({
            title: "",
            content: undefined,
            type: null,
            published: true,
          });
          toast.success(`Thêm thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
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
            name="title"
            label="Tên Trang"
            type="text"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.title ? null : "invisible"
            }`}
          >
            {errors?.title?.message}
          </p>
        </div>

        <div className="">
          <label
            htmlFor=""
            className="block mb-2 text-sm font-medium text-gray-900 "
          >
            Loại trang
          </label>
          <Controller
            control={control}
            name="type"
            // name="type"
            // value={type}
            // onChange={(e) => setType(e)}
            render={({
              field: { onChange, onBlur, value, name, ref },
              fieldState: { invalid, isTouched, isDirty, error },
              formState,
            }) => (
              <Select
                className="min-w-[150px] cursor-pointer"
                classNamePrefix="select"
                isClearable={false}
                isSearchable={false}
                value={value}
                onChange={onChange}
                options={typeOptions}
                getOptionLabel={(e) => e.label}
                getOptionValue={(e) => e.value}
                placeholder="Loại trang"
              />
            )}
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.type ? null : "invisible"
            }`}
          >
            {errors?.type?.message}
          </p>
        </div>
        <div className="">
          <AdminCheckbox control={control} name="published" label="Phát hành" />
        </div>
        <div>
          <label
            htmlFor="fullDescription"
            className="block mb-2 text-sm font-medium text-gray-900 "
          >
            Mô tả đầy đủ
          </label>
          <CKEditor
            name="content"
            onChange={(e) => {
              setValue("content", e.editor.getData());
              trigger("content");
            }}
            data={watch("content")}
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.content ? null : "invisible"
            }`}
          >
            {errors?.content?.message}
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

export default PageCreate;
