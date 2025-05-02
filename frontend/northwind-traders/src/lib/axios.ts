import { API_URL } from "@/consts/consts";
import axios from "axios";
import { toast } from "sonner";

const api = axios.create({
  baseURL: `${API_URL}`,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response?.status === 404) {
      toast.warning(error.response.data.message);
    }
    return Promise.reject(error);
  }
)

export default api;