import { ICustomer } from "../Customer/customer";
import { IEmployee } from "../Employee/employee";
import { IOrderDetail } from "../OrderDetail/orderDetail";
import { IShipper } from "../Shipper/shipper";

export interface IOrder {
  orderID: number;
  orderDate: Date;
  shipAddress: string;
  shipCity: string;
  shipRegion: string;
  shipPostalCode: string;
  shipCountry: string;
  customer?: ICustomer;
  shipper?: IShipper
  employee?: IEmployee;
}

export interface IOrderWithDetails {
  order: IOrder;
  orderDetails: IOrderDetail[];
}