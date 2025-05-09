import { MAP_KEY } from "@/consts/consts";
import axios from "axios";
import { toast } from "sonner";

export const geocodeAddress = async (fullAddress : string) => {
  try {
    const response = await axios.get("https://maps.googleapis.com/maps/api/geocode/json", {
      params: {
        address: fullAddress,
        key: MAP_KEY,
      },
    });

    if (response.data.status === "OK") {
      const { lat, lng } = response.data.results[0].geometry.location;
      return { lat, lng };
    } else {
      toast.error("Failed to geocode address");
    }
  } catch (error) {
    toast.error("Error geocoding address");
  }
};

export const validateAddress = async (address: string) => {
  try {
    const response = await axios.post(`https://addressvalidation.googleapis.com/v1:validateAddress?key=${MAP_KEY}`, {
      "address": {
        "addressLines": [address]
      }
    });

    return response;
  } catch (error) {
    toast.error("Error validating address");
  }
}