"use client";
import { getOrders } from "@/services/orderService";
import { IPaginatedOrder } from "@/types/Order/order";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import DefaultButton from "@/components/Button/DefaultButton";
import { toast } from "sonner";

export default function Home() {
  const [orders, setOrders] = useState<IPaginatedOrder>();
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

  const renderPagination = () => {
    if (!orders || orders.totalPages <= 1) return null;

    const totalPages = orders.totalPages;
    const pages = [];
    const maxVisiblePages = 5; // Adjust this number as needed

    // Always add first page
    pages.push(1);

    // Determine the range of pages to show around current page
    let startPage = Math.max(2, currentPage - 1);
    let endPage = Math.min(totalPages - 1, currentPage + 1);

    // Adjust if we're near the start or end
    if (currentPage <= 3) {
      endPage = Math.min(4, totalPages - 1);
    } else if (currentPage >= totalPages - 2) {
      startPage = Math.max(totalPages - 3, 2);
    }

    // Add ellipsis if there's a gap between first page and startPage
    if (startPage > 2) {
      pages.push('...');
    }

    // Add middle pages
    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }

    // Add ellipsis if there's a gap between endPage and last page
    if (endPage < totalPages - 1) {
      pages.push('...');
    }

    // Always add last page if there's more than one page
    if (totalPages > 1) {
      pages.push(totalPages);
    }

    const handlePageChange = (page: number) => {
      setCurrentPage(page);
    };

    return (
      <div className="flex justify-center items-center space-x-2">
        {pages.map((page, index) => (
          <div key={index}>
            {page === '...' ? (
              <span className="px-3 py-1 text-lg pointer-events-none">...</span>
            ) : (
              <DefaultButton
                type="button"
                onClick={() => handlePageChange(page as number)}
                className={currentPage === page ? 'bg-gray-600! text-white hover:bg-gray-600! hover:cursor-default!' : 'bg-gray-400! hover:bg-gray-700!'}
              >
                {page}
              </DefaultButton>
            )}
          </div>
        ))}
      </div>
    );
  };

  return (
    <div className="w-full grid grid-cols-1 grid-rows-[3rem_1fr_3rem] gap-4">
      <div className="flex justify-between items-center">
        <h1 className="text-3xl font-semibold">Orders</h1>
        <DefaultButton type="button" onClick={handleCreateOrder} className="bg-primary-dark-600! hover:bg-primary-dark-700!">
          Create New Order
        </DefaultButton>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-x-6 gap-y-4">
        {orders?.data.map((order) => (
          <div
            key={order.orderID}
            className="bg-white shadow-lg rounded-xl p-4 border border-gray-200"
          >
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
              <DefaultButton type="button" onClick={() => handleEditOrder(order.orderID)}>
                Edit
              </DefaultButton>
            </div>
          </div>
        ))}
      </div>
      {renderPagination()}
    </div>
  );
}
