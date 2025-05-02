"use client";
import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { toast } from "sonner";
import DefaultButton from "@/components/Button/DefaultButton";
import { IShipperWithOrders } from "@/types/Shipper/shipper";
import { getEmployeeWithOrders } from "@/services/employeeService";
import { getShipperWithOrders } from "@/services/shipperService";

export default function EmployeeDetailsPage() {
  const { shipperID } : {shipperID: string} = useParams();
  const [data, setData] = useState<IShipperWithOrders>();
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    if (isNaN(parseInt(shipperID!))) {
      toast.error("Invalid shipper ID");
      router.push("/order");
      return;
    }

    const fetchShipperData = async () => {
      try {
        if (!shipperID ) {
          toast.error("Shipper ID is required");
          return;
        }

        const data = await getShipperWithOrders(parseInt(shipperID));
        setData(data);
      } catch (error) {
        toast.error("Error loading shipper data");
      } finally {
        setLoading(false);
      }
    };

    fetchShipperData();
  }, [shipperID]);

  if (loading) {
    return (
      <div className="w-full h-full flex items-center justify-center">
        <p className="text-xl animate-pulse">Loading shipper details...</p>
      </div>
    );
  }

  if (!data) {
    return <p className="text-red-600">Data not found.</p>;
  }

  return (
    <div className="w-full p-6 space-y-8">
      <div className="bg-white shadow rounded p-6 space-y-2">
        <h1 className="text-2xl font-bold">Shipper: {data.shipper.companyName}</h1>
      </div>

      <div className="bg-white shadow rounded p-6 ">
        <h2 className="text-2xl font-semibold mb-4">Orders</h2>
        {data.orders.length === 0 ? (
          <p className="text-gray-500">No orders found for this shipper.</p>
        ) : (
          <div className="overflow-x-auto mt-4 max-h-[30rem] overflow-y-auto">
            <table className="w-full table-auto border border-gray-300">
              <thead>
                <tr className="bg-gray-100">
                  <th className="p-2 border">Order ID</th>
                  <th className="p-2 border">Customer</th>
                  <th className="p-2 border">Employee</th>
                  <th className="p-2 border">Order Date</th>
                  <th className="p-2 border">Ship Address</th>
                  <th className="p-2 border">Ship City</th>
                  <th className="p-2 border">Ship Region</th>
                  <th className="p-2 border">Ship PostalCode</th>
                  <th className="p-2 border">Ship Country</th>
                  <th className="p-2 border">Shipper</th>
                  <th className="p-2 border">Actions</th>
                </tr>
              </thead>
              <tbody>
                {data.orders.map((order) => (
                  <tr key={order.orderID} className="text-center">
                    <td className="border p-2">{order.orderID}</td>
                    <td className="border p-2">
                      {new Date(order.orderDate).toLocaleDateString()}
                    </td>
                    <td className="border p-2">{`${order.customer?.companyName}` || "-"}</td>
                    <td className="border p-2">{`${order.employee?.lastName} ${order.employee?.firstName}` || "-"}</td>
                    <td className="border p-2">{order.shipAddress || "-"}</td>
                    <td className="border p-2">{order.shipCity || "-"}</td>
                    <td className="border p-2">{order.shipRegion || "-"}</td>
                    <td className="border p-2">{order.shipPostalCode || "-"}</td>
                    <td className="border p-2">{order.shipCountry || "-"}</td>
                    <td className="border p-2">{order.shipper?.companyName || "-"}</td>
                    <td className="border p-2">
                      <DefaultButton
                        type="button"
                        onClick={() => window.location.href = `/order/${order.orderID}`}
                      >
                        View
                      </DefaultButton>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
