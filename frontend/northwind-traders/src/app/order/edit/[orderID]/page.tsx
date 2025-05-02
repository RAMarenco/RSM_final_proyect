"use client";
import OrderFormPage from "@/components/Order/OrderFormPage";
import { getOrdersWithDetails } from "@/services/orderService";
import { IOrderWithDetails } from "@/types/Order/Order";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

const EditOrderPage = () => {
  const [order, setOrder] = useState<IOrderWithDetails>();
  const params = useParams() as { orderID: string };
  useEffect(() => {
    getOrdersWithDetails(parseInt(params.orderID)).
      then((response) => {
        setOrder(response);
      });
  }, [])


  return <OrderFormPage mode="edit" initialData={order} />;
}

export default EditOrderPage;