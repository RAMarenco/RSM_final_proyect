"use client"
import DefaultButton from "@/components/Button/DefaultButton";
import { getOrdersWithDetails } from "@/services/orderService";
import { getProducts } from "@/services/productService";
import { IOrderWithDetails, IOrderDetail } from "@/types/Order/Order";
import { IProduct } from "@/types/Product/product";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";
import { toast } from "sonner";

const OrderDetailPage = () => {
  const params = useParams();
  const orderID = params.orderID as string;
  const [editing, setEditing] = useState(false);
  const [order, setOrder] = useState<IOrderWithDetails | null>(null);
  const [products ,setProducts] = useState<IProduct[]>([]);

  useEffect(() => {
    const fetchOrder = async () => {
      try {
        const response = await getOrdersWithDetails(parseInt(orderID!));
        setOrder(response);
        toast.info("Order fetched successfully");
      } catch (error) {
        toast.error("Failed to fetch order");
      }
    };

    const fetchProducts = async () => {
      try {
        const response = await getProducts();
        setProducts(response);
        toast.info("Products fetched successfully");
      } catch (error) {
        toast.error("Failed to fetch products");
      }
    }

    fetchOrder();
    fetchProducts();
  }, [orderID]);

  const findProductById = (productID: number) => {
    return products.find((product) => product.productID === productID);
  };

  return (
    <div className="w-full">
      <div className="flex justify-between items-center h-[3rem]">
        <h1 className="text-3xl font-semibold">Order #{orderID}</h1>
        <div className="flex gap-4">
          <DefaultButton type="button" onClick={() => {}} className="bg-primary-dark-600! hover:bg-primary-dark-700!">
            Generate Report
          </DefaultButton>
          {!editing ? 
            <DefaultButton type="button" onClick={() => setEditing(!editing)} className="bg-primary-dark-600! hover:bg-primary-dark-700!">
              Edit Order
            </DefaultButton> :
            <DefaultButton type="button" onClick={() => {}} className="bg-primary-dark-600! hover:bg-primary-dark-700!">
              Save Order Changes
            </DefaultButton>
          }
          <DefaultButton type="button" onClick={() => {}} className="bg-red-600! hover:bg-red-700!">
            Delete Order
          </DefaultButton>
        </div>
      </div>

      {order ? (
        <div className="mt-6">
          <div className="space-y-6">

          <div className="bg-white p-4 shadow rounded">
            <h2 className="text-2xl font-semibold">Order Details</h2>
            <div className="overflow-x-auto mt-4">
              <table className="min-w-full table-auto border-collapse">
                <thead className="bg-gray-100">
                  <tr>
                    <th className="px-4 py-2 text-left border-b">Product ID</th>
                    <th className="px-4 py-2 text-left border-b">Product Name</th>
                    <th className="px-4 py-2 text-left border-b">Quantity</th>
                    <th className="px-4 py-2 text-left border-b">Unit Price</th>
                  </tr>
                </thead>
                <tbody>
                  {order.orderDetails.map((detail: IOrderDetail, index: number) => {
                    const product = findProductById(detail.productID);
                    return (
                      <tr key={index} className="border-b">
                        <td className="px-4 py-2">{detail.productID}</td>
                        <td className="px-4 py-2">{product?.productName || 'Product Not Found'}</td>
                        <td className="px-4 py-2">{detail.quantity}</td>
                        <td className="px-4 py-2">${detail.unitPrice.toFixed(2)}</td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          </div>


            <div className="bg-white p-4 shadow rounded">
              <h2 className="text-2xl font-semibold">Shipping Information</h2>
              <p><strong>City:</strong> {order.order.shipCity}</p>
              <p><strong>Region:</strong> {order.order.shipRegion}</p>
              <p><strong>Postal Code:</strong> {order.order.shipPostalCode}</p>
              <p><strong>Country:</strong> {order.order.shipCountry}</p>
            </div>
          </div>
        </div>
      ) : (
        <div className="mt-6">Loading order details...</div>
      )}
    </div>
  );
};

export default OrderDetailPage;
