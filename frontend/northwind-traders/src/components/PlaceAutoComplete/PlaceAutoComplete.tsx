"use client";
import { useEffect, useRef } from "react";
import { useMapsLibrary } from "@vis.gl/react-google-maps";

type PlaceAutocompleteProps = {
  onPlaceSelect: (place: google.maps.places.PlaceResult | null) => void;
  placeholder?: string;
  className?: string;
};

const PlaceAutocomplete = ({ 
  onPlaceSelect, 
  placeholder = "Enter an address",
  className = "input"
}: PlaceAutocompleteProps) => {
  const inputRef = useRef<HTMLInputElement>(null);
  const autocompleteRef = useRef<google.maps.places.Autocomplete | null>(null);
  const places = useMapsLibrary('places');

  useEffect(() => {
    if (!places || !inputRef.current) return;

    autocompleteRef.current = new places.Autocomplete(
      inputRef.current,
      {
        types: ["address"],
        fields: ["address_components", "geometry", "formatted_address"],
      }
    );

    autocompleteRef.current.addListener("place_changed", () => {
      const place = autocompleteRef.current?.getPlace();
      onPlaceSelect(place || null);
    });

    return () => {
      if (autocompleteRef.current) {
        google.maps.event.clearInstanceListeners(autocompleteRef.current);
      }
    };
  }, [places, onPlaceSelect]);

  return (
    <input
      ref={inputRef}
      type="text"
      placeholder={placeholder}
      className={className}
    />
  );
};

export default PlaceAutocomplete;