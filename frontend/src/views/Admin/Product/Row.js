import React, { useEffect, useReducer } from "react";
import { FaRegEdit, FaRegTrashAlt,FaRegEye } from "react-icons/fa";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { setOpenAdminViewProduct } from "../../../redux/adminViewProduct/adminViewProductActions";
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
  const dispatchQV=useDispatch();

  const fetchData = async (id) => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/Product/ProductRowAdmin/${id}`,
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
      url: `/product/published/${id}`,
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
    navigate(`/admin/san-pham/chinh-sua/${slug}`);
  };

  const handleQuickView = () => {
    const obj = {
      show: true,
      id: props.slug,
    };
    dispatchQV(setOpenAdminViewProduct(obj));
  };

  return (
    <>
      {state.data.deleted === false ? (
        <tr>
          <td className="sticky left-0 px-4 py-2 ">
            <input
              className="w-5 h-5 border-gray-200 rounded"
              type="checkbox"
              id={state.data.id}
              value={state.data.id}
              onChange={props.handleProductSelect}
              checked={props.productSelect.some(x=>x===state.data.id)}
            />
          </td>
          <td className="px-4 py-2 text-gray-700 ">
            <div className="w-[60px] h-[60px]">
              <img
                src={state.data.imageSrc}
                alt=""
                className="w-full h-full object-cover object-center"
              />
            </div>
          </td>
          <td className="px-4 py-2 text-gray-700">
            <h2 className="productCard2Name">{state.data.name}</h2>
          </td>
          <td className="px-4 py-2 text-gray-700 ">
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
          </td>
          <td className="px-4 py-2 text-gray-700  text-[25px]">
            <div className="flex flex-row items-center gap-x-[20px]">
            <FaRegEye 
            onClick={()=>handleQuickView()}
            className="cursor-pointer"/>
              <FaRegEdit
                onClick={() => handleEdit(state.data.slug)}
                className="cursor-pointer"
              />
              <FaRegTrashAlt
                onClick={() => props.handleTrash(state.data.id)}
                className="cursor-pointer"
              />
            </div>
          </td>
        </tr>
      ) : null}
    </>
  );
}

export default Row;
