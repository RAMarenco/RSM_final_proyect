import api from "@/lib/axios";

export const getEmployees = async () => {
  const response = await api.get("Employee");
  return response.data;
};

export const getEmployeeWithOrders = async (id: number) => {
  const response = await api.get(`Employee/${id}`);
  return response.data;
};