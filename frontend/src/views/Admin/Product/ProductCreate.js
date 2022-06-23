import React, { createFactory, useEffect, useState } from "react";
import AsyncSelect from "react-select/async";
// import { colourOptions } from "../data";
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
    price: yup
      .number()
      .transform((currentValue, originalValue) => {
        return originalValue === "" ? null : currentValue;
      })
      .required("Thông tin này không được để trống")
      .nullable(true),
    priceDiscount: yup
      .number()
      .max(yup.ref("price"), "Giá giảm phải nhỏ hơn giá bán")
      .transform((currentValue, originalValue) => {
        return originalValue === "" ? null : currentValue;
      })
      .nullable(),
    quantity: yup
      .number()
      .required("Thông tin này không được để trống")
      .transform((currentValue, originalValue) => {
        return originalValue === "" ? null : currentValue;
      }),
    brandId: yup
      .object()
      .required("Thông tin này không được để trống")
      .nullable(),
    categoryIds: yup
      .array()
      .required("Thông tin này không được để trống")
      .nullable(),
   
    // description: yup
    //   .string()
    //   .required("Thông tin này không được để trống")
    //   .trim(),
  })
  .required();
function ProductCreate(props) {
  const {
    handleSubmit,
    watch,
    reset,
    setValue,
    getValues,
    register,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: {
      published: true,
      description: "",
      quantity: 1,
    },
  });

  const onSubmitHandler = async (values) => {
    
    if (files.length === 0) {
      setValidImg(false);
      return;
    }
    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("description", values.description);
    formData.append("published", values.published);
    formData.append("price", parseFloat(values.price));
    values.priceDiscount != null &&
      formData.append("priceDiscount", values.priceDiscount);
    formData.append("brandId", values.brandId.id);
    for (var j = 0; j < values.categoryIds.length; j++) {
      formData.append("categoryIds", values.categoryIds[j].id);
    }
    // formData.append("categoryIds", values.categoryIds[0]);
    formData.append("quantityInStock", parseInt(values.quantity));
    for (var i = 0; i < files.length; i++) {
      formData.append("Files", files[i]);
    }
    await api({
      method: "POST",
      url: `/product`,
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
          setValidImg(true);
          reset({
            name: "",
            published: true,
          });
          setBrandSelected(null);
          setCategorySelected([]);
          reset({
            brandId: null,
            categoryIds: null,
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
    setValidImg(true);
  };

  const DeleteImg = (item, index) => {
    setImage(image.filter((e) => e !== item));
    const a1 = files.slice(0, index);
    const a2 = files.slice(index + 1, files.length);
    setFiles(() => a1.concat(a2));

    if (a1.concat(a2).length === 0) {
      console.log("voday");
      setValidImg(false);
    }
  };
  console.log(files);
  const onClickResetForm = () => {
    reset({
      name: "",
      published: true,
    });
    setImage([]);
    setFiles([]);
    setRichText();
    setValidImg(true);
    setBrandSelected(null);
    setCategorySelected([]);
    reset({
      brandId: null,
      categoryIds: null,
    });
  };
  const [richText, setRichText] = useState();
  useEffect(() => {
    setValue("description", richText);
  }, [richText]);
  console.log(richText);
  console.log(errors);

  const fetchDataCategory = async () => {
    return await api({
      method: "GET",
      url: `/category/allcategorynamedeleted`,
      params: { deleted: false },
    }).then((res) => {
      return res.data;
    });
  };
  const fetchDataBrand = async () => {
    return await api({
      method: "GET",
      url: `/brand/allbrandnamedeleted`,
      params: { deleted: false },
    }).then((res) => {
      return res.data;
    });
  };
  const [brandSelected, setBrandSelected] = useState(null);
  const [categorySelected, setCategorySelected] = useState([]);
  const [validImg, setValidImg] = useState(true);
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
            label="Tên sản phẩm"
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
        <div className="flex flex-row gap-x-[25px] w-full">
          <div className="w-full">
            <AdminInput
              control={control}
              name="price"
              label="Giá"
              type="number"
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.price ? null : "invisible"
              }`}
            >
              {errors?.price?.message}
            </p>
          </div>
          <div className="w-full">
            <AdminInput
              control={control}
              name="priceDiscount"
              label="Giảm giá"
              type="number"
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.priceDiscount ? null : "invisible"
              }`}
            >
              {errors?.priceDiscount?.message}
            </p>
          </div>
        </div>
        <div className="flex flex-row w-full gap-x-[25px]">
          <div className="w-full">
            <label
              htmlFor="description"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Danh mục
            </label>
            <Controller
              control={control}
              name="categoryIds"
              value={categorySelected}
              onChange={(e) => setCategorySelected(e)}
              render={({
                field: { onChange, onBlur, value, name, ref },
                fieldState: { invalid, isTouched, isDirty, error },
                formState,
              }) => (
                <AsyncSelect
                  cacheOptions
                  name={name}
                  defaultOptions
                  loadOptions={fetchDataCategory}
                  getOptionLabel={(e) => e.name}
                  getOptionValue={(e) => e.id}
                  value={value}
                  onChange={onChange}
                  isMulti
                  className="basic-multi-select"
                  classNamePrefix="select"
                />
              )}
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.categoryIds ? null : "invisible"
              }`}
            >
              {errors?.categoryIds?.message}
            </p>
          </div>
          <div className="w-full">
            <label
              htmlFor="description"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Thương hiệu
            </label>
            <Controller
              control={control}
              name="brandId"
              value={brandSelected}
              onChange={(e) => setBrandSelected(e)}
              render={({
                field: { onChange, onBlur, value, name, ref },
                fieldState: { invalid, isTouched, isDirty, error },
                formState,
              }) => (
                <AsyncSelect
                  name={name}
                  cacheOptions
                  defaultOptions
                  loadOptions={fetchDataBrand}
                  getOptionLabel={(e) => e.name}
                  getOptionValue={(e) => e.id}
                  value={value}
                  onChange={onChange}
                  className="basic-multi-select"
                  classNamePrefix="select"
                />
              )}
            />
            <p
              className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
                errors?.brandId ? null : "invisible"
              }`}
            >
              {errors?.brandId?.message}
            </p>
          </div>
        </div>

        <div className="">
          <AdminInput
            control={control}
            name="quantity"
            label="Số lượng"
            type="number"
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              errors?.quantity ? null : "invisible"
            }`}
          >
            {errors?.quantity?.message}
          </p>
        </div>
        <div className="">
          <AdminCheckbox control={control} name="published" label="Phát hành" />
        </div>
        <div>
          {/* <Controller
            control={control}
            name="description"
            defaultValue=""
            render={({ field }) => <RichText label="Mô tả đầy đủ" {...field} />}
          /> */}
          <div>
            <label
              htmlFor="description"
              className="block mb-2 text-sm font-medium text-gray-900 "
            >
              Mô tả đầy đủ
            </label>
            <CKEditor
              onChange={(e) => setRichText(e.editor.getData())}
              data={richText}
            />
          </div>
        </div>
        <div>
          <AddListImage
            onChange={selectFile}
            image={image}
            DeleteImg={DeleteImg}
          />
          <p
            className={`text-red-500 text-sm h-[1.25rem] mt-[2px] ${
              !validImg ? null : "invisible"
            }`}
          >
            Thông tin này không được để trống
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

export default ProductCreate;
