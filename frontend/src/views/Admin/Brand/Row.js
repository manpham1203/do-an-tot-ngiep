import React, { useEffect, useReducer } from "react";
import { FaRegEdit, FaRegTrashAlt, FaRegEye } from "react-icons/fa";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import Tr from "../../../components/Table/Tr";
import Td from "../../../components/Table/Td";
import { setOpenadminViewBrand } from "../../../redux/adminViewBrand/adminViewBrandActions";
import { useDispatch } from "react-redux";

const initState = {
  loading: false,
  fail: false,
  data: {},
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
const fail = () => {
  return {
    type: FAIL,
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
function Row(props) {
  const [state, dispatch] = useReducer(reducer, initState);
  const navigate = useNavigate();

  const fetchData = async (id) => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/Brand/brandrowadmin/${id}`,
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(dispatch(fail()));
  };
  useEffect(() => {
    fetchData(props.id);
  }, [props.id]);
  const handlePublished = async (id) => {
    await api({
      method: "POST",
      url: `/brand/published/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          fetchData(id);
        } else {
          toast.error(`Thao tác thất bại`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
        }
      })
      .catch(() =>
        toast.error(`Thao tác thất bại`, {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        })
      );
  };
  const handleEdit = (slug) => {
    navigate(`/admin/thuong-hieu/chinh-sua/${slug}`);
  };
  const dispatchQV = useDispatch();
  const handleQuickView = () => {
    const obj = {
      show: true,
      id: state.data.id,
      type:"brand"
    };
    dispatchQV(setOpenadminViewBrand(obj));
  };

  return (
    <>
      {state.data.deleted === false ? (
        <Tr>
          <Td className="w-[50px]">
            <div className="flex justify-center">
              <input
                className="w-5 h-5 border-gray-200 rounded"
                type="checkbox"
                id="row_1"
                disabled
              />
            </div>
          </Td>
          <Td className="">
            <h2 className="productCard2Name px-[20px]">{state.data.name}</h2>
          </Td>
          <Td className="w-[150px]">
            <div className="w-full flex justify-center">
              <div
                className={`w-[50px] h-[25px]  flex items-center rounded-full relative
                  ${state.data.published ? "bg-blue-600 " : "bg-gray-300"}
                  transition-all duration-200 cursor-pointer
                  `}
                onClick={() => handlePublished(state.data.id)}
              >
                <div
                  className={`w-[18px] h-[18px] bg-white rounded-full  absolute
                    ${state.data.published ? "ml-[28px]" : "ml-[4px]"}
                    transition-all duration-200
                    `}
                ></div>
              </div>
            </div>
          </Td>
          <Td className="w-[200px]">
            <div className="w-full flex flex-row justify-center gap-x-[20px] text-[25px]">
              <FaRegEye
                onClick={() => handleQuickView()}
                className="cursor-pointer"
              />
              <FaRegEdit
                onClick={() => handleEdit(state.data.slug)}
                className="cursor-pointer"
              />
              <FaRegTrashAlt
                onClick={() => props.handleTrash(state.data.id)}
                className="cursor-pointer"
              />
            </div>
          </Td>
        </Tr>
      ) : null}
    </>
  );
}

export default Row;
