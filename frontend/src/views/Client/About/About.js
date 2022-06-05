import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import api from "../../../apis/api";

function About(props) {
    document.title="Giới Thiệu";
    const [data, setData] = useState();
    const fetchData = async () => {
        await api({
          method: "GET",
          url: `/page/GetBySlug/gioi-thieu`,
          data: null,
        })
          .then((res) => {
            setData(res.data);
          })
          .catch(() => console.log("fail"));
      };
      useEffect(()=>{
        fetchData();
      }, [])
    return (
        <div className='container px-[10px] sm:px-[20px] mx-auto'>
            <div dangerouslySetInnerHTML={{__html:data?.content}}>

            </div>
        </div>
    );
}

export default About;