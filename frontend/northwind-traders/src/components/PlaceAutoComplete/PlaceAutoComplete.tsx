"use client";
import { RefObject, useEffect, useRef, useState } from "react";
import { useMapsLibrary } from "@vis.gl/react-google-maps";

type PlaceAutocompleteProps = {
    onPlaceSelect: (place: google.maps.places.PlaceResult | null) => void;
    placeholder?: string;
    className?: string;
    setAddress?: (address: string) => void;
    inputRef: RefObject<HTMLInputElement | null>;
};

const PlaceAutocomplete = ({
    onPlaceSelect,
    placeholder = "Enter an address",
    inputRef,
    setAddress,
}: PlaceAutocompleteProps) => {
  const places = useMapsLibrary('places');
  const autocompleteRef = useRef<google.maps.places.Autocomplete | null>(null);
  const [isInputFocused, setIsInputFocused] = useState(false);

  useEffect(() => {
    if (!places || !inputRef.current) return;

    // Create options object
    const options = {
      fields: ["address_components", "geometry", "formatted_address"],
    };

    // Initialize the autocomplete
    autocompleteRef.current = new google.maps.places.Autocomplete(
      inputRef.current,
      options
    );

    // Set up event listener for place_changed
    const placeChangedListener = () => {
      const place = autocompleteRef.current?.getPlace();
      onPlaceSelect(place || null);
      if (place && setAddress) {
        setAddress(place.formatted_address || '');
      }
    };

    autocompleteRef.current.addListener('place_changed', placeChangedListener);

    // Cleanup function to remove the event listener
    return () => {
      if (autocompleteRef.current) {
        google.maps.event.clearInstanceListeners(autocompleteRef.current, 'place_changed');
        autocompleteRef.current = null;
      }
    };
  }, [places, onPlaceSelect, setAddress]);

  useEffect(() => {
      const inputElement = inputRef.current;
      if (!inputElement) return;

      const handleInputChange = () => {
        if (isInputFocused) { // Only update if input is focused
          return; // Do nothing, let autocomplete handle it.
        }
        if (autocompleteRef.current) {
          const place = autocompleteRef.current.getPlace();
          if (place) {
            onPlaceSelect(place || null);
            if (place && setAddress) {
              setAddress(place.formatted_address || '');
            }
          }
        }
      };

      const handleFocus = () => {
        setIsInputFocused(true);
      };

      const handleBlur = () => {
        setIsInputFocused(false);
      };

      inputElement.addEventListener('input', handleInputChange);
      inputElement.addEventListener('focus', handleFocus);
      inputElement.addEventListener('blur', handleBlur);

      return () => {
        inputElement.removeEventListener('input', handleInputChange);
        inputElement.removeEventListener('focus', handleFocus);
        inputElement.removeEventListener('blur', handleBlur);
      };
  }, [setAddress, onPlaceSelect, isInputFocused]);

  return (
    <div className="mb-0 w-full">
      <label className="block text-sm font-medium text-gray-900">
        Address
      </label>
      <div>
        <input
          ref={inputRef}
          placeholder={placeholder}
          className={`bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full
              p-2.5 disabled:bg-gray-200 disabled:text-gray-500 disabled:cursor-not-allowed`}
        />
      </div>
    </div>
  );
};

export default PlaceAutocomplete;
