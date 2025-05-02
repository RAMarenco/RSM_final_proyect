import { ShieldCheckIcon } from "@heroicons/react/16/solid"
import DefaultButton from "../Button/DefaultButton"
import PlaceAutocomplete from "../PlaceAutoComplete/PlaceAutoComplete"
import AddressStatusIcon from "../AddressValidationStatus/AddressValidationStatus"

const MapAddressValidation = ({addressValid, addressRef, handleOnPlaceSelect, setAddress, verifyAddress} : {
  addressValid: number,
  addressRef: React.RefObject<HTMLInputElement | null>,
  handleOnPlaceSelect: (place: google.maps.places.PlaceResult | null) => void,
  setAddress: (address: string) => void,
  verifyAddress: () => void
}) => {
  return (
    <>
      <div className="flex items-center gap-4">
        <PlaceAutocomplete inputRef={addressRef} onPlaceSelect={handleOnPlaceSelect} setAddress={setAddress} />
        <DefaultButton type="button" className="h-[2.625rem] md:w-[10rem]! mt-5" onClick={verifyAddress}>
        <span className="hidden md:inline">Verify Address</span>
          <span className="md:hidden">
            <ShieldCheckIcon className="h-5 w-5"/>
          </span>
        </DefaultButton>
        <div className="h-full w-10">
          <AddressStatusIcon status={addressValid} />
        </div>
      </div>
    </>
  )
}

export default MapAddressValidation;