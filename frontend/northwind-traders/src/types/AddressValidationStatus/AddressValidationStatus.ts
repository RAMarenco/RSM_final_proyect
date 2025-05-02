export enum AddressValidationStatus {
  NotValidated = 0,
  Validated = 1,
  Invalid = 2,
}

export const AddressValidationStatusText = {
  [AddressValidationStatus.NotValidated]: "Not Validated",
  [AddressValidationStatus.Validated]: "Validated",
  [AddressValidationStatus.Invalid]: "Invalid",
};