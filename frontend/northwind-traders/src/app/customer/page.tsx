"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import DefaultButton from "@/components/Button/DefaultButton";
import { getCustomers } from "@/services/customerService";
import { ICustomer } from "@/types/Customer/customer";

const Customer = () => {
  const [customers, setCustomers] = useState<ICustomer[]>([]);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        const data = await getCustomers();
        setCustomers(data);
      } catch (error) {
        console.error("Error loading customers", error);
      } finally {
        setLoading(false);
      }
    };

    fetchCustomers();
  }, []);

  const handleViewOrders = (customerID: string) => {
    router.push(`/customer/${customerID}`);
  };

  return (
    <div className="w-full p-6">
      <h1 className="text-3xl font-bold mb-6">Customers</h1>

      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
          {customers.map((customer: ICustomer) => (
            <div key={customer.customerID} className="border p-4 rounded shadow">
              <h2 className="text-xl font-semibold">{customer.companyName}</h2>
              <div className="mt-3">
                <DefaultButton type="button" onClick={() => handleViewOrders(customer.customerID)}>
                  View Orders
                </DefaultButton>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default Customer;