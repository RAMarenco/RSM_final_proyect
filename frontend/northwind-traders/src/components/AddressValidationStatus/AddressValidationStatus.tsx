import { AddressValidationStatus } from "@/types/AddressValidationStatus/AddressValidationStatus";
import { CheckIcon, XMarkIcon } from "@heroicons/react/16/solid";

const AddressStatusIcon = ({ status }: { status: AddressValidationStatus }) => {
  if (status === AddressValidationStatus.Validated)
    return (
      <div className="text-green-500 mt-4 outline-2 outline-green-500 rounded-full p-1 h-10 w-10 flex items-center justify-center">
        <CheckIcon className="h-5 w-5" />
      </div>
    );
  if (status === AddressValidationStatus.Invalid)
    return (
      <div className="text-red-500 mt-4 outline-2 outline-red-500 rounded-full p-1 h-10 w-10 flex items-center justify-center">
        <span className="h-5 w-5 text-center"><XMarkIcon/></span>
      </div>
    );
  return (
    <div className="text-gray-500 mt-4 outline-2 outline-gray-500 rounded-full p-1 h-10 w-10 flex items-center justify-center">
      <span className="h-5 w-5 text-center">?</span>
    </div>
  );
};

export default AddressStatusIcon;