import React, { useEffect, useState } from 'react';
import api from "../../apis/api";

function ListCategoryName(props) {
    const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/category/allcategoryname`,
      params: { id: id },
    })
      .then((res) => {
        setData(res.data);
      })
      .catch(() => console.log("fail"));
  };
  useEffect(() => {
    fetchData();
  }, []);
    return (
        <div>
            
        </div>
    );
}

export default ListCategoryName;