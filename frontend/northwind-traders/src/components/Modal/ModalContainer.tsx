import { ModalContainerProps } from "@/types/Modal/modal";

export const ModalContainer = ({ children, isMounted }: ModalContainerProps) => {
  return (
    <div className={`absolute bg-gray-900/50 sm:px-8 top-0 left-0 w-full h-full flex items-center justify-center ease-in duration-300 p-8 ${isMounted ? "opacity-100" : "opacity-0"}`}>
      <div className="relative bg-white rounded-lg shadow-sm w-full max-w-2xl">
        {children}
      </div>
    </div>
  );
};
