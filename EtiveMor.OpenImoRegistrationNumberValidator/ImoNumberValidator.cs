namespace EtiveMor.OpenImoRegistrationNumberValidator
{

    public static class ImoNumberValidator
    {
        /// <summary>
        /// Validates an IMO number for either a ship or a company. If the number is invalid for any reason
        /// the method returns <see cref="ImoNumberType.Invalid"/>. Otherwise it returns the type of IMO number
        /// </summary>
        /// <param name="imoNumber">The IMO number of either a ship or company</param>
        /// <returns>
        /// An <see cref="ImoNumberType"/> indicating the type of IMO number
        /// </returns>
        public static ImoNumberType ValidateImoNumber(string imoNumber)
        {
            if (IsValidShipRegistrationNumber(imoNumber))
            {
                return ImoNumberType.Ship;
            }
            if (IsValidCompanyRegistrationNumber(imoNumber))
            {
                return ImoNumberType.Company;
            }
            return ImoNumberType.Invalid;
        }

        /// <summary>
        /// Check if the given IMO number is valid for a ship. If the number is invalid for any reason
        /// returns a false, otherwise returns true.
        /// 
        /// This method will not throw any exceptions. To get the reason of the invalidity
        /// call the method <see cref="ValidateShipRegistrationNumber(string)"/> directly
        /// </summary>
        /// <param name="imoNumber">A ship's IMO number</param>
        /// <returns>true or false, indicating if the IMO number was valid</returns>
        public static bool IsValidShipRegistrationNumber(string imoNumber)
        {
            try { ValidateShipRegistrationNumber(imoNumber); return true; }
            catch { return false; }
        }

        /// <summary>
        /// Tests that the number passes common validation checks. If the number is invalid for any reason
        /// an exception is thrown.
        /// </summary>
        /// <param name="imoNumber">the IMO number to test</param>
        /// <returns>true if the given number passes basic validaion</returns>
        /// <exception cref="ImoNumberValidationException">The reason for the number being invalid</exception>
        private static bool CheckNumberPassesCommonValidation(string imoNumber)
        {
            if (!imoNumber.StartsWith("IMO"))
            {
                throw new ImoNumberValidationException("IMO number must start with 'IMO'");
            }
            if (imoNumber.Length != 10)
            {
                throw new ImoNumberValidationException($"IMO number must be 10 characters long. Received {imoNumber.Length} long string");
            }
            return true;
        }

        /// <summary>
        /// Validates the given IMO number. If the number is invalid for any reason
        /// 
        /// A ship's number must begin with "IMO", and be followed by 7 digits. This method will accept
        /// a case-insensitive string, and will remove any spaces in the string.
        /// </summary>
        /// <param name="imoNumber">
        /// The IMO number to validate. This should be a string of 10 characters, starting with "IMO"
        /// </param>
        /// <returns>
        /// a true if the IMO number is valid, otherwise throws an exception
        /// </returns>
        /// <exception cref="ImoNumberValidationException">
        /// An exception detailing the reason the IMO number was invalid
        /// </exception>
        public static bool ValidateShipRegistrationNumber(string imoNumber)
        {
            imoNumber = imoNumber.ToUpper();
            imoNumber = imoNumber.Replace(" ", "");

            CheckNumberPassesCommonValidation(imoNumber);

            string numberPart = imoNumber.Replace("IMO", "");

            int sum = 0;
            for (int i = 0; i < numberPart.Length - 1; i++)
            {
                bool isDigit = int.TryParse(numberPart[i].ToString(), out int digit);
                if (!isDigit)
                {
                    throw new ImoNumberValidationException($"The character '{numberPart[i]}' at index {i + 3} was not a digit");
                }
                
                int multiplier = 7 - i;
                sum += digit * multiplier;
            }

            string sumAsString = sum.ToString();
            char sumLastDigit = sumAsString[sumAsString.Length - 1];
            char lastDigit = numberPart[numberPart.Length - 1];
            if (sumLastDigit != lastDigit)
            {
                throw new ImoNumberValidationException($"The last digit of the IMO number was incorrect. Expected {sumLastDigit}, got {lastDigit}");
            }
            return true;
        }



        /// <summary>
        /// Check if the given IMO number is valid for a company. If the number is invalid for any reason
        /// returns a false, otherwise returns true.
        /// 
        /// This method will not throw any exceptions. To get the reason of the invalidity
        /// call the method <see cref="ValidateShippingCompanyRegistrationNumber(string)"/> directly
        /// </summary>
        /// <param name="imoNumber">A company's IMO number</param>
        /// <returns>true or false, indicating if the IMO number was valid</returns>
        public static bool IsValidCompanyRegistrationNumber(string imoNumber)
        {
            try { ValidateShippingCompanyRegistrationNumber(imoNumber); return true; }
            catch { return false; }
        }

        /// <summary>
        /// Validates a shipping company's IMO number. If the number is invalid for any reason
        /// throws an exception. Otherwise returns true
        /// </summary>
        /// <param name="imoNumber">A company's IMO number</param>
        /// <returns></returns>
        /// <exception cref="ImoNumberValidationException"></exception>
        public static bool ValidateShippingCompanyRegistrationNumber(string imoNumber)
        {
            // These numbers are defined by the IMO. The first digit in the imo number is 
            // multipl by 8, the second by 6, the third by 4, the fourth by 2, the fifth by 9, and the sixth by 7
            // The seventh digit is a check digit, which is calculated below
            int[] weights = [8, 6, 4, 2, 9, 7];

            imoNumber = imoNumber.ToUpper();
            imoNumber = imoNumber.Replace(" ", "");

            CheckNumberPassesCommonValidation(imoNumber);

            string numberPart = imoNumber.Replace("IMO", "");

            int sum = 0;
            for (int i = 0; i < numberPart.Length -1; i++)
            {
                bool isDigit = int.TryParse(numberPart[i].ToString(), out int digit);
                if (!isDigit)
                {
                    throw new ImoNumberValidationException($"The character '{numberPart[i]}' at index {i + 3} was not a digit");
                }

                int multiplier = weights[i % weights.Length];
                sum += digit * multiplier;
            }
            // Take the result of sum, and apply the mod11
            int mod11 = sum % 11;

            // subtract the mod11 from 11
            mod11 = 11 - mod11;

            // take the mod10 of the result
            int mod10 = mod11 % 10;

            // Check that the last digit of the IMO number is the same as the mod10 result
            char mod10Char = mod10.ToString()[0];
            char lastDigit = numberPart[numberPart.Length - 1];
            if (mod10Char != lastDigit)
            {
                throw new ImoNumberValidationException($"The last digit of the IMO number was incorrect. Expected {mod10Char}, got {lastDigit}");
            }
            return true;
        }
    }
}
