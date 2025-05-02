"use client";
import OrderFormPage from "@/components/Order/OrderFormPage";
import { getOrdersWithDetails } from "@/services/orderService";
import { IOrderWithDetails } from "@/types/Order/Order";
import { IUpdateOrderDto } from "@/types/Order/order.dto";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

const EditOrderPage = () => {
  const [order, setOrder] = useState<IOrderWithDetails>();
  const [orderDto, setOrderDto] = useState<IUpdateOrderDto>();
  const params = useParams() as { orderID: string };
  useEffect(() => {
    getOrdersWithDetails(parseInt(params.orderID)).
      then((response) => {
        setOrder(response);
      });
  }, []);

  useEffect(() => {
    if (order) {
      setOrderDto({
        orderDate: order.order.orderDate.toString(),
        shipVia: order.order.shipper?.shipperID!,
        shipAddress: order.order.shipAddress,
        shipCity: order.order.shipCity,
        shipRegion: order.order.shipRegion,
        shipPostalCode: order.order.shipPostalCode,
        shipCountry: order.order.shipCountry,
        products: order.orderDetails.map((product) => ({
          productID: product.productID,
          quantity: product.quantity,
          unitPrice: product.unitPrice,
        })),
      });
    }
  }, [order]);

  return <OrderFormPage mode="edit" existingOrder={orderDto} orderID={parseInt(params.orderID)}/>;
}

export default EditOrderPage;