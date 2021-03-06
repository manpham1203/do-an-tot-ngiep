import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import api from "../../../apis/api";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import AdminInput from "../../../components/Form/Input/AdminInput";
import AdminCheckbox from "../../../components/Form/Checkbox/AdminCheckbox";
import { useParams } from "react-router-dom";
import Select from "react-select";

const schema = yup
  .object({
    content: yup.string().required("Thông tin này không được để trống").trim(),
    type: yup.object().required("Thông tin này không được để trống").nullable(),
  })
  .required();
function ContactEdit(props) {
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
    },
  });
  const { id } = useParams();
  const [data, setData] = useState();
  const typeOptions = [
    { value: "number", label: "Số điện thoại" },
    { value: "email", label: "Email" },
    { value: "address", label: "Địa chỉ" },
  ];
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/contact/getbyid`,
      params: { id: id },
    })
      .then((res) => {
        setData(res.data);
        reset({
          content: res.data.content,
          published: res.data.published,
          type:
            res.data.type === typeOptions[0].value
              ? typeOptions[0]
              : res.data.type === typeOptions[1].value
              ? typeOptions[1]
              : typeOptions[2],
        });
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData(id);
  }, [id]);
  const onSubmitHandler = async (values) => {
    // formData.append("content", values.content);
    // formData.append("type", values.type.value);
    // formData.append("published", values.published);
    await api({
      method: "PUT",
      url: `/contact/update/${id}`,
      data: {
        content: values.content,
        type: values.type.value,
        published: values.published,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          toast.success(`Chỉnh sửa thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        } else {
          toast.error(`Chỉnh sửa thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Chỉnh sửa thất bại`, {
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
          <AdminCheckbox control={control} name="published" label="Phát hành" />
        </div>

        <div className="">
          <label
            htmlFor=""
            className="block mb-2 text-sm font-medium text-gray-900 "
          >
            Loại liên hệ
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
                // defaultValue={orderOptions[0]}
                isClearable={false}
                isSearchable={false}
                value={value}
                onChange={onChange}
                // name={name}
                // value={value}
                // onChange={onChange}
                options={typeOptions}
                getOptionLabel={(e) => e.label}
                getOptionValue={(e) => e.value}
                placeholder="Loại liên hệ"
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

export default ContactEdit;
