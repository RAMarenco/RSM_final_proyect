"use client";
import { getOrders } from "@/services/orderService";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import DefaultButton from "@/components/Button/DefaultButton";
import { toast } from "sonner";
import Pagination from "@/components/Pagination/Pagination";
import { IOrder } from "@/types/Order/Order";
import { IPaginated } from "@/types/Pagination/pagination";
import Card from "@/components/Card/Card";

export default function Home() {
  const [orders, setOrders] = useState<IPaginated<IOrder>>();
  const [currentPage, setCurrentPage] = useState(1);
  const router = useRouter();

  const fetchOrders = async (page: number) => {
    try {
      const response = await getOrders(page);
      setOrders(response);
    } catch (error) {
      toast.error("Error fetching orders");
    }
  };


  useEffect(() => {
    fetchOrders(currentPage);
  }, [currentPage]);

  const handleViewOrder = (orderID: number) => {
    // Navigate to the order details page
    router.push(`/order/${orderID}`);
  };

  const handleEditOrder = (orderID: number) => {
    // Navigate to the order edit page
    router.push(`/order/edit/${orderID}`);
  };

  const handleCreateOrder = () => {
    // Navigate to the create order page
    router.push("/order/create");
  };

  return (
    <div className="w-full grid grid-cols-1 grid-rows-[3rem_1fr_3rem] gap-4">
      <div className="flex justify-between items-center">
        <h1 className="text-3xl font-semibold">Orders</h1>
        <DefaultButton type="button" onClick={handleCreateOrder} className="bg-primary-dark-600! hover:bg-primary-dark-700!">
          Create New Order
        </DefaultButton>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 xl:grid-cols-5 gap-x-6 gap-y-4 overflow-y-auto pb-4">
        {orders?.data.map((order) => (
          <Card key={order.orderID}>
            <div className="flex items-center justify-between mb-2">
              <h2 className="text-xl font-bold">
                #{order.orderID}
              </h2>
              <p>
                {new Date(order.orderDate).toLocaleDateString()}
              </p>
            </div>

            <div className="text-sm text-gray-600 mb-4 space-y-1">
              
              {order.customer?.companyName && (
                <p>
                  <span className="font-semibold">Customer:</span>{" "}
                  {order.customer.companyName}
                </p>
              )}
              {order.employee && (
                <p>
                  <span className="font-semibold">Employee:</span>{" "}
                  {order.employee.firstName} {order.employee.lastName}
                </p>
              )}
              {order.shipper?.companyName && (
                <p>
                  <span className="font-semibold">Shipper:</span>{" "}
                  {order.shipper.companyName}
                </p>
              )}
              {order.shipper?.phone && (
                <p>
                  <span className="font-semibold">Shipper Phone:</span>{" "}
                  {order.shipper.phone}
                </p>
              )}
            </div>

            {(order.shipAddress || order.shipCity || order.shipPostalCode) && (
              <div className="text-sm text-gray-700">
                <h3 className="font-semibold mb-1">Shipping Address:</h3>
                {order.shipAddress && <p>{order.shipAddress}</p>}
                {(order.shipCity || order.shipRegion) && (
                  <p>
                    {[order.shipCity, order.shipRegion]
                      .filter(Boolean)
                      .join(", ")}
                  </p>
                )}
                {(order.shipPostalCode || order.shipCountry) && (
                  <p>
                    {[order.shipPostalCode, order.shipCountry]
                      .filter(Boolean)
                      .join(", ")}
                  </p>
                )}
              </div>
            )}
            <div className="flex mt-4 space-x-3">
              <DefaultButton type="button" onClick={() => handleViewOrder(order.orderID)}>
                View
              </DefaultButton>
            </div>
          </Card>
        ))}
      </div>
      {orders && <Pagination data={orders} currentPage={currentPage} setCurrentPage={setCurrentPage}/>}
    </div>
  );
}
