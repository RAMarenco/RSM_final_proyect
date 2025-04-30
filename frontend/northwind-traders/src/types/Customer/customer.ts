import { IOrder } from "../Order/Order";

export interface ICustomer {
  customerID: string;
  companyName: string;
}

export interface ICustomerWithOrders {
  customer: ICustomer;
  orders: IOrder[];
}
