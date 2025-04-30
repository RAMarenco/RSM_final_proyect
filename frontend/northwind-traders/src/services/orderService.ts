import api from "@/lib/axios";
import { ICreateOrderDto, IUpdateOrderDto } from "@/types/Order/order.dto";

export const getOrders = async (page: number) => {
  const response = await api.get(`Order?pageNumber=${page}`);
  return response.data;
};

export const getOrdersWithDetails = async (id: number) => {
  const response = await api.get(`Order/${id}`);
  return response.data;
}

export const createOrder = async (id: number, data: ICreateOrderDto) => {
  const response = await api.post(`Order/${id}`, data);
  return response.data;
}

export const updateOrder = async (id: number, data: IUpdateOrderDto) => {
  const response = await api.put(`Order/${id}`, data);
  return response.data;
}

export const deleteOrder = async (id: number) => {
  const response = await api.delete(`Order/${id}`);
  return response.data;
};
