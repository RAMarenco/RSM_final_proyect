import { AddressFields } from "@/types/AddressFields/AddressFields";

export const extractAddressFields = (addressComponents: any[]): AddressFields => {
  const result: Partial<AddressFields> = {};
  for (const component of addressComponents) {
    const type = component.componentType;
    const text = component.componentName?.text || '';
    switch (type) {
      case 'locality': result.shipCity = text; break;
      case 'administrative_area_level_1': result.shipRegion = text; break;
      case 'postal_code': result.shipPostalCode = text; break;
      case 'country': result.shipCountry = text; break;
    }
  }
  return {
    shipCity: result.shipCity || '',
    shipRegion: result.shipRegion || '',
    shipPostalCode: result.shipPostalCode || '',
    shipCountry: result.shipCountry || '',
  };
};