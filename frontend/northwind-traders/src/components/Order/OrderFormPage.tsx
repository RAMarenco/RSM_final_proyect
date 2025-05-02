import { useEffect, useRef, useState } from "react";
import { APIProvider, Map, AdvancedMarker } from "@vis.gl/react-google-maps";
import { validateAddress } from "@/services/googleService";
import { MAP_ID, MAP_KEY } from "@/consts/consts";
import DefaultButton from "../Button/DefaultButton";
import { toast } from "sonner";
import { AddressFields } from "@/types/AddressFields/AddressFields";
import { IEmployee } from "@/types/Employee/employee";
import { IShipper } from "@/types/Shipper/shipper";
import { ICustomer } from "@/types/Customer/customer";
import { IProductDto } from "@/types/Product/product.dto";
import NegativeButton from "../Button/NegativeButton";
import { IUpdateOrderDto } from "@/types/Order/order.dto";
import { AddressValidationStatus, AddressValidationStatusText } from "@/types/AddressValidationStatus/AddressValidationStatus";
import MapAddressValidation from "./MapAddresValidation";
import { extractAddressFields } from "@/util/extractAddressFields";
import { useInitialData } from "@/hooks/useInitialData";
import { useSaveOrder } from "@/hooks/useSaveOrder";

type OrderFormPageProps = {
  mode: 'create' | 'edit';
  existingOrder?: IUpdateOrderDto;
  orderID?: number;
};

