import { ButtonProps } from "@/types/button";
import React from "react";

const TransparentButton: React.FC<ButtonProps> = ({
  type,
  children,
  onClick,
  className
}) => {
  return (
    <button
      type={type}
      className={`cursor-pointer text-sm font-medium text-gray-900 focus:outline-none rounded-full hover:text-blue-700 transition-colors duration-300 ${className}`}
      onClick={onClick}
    >
      {children}
    </button>
  );
};

export default TransparentButton;