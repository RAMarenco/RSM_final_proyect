import { ModalContainer } from "@/components/Modal/ModalContainer";
import { ModalHeader } from "@/components/Modal/ModalHeader";
import NegativeButton from "@/components/Button/NegativeButton";
import { useRef } from "react";
import { toast } from "sonner";
import { ModalProps } from "@/types/Modal/modal";
import { useModal } from "@/hooks/useModal";
import { IOrder } from "@/types/Order/Order";
import { deleteOrder } from "@/services/orderService";
import { useRouter } from "next/navigation";

export const OrderDeleteModal: React.FC<ModalProps<IOrder>> = ({
  setShowModal,
  initialState,
}) => {
  const hasFetched = useRef(false);
  const {isMounted, handleClose, isLoading, setIsLoading} = useModal(setShowModal);
  const router = useRouter();

  const handleClick = () => {
    if (hasFetched.current) return;
    hasFetched.current = true;

    setIsLoading(true);
    deleteOrder(initialState!.orderID)
    .then((response) => {
        toast.success(response);
        setIsLoading(false);
        handleClose();
      })
      .catch(() => {
        setIsLoading(false);
      })
      .finally(() => {
        hasFetched.current = false;
        router.push("/order");
      });
  }

  return (
    <ModalContainer isMounted={isMounted}>
      <ModalHeader
        title="Confirm Deletion"
        onClose={() => handleClose()}
        Icon={<i className="fa-solid fa-warning text-amber-500"></i>}
      />
      <div className="p-4 md:p-5">
        <div className="flex flex-col gap-2">
          <h4 className="text-lg font-medium text-gray-800">
            Are you sure you want to delete the order {initialState && (
              <span className="font-bold text-red-600">#{initialState.orderID}</span>
            )}?
          </h4>
          <p className="text-gray-500">This action cannot be undone.</p>
        </div>
        <div className="flex justify-end">
          <NegativeButton type="button" className={`mt-6 cursor-pointer rounded-lg! ${isLoading && "!py-1.25"}`} onClick={handleClick} disabled={isLoading}>
            {isLoading ? "..." : "Delete order"}
          </NegativeButton>
        </div>
      </div>
    </ModalContainer>
  );
};
