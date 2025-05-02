import api from "@/lib/axios";

export const getCustomers = async () => {
  const response = await api.get("Customer");
  return response.data;
};

export const getCustomerWithOrders = async (id: string) => {
  const response = await api.get(`Customer/${id}`);
  return response.data;
};