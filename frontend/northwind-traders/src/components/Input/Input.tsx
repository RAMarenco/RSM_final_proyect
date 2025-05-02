import React, { forwardRef } from "react";

interface InputProps {
  title: string;
  type: string;
  placeholder: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  required?: boolean;
  Icon?: React.ReactNode;
  disabled?: boolean;
  className?: string;
  autoComplete?: string;
  error?: string;
}

const Input = forwardRef<HTMLInputElement, InputProps>(
  ({ title, type, placeholder, value, onChange, Icon, disabled, className, autoComplete, error }, ref) => {
    const inputId = `input-${title.toLowerCase().replace(" ", "-")}`; // Genera un ID único

    return (
      <div id={inputId} autoFocus={true} className={className}>
        <label className="block mb-2 text-sm font-medium text-gray-900 ">
          {title}
        </label>
        <div className={Icon ? "relative mb-6" : "mb-6"}>
          {Icon && (
            <div className="absolute inset-y-0 start-0 flex items-center ps-3.5 pointer-events-none">
              {Icon}
            </div>
          )}
          <input
            ref={ref}
            autoComplete={autoComplete}
            disabled={disabled}
            type={type}
            placeholder={placeholder}
            value={value}
            id={inputId}
            onChange={onChange}
            className={`bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full
              ${Icon ? "ps-10" : ""}
              p-2.5 disabled:bg-gray-200 disabled:text-gray-500 disabled:cursor-not-allowed`}
          />
          {error && <p className="text-red-500 text-sm pt-1">{error}</p>}
        </div>
      </div>
    );
  }
);

Input.displayName = "Input";

export default Input;