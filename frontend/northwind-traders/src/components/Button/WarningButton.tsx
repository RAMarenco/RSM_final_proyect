import React from "react";

interface ButtonProps {
  type: "button" | "submit" | "reset";
  children: React.ReactNode;
  onClick?: () => void;
}

const WarningButton: React.FC<ButtonProps> = ({ type, children, onClick }) => {
  return (
    <button
      type={type}
      className="bg-yellow-400 hover:bg-yellow-500 focus:outline-none focus:ring-4 focus:ring-yellow-300 font-medium rounded-full text-sm px-5 py-2.5 text-center me-2 mb-2 transition-colors duration-300"
      onClick={onClick}
    >
      {children}
    </button>
  );
};

export default WarningButton;
