import { ButtonProps } from "@/types/button";
import React from "react";

const DefaultButton: React.FC<ButtonProps> = ({ type, children, onClick, className }) => {
  return (
    <button
      type={type}
      className={`text-white bg-primary-500 hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center disabled:opacity-50 hover:cursor-pointer ease-in-out duration-300 ${className}`}
      onClick={onClick}
    >
      {children}
    </button>
  );
};

export default DefaultButton;
