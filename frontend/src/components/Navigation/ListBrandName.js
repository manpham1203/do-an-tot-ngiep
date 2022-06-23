import React, { useEffect, useState } from 'react';
import api from "../../apis/api";

function ListBrandName(props) {
    const [data, setData] = useState([]);
  const fetchData = async (id) => {
    await api({
      method: "GET",
      url: `/brand/allbrandname`,
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
            {data.map(item=>{
              return(
                <p>{item.name}</p>
              )
            })}
        </div>
    );
}

export default ListBrandName;