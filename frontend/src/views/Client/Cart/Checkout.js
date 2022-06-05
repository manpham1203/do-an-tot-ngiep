import React, { useEffect, useReducer, useState } from "react";
import Input from "../../../components/Form/Input/Input";
import Radio from "../../../components/Form/Radio/Radio";
import { toast } from "react-toastify";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useDispatch, useSelector } from "react-redux";
import api from "../../../apis/api";
import { setCartEmpty } from "../../../redux/cart/cartActions";
import { useNavigate } from "react-router-dom";

const initState = {
  loading: false,
  fail: false,
  data: [],
};
//action
const LOADING = "LOADING";
const SUCCESS = "SUCCESS";
const FAIL = "FAIL";
const loading = () => {
  return {
    type: LOADING,
  };
};
const success = (payload) => {
  return {
    type: SUCCESS,
    payload: payload,
  };
};
const fail = (payload) => {
  return {
    type: FAIL,
    payload: payload,
  };
};
const reducer = (state, action) => {
  switch (action.type) {
    case LOADING:
      return {
        ...state,
        loading: true,
        fail: false,
      };
    case SUCCESS:
      return {
        ...state,
        loading: false,
        fail: false,
        data: action.payload,
      };
    case FAIL:
      return {
        ...state,
        loading: false,
        fail: true,
      };
    default:
      return state;
  }
};
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
    deliveryEmail: yup
      .string()
      .email("Sai định dạng email")
      .required("Thông tin này không được để trống")
      .nullable()
      .trim(),
    deliveryPhone: yup
      .string()
      .required("Thông tin này không được để trống")
      .matches(/^\d+$/, "Chỉ được phép nhật số")
      .nullable()
      .trim(),
    deliveryAddress: yup
      .string()
      .required("Thông tin này không được để trống")
      .nullable(),
  })
  .required();
