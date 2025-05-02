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

export const getOrdersReport = async () => {
  const response = await api.get(`Order/report`, {
    responseType: 'blob',  // Ensure the response is treated as a blob
  });

  // Create a URL for the PDF blob content
  const fileURL = URL.createObjectURL(response.data);

  // Open the PDF in a new tab
  window.open(fileURL, '_blank');
}

export const getOrderByIdReport = async (id: number) => {
  const response = await api.get(`Order/report/${id}`, {
    responseType: 'blob',  // Ensure the response is treated as a blob
  });

  // Create a URL for the PDF blob content
  const fileURL = URL.createObjectURL(response.data);

  // Open the PDF in a new tab
  window.open(fileURL, '_blank');
}

export const createOrder = async (data: ICreateOrderDto) => {
  const response = await api.post(`Order`, data);
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
