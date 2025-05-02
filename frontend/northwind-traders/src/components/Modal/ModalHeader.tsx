import { ModalHeaderProps } from "@/types/Modal/modal";

export const ModalHeader = ({ title, onClose, Icon }: ModalHeaderProps) => {
  return (
    <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t border-gray-200">
      <h3 className="text-xl font-semibold text-gray-900 ">
        {Icon && <span className="pr-4">{Icon}</span>}
        {title}
      </h3>
      <CloseButton onClick={onClose} />
    </div>
  );
};

const CloseButton = ({ onClick }: { onClick: () => void }) => (
  <button
    type="button"
    className="cursor-pointer end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center"
    onClick={onClick}
  >
    <svg
      className="w-3 h-3"
      aria-hidden="true"
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 14 14"
    >
      <path
        stroke="currentColor"
        strokeLinecap="round"
        strokeLinejoin="round"
        strokeWidth="2"
        d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
      />
    </svg>
    <span className="sr-only">Close modal</span>
  </button>
);
