"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import DefaultButton from "@/components/Button/DefaultButton";
import { IEmployee } from "@/types/Employee/employee";
import { getEmployees } from "@/services/employeeService";
import Card from "@/components/Card/Card";

const Employee = () => {
  const [employees, setEmployees] = useState<IEmployee[]>([]);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const data = await getEmployees();
        setEmployees(data);
      } catch (error) {
        console.error("Error loading employees", error);
      } finally {
        setLoading(false);
      }
    };

    fetchEmployees();
  }, []);

  const handleViewOrders = (employeeID: number) => {
    router.push(`/employee/${employeeID}`);
  };

  return (
    <div className="w-full p-6">
      <h1 className="text-3xl font-bold mb-6">Employees</h1>

      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
          {employees.map((employee: IEmployee) => (
            <Card key={employee.employeeID}>
              <h2 className="text-xl font-semibold">{employee.lastName} {employee.firstName}</h2>
              <div className="mt-3">
                <DefaultButton type="button" onClick={() => handleViewOrders(employee.employeeID)}>
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