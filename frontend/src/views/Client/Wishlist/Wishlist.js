import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import api from "../../apis/api";

function Wishlist(props) {
    const {user}=useSelector(store=>store);
    const [data, setData] = useState([]);
    const fetchData = async () => {
        await api({
          method: "GET",
          url: `/Wishlist`,
          params: { userId: user.id, productId: props.id },
        })
          .then((res) => {
            if (res.status === 200) {
              setData(res.data);
            }
          })
          .catch(() => console.log("fail"));
      };
    return (
        <div>
            
        </div>
    );
}

export default Wishlist;