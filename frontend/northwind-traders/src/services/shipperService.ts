import api from "@/lib/axios";

export const getShippers = async () => {
  const response = await api.get("Shipper");
  return response.data;
};

export const getShipperWithOrders = async (id: number) => {
  const response = await api.get(`Shipper/${id}`);
  return response.data;
};