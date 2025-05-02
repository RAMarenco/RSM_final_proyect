import { MAP_KEY } from "@/consts/consts";
import { APIProvider, Map, Marker } from "@vis.gl/react-google-maps";
import { useState } from "react";
import PlaceAutocomplete from "../PlaceAutoComplete/PlaceAutoComplete";
import { geocodeAddress } from "@/services/googleService";
import DefaultButton from "../Button/DefaultButton";

const OrderFormPage = () => {
  const defaultLocation = { lat: 13.6929, lng: -89.2182 }; // San Salvador
  const [location, setLocation] = useState(defaultLocation);

  const handleOnPlaceSelect = (place: google.maps.places.PlaceResult | null) => {
    if (!place) return;
    console.log(place);
    geocodeAddress(place.adr_address!).then((location) => {
      if (location) {
        setLocation(location);
      } else {
        console.error("Failed to geocode address");
      }
    });
  }

  return (
    <div className="w-full">
      {MAP_KEY && (
        <APIProvider apiKey={MAP_KEY}>
          <PlaceAutocomplete onPlaceSelect={handleOnPlaceSelect}/>
          <DefaultButton type="button" onClick={() => console.log(location)} className="mt-4">
            Verify Address
          </DefaultButton>
          <Map 
            className="h-[30rem]" 
            defaultZoom={15} 
            center={location}
            gestureHandling="auto"
            disableDefaultUI={true}
            mapTypeControl={true}
            zoomControl={true}
          >
            <Marker position={location} />
          </Map>
        </APIProvider>
      )}
    </div>
  )
}

export default OrderFormPage;