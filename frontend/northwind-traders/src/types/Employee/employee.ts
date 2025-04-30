import { IOrder } from "../Order/Order";

export interface IEmployee {
  employeeID: number;
  lastName: string;
  firstName: string;
}

export interface IEmployeeWithOrders {
  employee: IEmployee;
  orders: IOrder[];
}
