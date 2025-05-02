"use client"
import DefaultButton from "@/components/Button/DefaultButton";
import { MAP_ID, MAP_KEY } from "@/consts/consts";
import { geocodeAddress } from "@/services/googleService";
import { getOrderByIdReport, getOrdersWithDetails } from "@/services/orderService";
import { getProducts } from "@/services/productService";
import { IOrder, IOrderWithDetails } from "@/types/Order/Order";
import { IOrderDetail } from "@/types/OrderDetail/orderDetail";
import { IProduct } from "@/types/Product/product";
import { AdvancedMarker, APIProvider, Map } from "@vis.gl/react-google-maps";
import { useParams, useRouter } from "next/navigation";
import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { toast } from "sonner";
import { OrderDeleteModal } from "./components/DeleteModal";
import { DocumentArrowDownIcon, PencilIcon, TrashIcon } from "@heroicons/react/24/outline";

const OrderDetailPage = () => {
  const params = useParams();
  const orderID = params.orderID as string;
  const [editing, setEditing] = useState(false);
  const [order, setOrder] = useState<IOrderWithDetails | null>(null);
  const [products ,setProducts] = useState<IProduct[]>([]);
  const [geocodedLocation, setGeocodedLocation] = useState<{ lat: number; lng: number } | null>(null);
  const fetchedRef = useRef(false);
  const [orderDelete, setOrderDelete] = useState<IOrder>();
  const [showDeleteModal, setShowDeleteModal] = useState<boolean>(false);
  const [loading, setLoading] = useState(false);
  const router = useRouter();

  const fetchOrder = async () => {
    if (isNaN(parseInt(orderID!))) {
      router.push("/order");
      toast.error("Invalid order ID");
      return;
    }

    try {
      const response = await getOrdersWithDetails(parseInt(orderID!));
      setOrder(response);
      toast.info("Order fetched successfully");
    } catch (error) {
      router.push("/order");
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

  useEffect(() => {
    if (!orderID || fetchedRef.current) return;
    fetchedRef.current = true;
    fetchOrder();
  }, [orderID]);

  useEffect(() => {
    if (!order) return;
    fetchProducts();

    const address = `${order?.order.shipAddress}, ${order?.order.shipCity}, ${order?.order.shipPostalCode}, ${order.order.shipCountry}`;
    geocodeAddress(address).then((location) => {
      if (location) {
        setGeocodedLocation(location);
      } else {
        console.error("Failed to geocode address");
      }
    });
  }, [order]);

  const findProductById = (productID: number) => {
    return products.find((product) => product.productID === productID);
  };

  const handleEditOrder = (orderId: number) => {
    // Navigate to the create order page
    router.push(`/order/edit/${orderID}`);
  };

  const handleDeleteOrder = useCallback(
    (order: IOrder) => {
      setOrderDelete(order);
      setShowDeleteModal(true);
    },
    [setOrderDelete]
  );

  const calculatedTotal = useMemo(() => {
    if (!order) return 0;
    return order.orderDetails.reduce((sum, detail) => {
      return sum + detail.unitPrice * detail.quantity;
    }, 0);
  }, [order]);

  if (!order) {
    return <div className="mt-6">Loading order details...</div>;
  }

  const handleGenerateReport = async () => {
    setLoading(true);
    try {
      await getOrderByIdReport(parseInt(orderID));
    } catch (error) {
      toast.error("Error generating report");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
    {loading && (
      <div className="fixed inset-0 flex items-center justify-center bg-overlay z-50">
        <div className="bg-primary-100 px-16 py-8 rounded shadow-lg text-2xl font-bold">
          <p className="animate-pulse">Generating report...</p>
        </div>
      </div>
    )}
    <div className="w-full">
      <div className="flex justify-between items-center h-[3rem] justify-end">
        <div className="flex gap-4">
          <DefaultButton type="button" onClick={handleGenerateReport} outline>
            <span className="md:hidden"><DocumentArrowDownIcon className="h-5 w-5"/></span>
            <span className="hidden md:inline">Generate Report</span>
          </DefaultButton>
          {!editing ? 
            <DefaultButton type="button" onClick={() => handleEditOrder(parseInt(orderID))} className="bg-primary-dark-600! hover:bg-primary-dark-700!">
              <span className="md:hidden"><PencilIcon className="h-5 w-5"/></span>
              <span className="hidden md:inline">Edit Order</span>
            </DefaultButton> :
            <DefaultButton type="button" onClick={() => {}} className="bg-primary-dark-600! hover:bg-primary-dark-700!">
              Save Order Changes
            </DefaultButton>
          }
          <DefaultButton type="button" onClick={() => {handleDeleteOrder(order?.order)}} className="bg-red-600! hover:bg-red-700!">
            <span className="md:hidden"><TrashIcon className="h-5 w-5"/></span>
            <span className="hidden md:inline">Delete Order</span>
          </DefaultButton>
        </div>
      </div>

      {order ? (
        <div className="mt-6">
          <div className="space-y-6">
            <div className="bg-white p-4 shadow rounded">
              <h2 className="text-2xl font-semibold">Order Information</h2>
              <div className="flex flex-col md:flex-row gap-4 mt-4">
                <p><strong>Order #</strong> {order.order.orderID}</p>
                <p><strong>Customer:</strong> {order.order.customer?.companyName}</p>
                <p><strong>Employee:</strong> {order.order.employee?.firstName} {order.order.employee?.lastName}</p>
                <p><strong>Order Date:</strong> {new Date(order.order.orderDate).toLocaleDateString()}</p>
              </div>
            </div>
            <div className="bg-white p-4 shadow rounded">
              <h2 className="text-2xl font-semibold">Order Details</h2>
              <div className="overflow-x-auto mt-4 max-h-[30rem] overflow-y-auto">
                <table className="min-w-full table-auto border-collapse">
                  <thead className="bg-gray-100">
                    <tr>
                      <th className="px-4 py-2 text-left border-b">Product ID</th>
                      <th className="px-4 py-2 text-left border-b">Product Name</th>
                      <th className="px-4 py-2 text-left border-b">Quantity</th>
                      <th className="px-4 py-2 text-left border-b">Unit Price</th>
                      <th className="px-4 py-2 text-left border-b">Total Price</th>
                    </tr>
                  </thead>
                  <tbody>
                    {order.orderDetails.map((detail: IOrderDetail) => {
                      const product = findProductById(detail.productID);
                      if (!product) return null; // Skip if product not found
                      const total = (detail.unitPrice * detail.quantity).toFixed(2);
                      const unitPrice = detail.unitPrice.toFixed(2);
                      const quantity = detail.quantity.toFixed(2);
                      const productName = product.productName || 'Product Not Found';
                      const productID = detail.productID || 'Product Not Found';

                      return (
                        <tr key={productID} className="border-b">
                          <td className="px-4 py-2">{detail.productID}</td>
                          <td className="px-4 py-2">{productName}</td>
                          <td className="px-4 py-2">{quantity}</td>
                          <td className="px-4 py-2">
                            <div className="flex justify-between">
                                <span>$</span>
                                <span>{unitPrice}</span>
                              </div>
                            </td>
                          <td className="px-4 py-2">
                            <div className="flex justify-between">
                              <span>$</span>
                              <span>{total}</span>
                            </div>
                          </td>
                        </tr>
                      );
                    })}
                    <tr className="bg-gray-100">
                      <td colSpan={4} className="px-4 py-2 text-right font-semibold">Total</td>
                      <td className="px-4 py-2">
                        <div className="flex justify-between">
                          <span>$</span>
                          <span>{calculatedTotal.toFixed(2)}</span>
                        </div>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>

            <div className="bg-white p-4 shadow rounded">
              <h2 className="text-2xl font-semibold">Shipping Information</h2>
              <p className="py-4"><strong>Address:</strong> {order.order.shipAddress}</p>
              <div className="pb-4 flex flex-col md:flex-row gap-4">
                <p><strong>City:</strong> {order.order.shipCity}</p>
                <p><strong>Region:</strong> {order.order.shipRegion}</p>
                <p><strong>Postal Code:</strong> {order.order.shipPostalCode}</p>
                <p><strong>Country:</strong> {order.order.shipCountry}</p>
                <p><strong>Shipper:</strong> {order.order.shipper?.companyName}</p>
                <p><strong>Geocoded location:</strong> {geocodedLocation?.lat}, {geocodedLocation?.lng}</p>
              </div>
                
              {MAP_KEY && (
                <APIProvider apiKey={MAP_KEY}>
                  <Map 
                    mapId={MAP_ID}
                    className="h-[30rem]" 
                    defaultZoom={15} 
                    center={geocodedLocation}
                    gestureHandling="auto"
                    disableDefaultUI={true}
                    mapTypeControl={true}
                    zoomControl={true}
                  >
                    <AdvancedMarker position={geocodedLocation} />
                  </Map>
                </APIProvider>
              )}
              
            </div>
          </div>
        </div>
      ) : (
        <div className="mt-6">Loading order details...</div>
      )}
      {showDeleteModal && (
          <OrderDeleteModal
            setShowModal={setShowDeleteModal}
            initialState={orderDelete}
          />
        )}
    </div>
    </>
  );
};

export default OrderDetailPage;
