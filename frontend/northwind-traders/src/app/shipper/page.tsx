"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import DefaultButton from "@/components/Button/DefaultButton";
import { IEmployee } from "@/types/Employee/employee";
import { getEmployees } from "@/services/employeeService";
import { IShipper } from "@/types/Shipper/shipper";
import { getShippers } from "@/services/shipperService";
import Card from "@/components/Card/Card";

const Employee = () => {
  const [shippers, setShippers] = useState<IShipper[]>([]);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    const fetchShippers = async () => {
      try {
        const data = await getShippers();
        setShippers(data);
      } catch (error) {
        console.error("Error loading shippers", error);
      } finally {
        setLoading(false);
      }
    };

    fetchShippers();
  }, []);

  const handleViewOrders = (employeeID: number) => {
    router.push(`/shipper/${employeeID}`);
  };

  return (
    <div className="w-full p-6">
      <h1 className="text-3xl font-bold mb-6">Employees</h1>

      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
          {shippers.map((shipper: IShipper) => (
            <Card key={shipper.shipperID}>
              <h2 className="text-xl font-semibold">{shipper.companyName}</h2>
              <div className="mt-3">
                <DefaultButton type="button" onClick={() => handleViewOrders(shipper.shipperID)}>
                  View Orders
                </DefaultButton>
              </div>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
};

export default Employee;