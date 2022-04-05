import React, { useEffect, useReducer } from "react";
import api from "../../../apis/api";

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
function ProductTable(props) {
  const [state, dispatch] = useReducer(reducer, initState);

  const fetchData = async () => {
    dispatch(loading());
    await api({
      method: "GET",
      url: `/Product/productfull`,
      data: null,
    })
      .then((res) => {
        dispatch(success(res.data));
      })
      .catch(dispatch(fail()));
  };

  useEffect(() => {
    fetchData();
  }, []);

  console.log(state);
  return (
    <div className="overflow-hidden overflow-x-auto border border-gray-100 rounded-xl">
      <table className="min-w-full text-sm divide-y divide-gray-200">
        <thead>
          <tr className="bg-white">
            <th className="sticky left-0 px-4 py-2 text-left bg-white">
              <input
                className="w-5 h-5 border-gray-200 rounded"
                type="checkbox"
                id="row_all"
              />
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Hình
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Tên sản phẩm
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Phát hành
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Hành động
            </th>
          </tr>
        </thead>

        <tbody className="divide-y divide-gray-100">
          {state.data.map((item) => {
            return (
              <tr key={item.id}>
                <td className="sticky left-0 px-4 py-2 ">
                  <input
                    className="w-5 h-5 border-gray-200 rounded"
                    type="checkbox"
                    id="row_1"
                  />
                </td>
                <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
                  hinh
                </td>
                <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
                  {item.name}
                </td>
                <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
                  {item.pulished?"true":"false"}
                </td>
                <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
                  
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default ProductTable;
