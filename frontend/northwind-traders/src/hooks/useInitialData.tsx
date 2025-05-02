import { getCustomers } from "@/services/customerService";
import { getEmployees } from "@/services/employeeService";
import { getProducts } from "@/services/productService";
import { getShippers } from "@/services/shipperService";
import { ICustomer } from "@/types/Customer/customer";
import { IEmployee } from "@/types/Employee/employee";
import { IProduct } from "@/types/Product/product";
import { IShipper } from "@/types/Shipper/shipper";
import { useEffect, useState } from "react";
import { toast } from "sonner";

export const useInitialData = () => {
  const [customers, setCustomers] = useState<ICustomer[]>([]);
  const [employees, setEmployees] = useState<IEmployee[]>([]);
  const [shippers, setShippers] = useState<IShipper[]>([]);
  const [products, setProducts] = useState<IProduct[]>([]);

  useEffect(() => {
    Promise.all([getCustomers(), getEmployees(), getShippers(), getProducts()])
      .then(([c, e, s, p]) => {
        setCustomers(c);
        setEmployees(e);
        setShippers(s);
        setProducts(p);
      })
      .catch(() => toast.error("Error loading data"));
  }, []);

  return { customers, employees, shippers, products };
};