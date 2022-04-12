import React, { useEffect, useReducer } from "react";
import { FaRegEdit, FaRegTrashAlt } from "react-icons/fa";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

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
      url: `/Category/categoryrowadmin/${id}`,
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
      url: `/category/published/${id}`,
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
    navigate(`/admin/chinh-sua-san-pham/${slug}`);
  };
  const handleTrash = async (id) => {
    await api({
      method: "POST",
      url: `/category/deleted/${id}`,
    })
      .then((res) => {
        if (res.status === 200) {
          toast.warn(`Chuyển vào thùng rác thành công`, {
            position: toast.POSITION.TOP_RIGHT,
            autoClose: 3000,
          });
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
  return (
    <>
      {state.data.deleted === false ? (
        <tr>
          <td className="sticky left-0 px-4 py-2 ">
            <input
              className="w-5 h-5 border-gray-200 rounded"
              type="checkbox"
              id="row_1"
            />
          </td>
          <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
            {state.data.name}
          </td>
          <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
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
          <td className="px-4 py-2 text-gray-700 whitespace-nowrap flex flex-row text-[25px] gap-x-[20px]">
            <FaRegEdit
              onClick={() => handleEdit(state.data.slug)}
              className="cursor-pointer"
            />
            <FaRegTrashAlt
              onClick={() => handleTrash(state.data.id)}
              className="cursor-pointer"
            />
          </td>
        </tr>
      ) : null}
    </>
  );
}

export default Row;
