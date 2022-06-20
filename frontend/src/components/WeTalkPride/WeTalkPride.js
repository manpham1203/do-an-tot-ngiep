import React from "react";
import divider1 from "../../assets/divider1.svg";
import img from "../../assets/watches-about.jpg";
function WeTalkPride(props) {
  return (
    <div className="w-[100%]  pb-[60px] bg-white">
      <div className="container mx-auto text-center pt-[80px]">
        <h2 className="text-[20px]">We Take Pride</h2>
        <h1 className="text-[30px] font-bold">About Collection Watches</h1>
        <div className="flex justify-center">
          <img src={divider1} alt="" className="w-[250px]" />
        </div>
        <p className="text-[20px] font-second text-[#a09db4]">
          Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua.
        </p>
        <div className="flex justify-center">
          <img src={img} alt="" className="w-600px" />
        </div>
        <h1 className="text-[30px] font-bold">
          Carfully Crafted and Built to Last
        </h1>
        <p className="text-[20px] font-second text-[#a09db4]">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et
          purus quis nisi tempus euismod. Curabitur pretium eros nunc, ac cursus
          dolor scelerisque ac. Nulla volutpat felis quis ante placerat, sed
          sagittis diam lobortis. Donec auctor rutrum hendrerit. Pellentesque a
          sollicitudin erat, vel interdum ipsum. Integer molestie erat id tellus
          euismod, eu accumsan orci venenatis.
        </p>
      </div>
    
    </div>
  );
}

export default WeTalkPride;
