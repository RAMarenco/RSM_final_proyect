import { IOrder } from "../Order/Order";

export interface IShipper {
  shipperID: number;
  companyName: string;
  phone: string;
}

export interface IShipperWithOrders {
  shipper: IShipper;
  orders: IOrder[];
}