function Checkout(props) {
  document.title = "Thanh toán";
  const { cart, user } = useSelector((state) => state);
  const [cartData, setCartData] = useState([]);
  const [items, setItems] = useState([]);
  const [state, dispatchProduct] = useReducer(reducer, initState);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const {
    handleSubmit,
    reset,
    watch,
    register,
    formState: { errors, isValid, isSubmitting, isSubmitSuccessful },
    control,
  } = useForm({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: { paymentMethod: "1" },
  });
  const fetchProducts = async (arr) => {
    dispatchProduct(loading());
    await api({
      method: "POST",
      url: `/Product/cartrows`,
      data: arr,
    })
      .then((res) => {
        dispatchProduct(success(res.data));
      })
      .catch(() => dispatchProduct(fail()));
  };
  const fetchDataUser = async (id) => {
    await api({
      method: "GET",
      url: `/user/${id}`,
    })
      .then((res) => {
        console.log(res);
        reset({
          firstName: res.data.firstName,
          lastName: res.data.lastName,
          deliveryEmail: res.data.email,
          deliveryPhone: res.data.phoneNumber,
          deliveryAddress: res.data.address,
        });
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchDataUser(user.id);
  }, [user.id]);

  useEffect(() => {
    var newArr = [];
    for (var i = 0; i < cart.length; i++) {
      newArr.push(cart[i].cartId);
    }
    fetchProducts(newArr);
  }, []);

  useEffect(() => {
    var newArr = [];
    for (var i = 0; i < cart.length; i++) {
      for (var j = 0; j < state.data.length; j++) {
        if (state.data[j].id === cart[i].cartId) {
          newArr.push({
            productId: state.data[j].id,
            quantity: cart[i].qty,
            unitPrice: state.data[j].currentPrice,
          });
        }
      }
    }
    setItems(newArr);
  }, [cart, state.data]);
  useEffect(() => {
    var newArr = [];
    for (var i = 0; i < cart.length; i++) {
      for (var j = 0; j < state.data.length; j++) {
        if (state.data[j].id === cart[i].cartId) {
          newArr.push({
            item: state.data[j],
            qty: cart[i].qty,
          });
        }
      }
    }
    setCartData(newArr);
  }, [cart, state.data]);

  const watchShip = watch("paymentMethod");
  const watchAddress = watch("deliveryAddress");
  const watchEmail = watch("deliveryEmail");
  const watchPhone = watch("deliveryPhone");
  const watchFirstName = watch("firstName");
  const watchLastName = watch("lastName");
  const watchNote = watch("note");
  const onSubmitHandler = async () => {
    var item = items;
    const data = {
      userId: user.id,
      deliveryAddress: watchAddress,
      deliveryEmail: watchEmail,
      deliveryPhone: watchPhone,
      orderDetailVMs: item,
      firstName: watchFirstName,
      lastName: watchLastName,
      note: watchNote,
    };
    await api({
      method: "POST",
      url: `/order`,
      data: data,
    })
      .then((res) => {
        if (res.status === 201) {
          toast.success("Thanh toán thành công", {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
          dispatch(setCartEmpty());
          navigate("/");
        } else {
          toast.error("Thanh toán thất bại", {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error("Thanh toán thất bại", {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  console.log(errors);
  return (
    <div className=" container px-[10px] sm:px-[20px] mx-auto">
      <form
        onSubmit={handleSubmit(onSubmitHandler)}
        autoComplete="off"
        className="flex flex-col lg:flex-row container mx-auto mt-[40px] gap-x-[20px] gap-y-[25px]"
      >
        <div className="w-full">
          <div className="flex flex-col lg:flex-row gap-x-[20px] gap-y-[20px]">
            <div className="w-full">
              <Input name="lastName" type="text" label="Họ" control={control} />
              <p
                className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                  errors?.lastName ? null : "invisible"
                }`}
              >
                {errors?.lastName?.message}
              </p>
            </div>
            <div className="w-full">
              <Input
                name="firstName"
                type="text"
                label="Tên"
                control={control}
              />
              <p
                className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                  errors?.firstName ? null : "invisible"
                }`}
              >
                {errors?.firstName?.message}
              </p>
            </div>
          </div>
          <div className="mt-[20px]">
            <Input
              name="deliveryPhone"
              type="text"
              label="Số điện thoại"
              control={control}
            />
            <p
              className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                errors?.deliveryPhone ? null : "invisible"
              }`}
            >
              {errors?.deliveryPhone?.message}
            </p>
          </div>
          <div className="mt-[20px]">
            <Input
              name="deliveryEmail"
              type="text"
              label="Email"
              control={control}
            />
            <p
              className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                errors?.deliveryEmail ? null : "invisible"
              }`}
            >
              {errors?.deliveryEmail?.message}
            </p>
          </div>
          <div className="mt-[20px]">
            <Input
              name="deliveryAddress"
              type="text"
              label="Địa chỉ giao hàng"
              control={control}
            />
            <p
              className={`text-red-500 text-sm min-h-[1.25rem] mt-[2px] ${
                errors?.deliveryAddress ? null : "invisible"
              }`}
            >
              {errors?.deliveryAddress?.message}
            </p>
          </div>
          <div className="mt-[20px]">
            <div className="w-full relative">
              <textarea
                placeholder=" "
                {...register("note")}
                className="h-[100px] py-[20px] form-input border border-input-border text-input-color font-normal rounded-[4px] w-[100%] px-[20px] transition-all duration-[0.25s] focus:border-second outline-none bg-third"
              />
              <label className="form-label absolute left-[20px] top-[20%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-input-label">
                Ghi chú
              </label>
            </div>
          </div>
        </div>
        <div className="w-full lg:w-[400px] flex-none h-fit bg-[#F9F9F9] border border-dashed border-[#D7D7D7] rounded">
          <h2 className="my-[20px] text-[20px] text-center">
            Đơn hàng của bạn
          </h2>
          <div className="px-[20px]">
            <div className="w-full border-t border-[#CCCCCC]"></div>
          </div>
          <div className="w-full lg:w-[350px] mx-0 lg:mx-auto px-[20px] lg:px-0">
            <div className="flex flex-row justify-between pt-[20px]">
              <div className="grow-[1]">Sản phẩm</div>
              <div className=" w-[110px]  text-right">Thành tiền</div>
            </div>
            {cartData.map((cart) => {
              return (
                <div
                  key={cart?.item?.id}
                  className="flex flex-row justify-between pt-[20px] border-b pb-[20px] border-[#EBEBEB]"
                >
                  <div className="font-light max-w-[150px] sm:max-w-[300px] lg:max-w-[220px]">
                    {cart?.item?.name} (x{cart?.qty})
                  </div>
                  <div className="font-light w-[110px] text-right">
                    {cart?.item?.currentPrice * cart?.qty}
                  </div>
                </div>
              );
            })}

            <div className="flex flex-row justify-between pt-[20px]  border-b pb-[20px] border-[#EBEBEB]">
              <div className="grow-[1]">Tổng tiền hàng</div>
              <div className=" w-[110px]  text-right">
                {cartData.reduce((result, prod) => {
                  return result + prod.item.currentPrice * prod.qty;
                }, 0)}
              </div>
            </div>
            <div className="flex flex-row justify-between pt-[20px]  border-b pb-[20px] border-[#EBEBEB]">
              <div className="grow-[1]">Tổng thanh toán</div>
              <div className=" w-[110px]  text-right">
                {cartData.reduce((result, prod) => {
                  return result + prod.item.currentPrice * prod.qty;
                }, 0)}
              </div>
            </div>
          </div>
          <div className="w-full lg:w-[350px] mx-0 lg:mx-auto pt-[20px] px-[20px] lg:px-0">
            <h2 className="mb-[10px]">Phương thức thanh toán</h2>
            <div className="flex flex-col gap-y-[10px]">
              <Radio
                name="paymentMethod"
                label="Thanh toán khi nhận hàng"
                control={control}
                value="1"
                checked={watchShip === "1"}
              />
              <Radio
                name="paymentMethod"
                label="Chuyển khoản"
                control={control}
                value="2"
                checked={watchShip === "2"}
                disabled
                className="opacity-[0.5] cursor-default"
              />
            </div>
          </div>
          <div className="flex justify-center my-[30px]">
            <button
              className="border-2 border-second px-[20px] h-[40px] text-[20px]"
              type="submit"
            >
              THANH TOÁN
            </button>
          </div>
        </div>
      </form>
    </div>
  );
}

export default Checkout;
