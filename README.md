# Open-Imo-Registration-Number-Validator 🔍✔


## What is this?

An unofficial open source validator for the International Maritime Organisation (IMO)'s registration numbers. Ships and shipping companies have IMO Registration numbers, made up of "IMO" followed by seven numerical digits. The digits are validated using a checksum. This application implements validation for both companies and ships in C#. 

## Getting Started

The library can be installed via Nuget.

# Available methods

## Validate arbitrary strings
Once installed, run `ImoNumberValidator.ValidateImoNumber(imoNumber);`, where `imoNumber` is your 10 character IMO number. The method will return an Enum of type `EtiveMor.OpenImoRegistrationNumberValidator.ImoNumberType`:

```csharp
public enum ImoNumberType
{
    Invalid = -1,
    Ship = 1,
    Company = 2
}
```

## **Ship** IMO number validation 🚢

```csharp 
  bool shipResult = ImoNumberValidator.IsValidShipRegistrationNumber(companyImoNumber);
```
The method validates a **Ship**'s registration number. Returns either `true` if the number is a valid ship IMO number. Returns `false` in any other scenario - the method does not return details of the validation failure. 

### **Ship** IMO number validation (with descriptive exceptions)

```csharp
try{
  // success condition
  bool shipResult = ImoNumberValidator.ValidateShipRegistrationNumber(companyImoNumber);
} catch (ImoNumberValidationException ex) {
  // fail condition
  // ...
}
```
The method validates a **Ship**'s registration number. Returns either `true` if the number is a valid ship. Throws an `ImoNumberValidationException` in any other scenario. The `ImoNumberValidationException`'s `Message` property includes details of the failure. 


## **Company** IMO number validation 💼

```csharp 
  bool companyResult = ImoNumberValidator.IsValidCompanyRegistrationNumber(companyImoNumber);
```
The method validates a **Company**'s registration number. Returns either `true` if the number is a valid company. Returns `false` in any other scenario - the method does not return details of the validation failure. 

### **Company** IMO number validation (with descriptive exceptions)

```csharp 
  try{
    // success condition
    bool companyResult = ImoNumberValidator.ValidateShippingCompanyRegistrationNumber(companyImoNumber);
  } catch (ImoNumberValidationException ex) {
    // fail condition
    // ...
  }
```
The method validates a **Company**'s registration number. Returns either `true` if the number is a valid company. Throws an `ImoNumberValidationException` in any other scenario. The `ImoNumberValidationException`'s `Message` property includes details of the failure. 



# Validation Algorithm


