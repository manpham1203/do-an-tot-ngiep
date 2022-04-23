import React from 'react';
import { useSelector } from 'react-redux';

function About(props) {
    document.title="Giới Thiệu";
    const store=useSelector(state=>state);
    console.log(store)
    return (
        <div className=''>
            About page
        </div>
    );
}

export default About;