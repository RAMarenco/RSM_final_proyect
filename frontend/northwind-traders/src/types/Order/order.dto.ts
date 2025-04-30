import { IProductDto } from "../Product/product.dto";

export interface ICreateOrderDto {
  customerID: string;
  employeeID: number;
  orderDate: string;
  shipVia: number;
  shipAddress: string;
  shipCity: string;
  shipRegion: string;
  shipPostalCode: string;
  shipCountry: string;
  products: IProductDto[];
}

export interface IUpdateOrderDto {
  orderDate: string;
  shipVia: number;
  shipAddress: string;
  shipCity: string;
  shipRegion: string;
  shipPostalCode: string;
  shipCountry: string;
  products: IProductDto[];
}