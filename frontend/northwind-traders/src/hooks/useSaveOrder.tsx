import { createOrder, updateOrder } from "@/services/orderService";
import { ICreateOrderDto, IUpdateOrderDto } from "@/types/Order/order.dto";
import { IProductDto } from "@/types/Product/product.dto";
import { useRouter } from "next/navigation";
import { toast } from "sonner";

export const useSaveOrder = ({
    customerID,
    employeeID,
    shipVia,
    orderDate,
    address,
    addressFields,
    selectedProducts,
    mode,
    existingOrder,
    orderID,
  }: {
    customerID: string;
    employeeID: number;
    shipVia: number;
    orderDate: string;
    address: string;
    addressFields: {
      shipCity: string;
      shipRegion: string;
      shipPostalCode: string;
      shipCountry: string;
    };
    selectedProducts: IProductDto[];
    mode: "create" | "edit";
  } & {
    orderID?: number;
    existingOrder?: IUpdateOrderDto;
}) => {
  const router = useRouter();
  const commonFields = {
    shipVia,
    orderDate: orderDate === "" ? new Date().toISOString() : orderDate,
    shipAddress: address,
    shipCity: addressFields.shipCity,
    shipRegion: addressFields.shipRegion,
    shipPostalCode: addressFields.shipPostalCode,
    shipCountry: addressFields.shipCountry,
    products: selectedProducts,
  };

  return () => {
    if (mode === "create") {
      const createOrderDto: ICreateOrderDto = {
        customerID,
        employeeID,
        ...commonFields,
      };
      createOrder(createOrderDto).then((response) => {
        toast.success("Order created successfully");
        const match = response.match(/Order:\s*(\d+)/);
        if (match) router.push(`/order/${match[1]}`);
      }).catch((error) => {
        for(const errorMessage in error.messages) {
          toast.error("Error creating order: " + error.messages[errorMessage]);
        }
      });
    } else if (mode === "edit" && existingOrder) {
      const updateOrderDto: IUpdateOrderDto = {
        ...commonFields,
      };
      updateOrder(orderID!, updateOrderDto).then(() => {
        toast.success("Order updated successfully");
        router.push(`/order/${orderID}`);
      }).catch((error) => {
        for(const errorMessage in error.messages) {
          toast.error("Error updating order: " + error.messages[errorMessage]);
        }
      });
    }
  }
};