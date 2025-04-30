import React from "react";

interface ButtonProps {
  type: "button" | "submit" | "reset";
  children: React.ReactNode;
  onClick?: () => void;
}

const NegativeButton: React.FC<ButtonProps> = ({ type, children, onClick }) => {
  return (
    <button
      type={type}
      className="text-white bg-red-700 hover:bg-red-800 focus:outline-none focus:ring-4 focus:ring-red-300 font-medium rounded-full text-sm px-5 py-2.5 text-center me-2 mb-2"
      onClick={onClick}
    >
      {children}
    </button>
  );
};

export default NegativeButton;
