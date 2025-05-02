import { ButtonProps } from "@/types/button";
import React from "react";

const DefaultButton: React.FC<ButtonProps> = ({ type, children, onClick, className, outline = false, disabled }) => {
  return (
    <button
      type={type}
      className={`${!outline ? "text-white bg-primary-500": "outline-solid outline-primary-500 hover:outline-primary-800 text-black hover:text-white"} hover:bg-primary-800 focus:outline-none focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center disabled:opacity-50 disabled:cursor-not-allowed hover:cursor-pointer ease-in-out duration-300 ${className}`}
      onClick={onClick}
      disabled={disabled}
    >
      {children}
    </button>
  );
};

export default DefaultButton;
