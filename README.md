# Open-Imo-Registration-Number-Validator 🔍✔


## What is this?

An unofficial open source validator for the International Maritime Organisation (IMO)'s registration numbers. Ships and shipping companies have IMO Registration numbers, made up of "IMO" followed by seven numerical digits. The digits are validated using a checksum. This application implements validation for both companies and ships in C#. 

## Getting Started

The library can be installed via Nuget

```bash
dotnet add package EtiveMor.OpenImoRegistrationNumberValidator --version 1.0.0
```

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



# IMO Number Validation Algorithm

All IMO numbers must be `10` characters in length. The first three characters are uppercase `"IMO"`. The next 6 characters are a set of numbers (unique to each ship). The final (7th) character is a check-digit. Ships and Companies have two different algorithms to calculate the check-digit integrity, outlined below:

## Validating a ship's IMO check-digit

**Weights**: `[7, 6, 5, 4, 3, 2]`

- Take each of the first six numerical digits in the `input-string` (ignore the "IMO" characters)
- Multiply each by its corresponding weight, so 
  - Digit 1 is multiplied by `7`
  - Digit 2 is multiplied by `6`
  - Digit 3 is multiplied by `5`
  - Digit 4 is multiplied by `4`
  - Digit 5 is multiplied by `3`
  - Digit 6 is multiplied by `2`
- Sum the results of the multiplication step
- Take the last character from the sum 
  - Check against the last character for the `input-string`


### **Example**: The ship **Nordic Sola**'s IMO number `"IMO9375989"`

- Multiply each numerical char by its weight: 
  - $(9 \times 7) + (3 \times 6) + (7 \times 5) + (5 \times 4) + (9 \times 3) + (8 \times 2) = 179$
- Take the last digit of `179` (`9`)
- Take the last digit of `IMO9375989` (9)
- Compare the two:
  - If they are the same (`9` in this case) then the number **is** a valid Ship IMO Registration number
  - If they are different, the IMO number **is not** a valid Ship IMO Registration number


## Validating a company's IMO check-digit

**Weights**: `[8, 6, 4, 2, 9, 7]`

- Take each of the first six numerical digits in the `input-string` (ignore the "IMO" characters)
- Multiply each by its corresponding weight, so 
  - Digit 1 is multiplied by `8`
  - Digit 2 is multiplied by `6`
  - Digit 3 is multiplied by `4`
  - Digit 4 is multiplied by `2`
  - Digit 5 is multiplied by `9`
  - Digit 6 is multiplied by `7`
- Sum the results of the multiplication step
- With the result, apply the `mod11` algorithm
- Subtract the results of the `mod11` step from 11 
- With the result, apply the `mod10` algorithm
- Take the last character from the `input-string` and compare to the `mod10` result


### **Example**: The company **EVERGREEN MARINE CORP**'s IMO number `"IMO0344771"`

- Multiply each numerical char by its weight: 
  - $(8 \times 0) + (6 \times 3) + (4 \times 4) + (2 \times 4) + (9 \times 7) + (7 \times 7) = 154$
- Apply `mod11` to 154
  - $(154) \mod{11} = 0$
- Subtract `0` from `11`
  - $11 - 0 = 11$
- Apply `mod10` to 11
  - $11 \mod{10} = 1$
- Take the last digit of `IMO0344771`: `1`
- Compare the two:
  - If they are the same (`1` in this case) then the number **is** a valid Company IMO Registration number
  - If they are different, the IMO number **is not** a valid Company IMO Registration number


# References & datasets

- IMO Number Wiki page: https://en.wikipedia.org/wiki/IMO_number
- All ship and company IMO numbers: https://gisis.imo.org/Public/Default.aspx
- IMO Ship Identification Number Schemes: https://www.imo.org/en/OurWork/MSAS/Pages/IMO-identification-number-scheme.aspx