const OrderFormPage = ({ mode, existingOrder, orderID }: OrderFormPageProps) => {
  const defaultLocation = { lat: 13.6929, lng: -89.2182 };
  const addressRef = useRef<HTMLInputElement>(null);
  const { customers, employees, shippers, products } = useInitialData();
  const [address, setAddress] = useState<string>("");
  const [addressValid, setAddressValid] = useState<AddressValidationStatus>(AddressValidationStatus.NotValidated);
  const [location, setLocation] = useState(defaultLocation);
  const [addressFields, setAddressFields] = useState<AddressFields>({
    shipCity: "",
    shipRegion: "",
    shipPostalCode: "",
    shipCountry: "",
  });

  const [customerID, setCustomerID] = useState("");
  const [employeeID, setEmployeeID] = useState<number>(0);
  const [shipVia, setShipVia] = useState<number>(0);
  const [orderDate, setOrderDate] = useState<string>("");
  const [selectedProducts, setSelectedProducts] = useState<IProductDto[]>([]);
  const [newProduct, setNewProduct] = useState<{ productID: number; quantity: number }>({ productID: 0, quantity: 1 });

  const handleSaveOrder = useSaveOrder({
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
  })

  const handleOnPlaceSelect = (place: google.maps.places.PlaceResult | null) => {
    if (!place) return;
    setAddress(place.formatted_address || "");
  };

  useEffect(() => {
    if (mode === 'edit' && existingOrder) {
      setShipVia(existingOrder.shipVia);
      setOrderDate(existingOrder.orderDate.split("T")[0]);
      setAddress(existingOrder.shipAddress);
      addressRef.current!.value = existingOrder.shipAddress;
      setAddressFields({
        shipCity: existingOrder.shipCity,
        shipRegion: existingOrder.shipRegion,
        shipPostalCode: existingOrder.shipPostalCode,
        shipCountry: existingOrder.shipCountry,
      });
      setSelectedProducts(existingOrder.products);
      verifyAddress();
    }
  }, [mode, existingOrder]);

  useEffect(() => {
    const input = addressRef.current;
    if (!input) return;
    const observer = new MutationObserver(() => {
      if (input.value !== address && addressValid !== AddressValidationStatus.Invalid) {
        setAddressValid(AddressValidationStatus.Invalid);
      }
    });
    observer.observe(input, { attributes: true, childList: true, subtree: true, characterData: true });
    return () => observer.disconnect();
  }, [address, addressValid]);

  const verifyAddress = () => {
    validateAddress(addressRef.current?.value ?? address).then((response) => {
      if (response?.status === 200) {
        setAddress(addressRef.current?.value!);
        if (response?.data.result.geocode) {
          const locationData = response.data.result.geocode.location;
          setLocation({ lat: locationData.latitude, lng: locationData.longitude });
          setAddressFields(extractAddressFields(response.data.result.address.addressComponents));
          toast.success("Address verified successfully");
          setAddressValid(AddressValidationStatus.Validated);
        } else {
          setAddressFields({ shipCity: "", shipRegion: "", shipPostalCode: "", shipCountry: "" });
          setAddressValid(AddressValidationStatus.Invalid);
          toast.info("No location found for this address");
        }
      }
    });
  };

  const handleAddProduct = () => {
    if (newProduct.productID && newProduct.quantity > 0) {
      const selected = products.find((p) => p.productID === newProduct.productID);
      if (selected) {
        // Check if product is already added
        if (!selectedProducts.some((p) => p.productID === newProduct.productID)) {
          // Add product to the selected list with product name
          setSelectedProducts([
            ...selectedProducts,
            { ...newProduct }, // Include product name
          ]);
        }
      }
      setNewProduct({ productID: 0, quantity: 1 });
    }
  };

  const handleRemoveProduct = (productID: number) => {
    setSelectedProducts(selectedProducts.filter((product) => product.productID !== productID));
  };

  return (
    <div className="w-full flex-col gap-4 items-center justify-center p-6 space-y-8">
      {MAP_KEY && (
        <APIProvider apiKey={MAP_KEY}>
          <div className="w-full flex flex-col gap-6">
            <div className="flex flex-col md:flex-row w-full justify-between items-center gap-12">
              {/* Form Section */}
              <div className="w-full md:min-h-[20rem] flex flex-col gap-4">
                {mode === 'edit' && (
                  <h2 className="font-bold text-2xl mb-auto">Editing order #{orderID}</h2>
                )}
                <div className="grid grid-cols-2 gap-4">
                  {mode !== 'edit' && (
                    <>
                      <label className="flex flex-col">
                        Customer
                        <select value={customerID} onChange={(e) => setCustomerID(e.target.value)} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full
                            p-2.5 disabled:bg-gray-200 disabled:text-gray-500 disabled:cursor-not-allowed">
                          <option value="">Select a customer</option>
                          {customers.map((c: ICustomer) => (<option key={c.customerID} value={c.customerID}>{c.companyName}</option>))}
                        </select>
                        {customerID === "" && <p className="text-red-500 text-sm pt-1">Select a customer*</p>}
                      </label>
                      <label className="flex flex-col">
                        Employee
                        <select value={employeeID} onChange={(e) => setEmployeeID(Number(e.target.value))} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full
                            p-2.5 disabled:bg-gray-200 disabled:text-gray-500 disabled:cursor-not-allowed">
                          <option value="">Select an employee</option>
                          {employees.map((e: IEmployee) => <option key={e.employeeID} value={e.employeeID}>{e.lastName} {e.firstName}</option>)}
                        </select>
                        {employeeID === 0 && <p className="text-red-500 text-sm pt-1">Select an employee*</p>}
                      </label>
                    </>
                  )}
                  <label className="flex flex-col">
                    Ship Via
                    <select value={shipVia} onChange={(e) => setShipVia(Number(e.target.value))} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full
                        p-2.5 disabled:bg-gray-200 disabled:text-gray-500 disabled:cursor-not-allowed">
                      <option value="">Select shipper</option>
                      {shippers.map((s: IShipper) => <option key={s.shipperID} value={s.shipperID}>{s.companyName}</option>)}
                    </select>
                    {shipVia === 0 && <p className="text-red-500 text-sm pt-1">Select a shipper*</p>}
                  </label>
                  <label className="flex flex-col">
                    Order Date
                    <input type="date" value={orderDate} onChange={(e) => setOrderDate(e.target.value)} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full
                        p-2.5 disabled:bg-gray-200 disabled:text-gray-500 disabled:cursor-not-allowed"/>
                      {orderDate === "" && <p className="text-red-500 text-sm pt-1">Select order date*</p>}
                  </label>
                </div>

                <MapAddressValidation 
                  addressValid={addressValid} 
                  addressRef={addressRef} 
                  handleOnPlaceSelect={handleOnPlaceSelect} 
                  setAddress={setAddress} 
                  verifyAddress={verifyAddress}/>
              </div>

              {/* Google Map */}
              <div>
                <div className="flex flex-row items-center justify-between mb-4">
                  <h3 className="font-semibold">Location</h3>
                  <h3 className="font-semibold">{AddressValidationStatusText[addressValid]}</h3>  
                </div>                
                {location && <p className="text-sm text-gray-500">Latitude: {location.lat}, Longitude: {location.lng}</p>}
                <Map
                  mapId={MAP_ID}
                  className="h-[30rem] w-full md:min-w-[40rem] rounded-xl shadow-lg"
                  defaultZoom={15}
                  center={location}
                  gestureHandling="auto"
                  disableDefaultUI
                  mapTypeControl
                  zoomControl
                >
                  <AdvancedMarker position={location} />
                </Map>
              </div>
            </div>

            {/* Products Section */}
            <div className="space-y-2 mt-6">
              <h3 className="font-semibold">Products</h3>
              <div className="flex gap-2 items-center">
                <select
                  value={newProduct.productID}
                  onChange={(e) => setNewProduct({ ...newProduct, productID: Number(e.target.value) })}
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
                >
                  <option value="">Select product</option>
                  {products
                    .filter((p) => !selectedProducts.some((sp) => sp.productID === p.productID)) // Exclude already selected
                    .map((p) => (
                      <option key={p.productID} value={p.productID}>
                        {p.productName}
                      </option>
                    ))}
                </select>
                <input
                  type="number"
                  value={newProduct.quantity}
                  onChange={(e) => setNewProduct({ ...newProduct, quantity: Number(e.target.value) })}
                  placeholder="Quantity"
                  className="border rounded p-2 w-1/3"
                />
                <DefaultButton type="button" onClick={handleAddProduct}  disabled={!newProduct.productID || newProduct.quantity <= 0}>
                  Add
                </DefaultButton>
              </div>

              {/* Display Selected Products in a Table */}
              <div className="overflow-x-auto">
                <table className="min-w-full table-auto">
                  <thead>
                    <tr className="bg-gray-100 border-b">
                      <th className="px-4 py-2 text-left">Product Name</th>
                      <th className="px-4 py-2 text-left">Quantity</th>
                      <th className="px-4 py-2 text-left">Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    {selectedProducts.map((product) => {
                      const productDetails = products.find((p) => p.productID === product.productID);
                      const productName = productDetails ? productDetails.productName : "Unknown Product";
                      return (
                        <tr key={product.productID} className="border-b">
                          <td className="px-4 py-2">{productName}</td>
                          <td className="px-4 py-2">{product.quantity}</td>
                          <td className="px-4 py-2">
                            <NegativeButton
                              type="button"
                              onClick={() => handleRemoveProduct(product.productID)}
                              className="cursor-pointer rounded-lg!"
                            >
                              Remove
                            </NegativeButton>
                          </td>
                        </tr>
                      );
                    })}
                  </tbody>
                </table>
              </div>
            </div>
            {/* Save Order Button */}
            <div className="flex justify-end mt-6">
              <DefaultButton type="button" onClick={handleSaveOrder} disabled={addressValid !== AddressValidationStatus.Validated} className="w-[12rem]">
                Save Order
              </DefaultButton>
            </div>
          </div>
        </APIProvider>
      )}
    </div>
  );
};

export default OrderFormPage;
