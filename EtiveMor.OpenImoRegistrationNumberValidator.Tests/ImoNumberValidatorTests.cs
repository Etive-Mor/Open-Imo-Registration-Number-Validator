namespace EtiveMor.OpenImoRegistrationNumberValidator.Tests
{
    /// <summary>
    ///  A test suite which tests the <see cref="ImoNumberValidator"/> class
    ///  
    /// A set of known IMO numbers for ships and companies are tested to ensure that the validation
    /// is accurate. A set of known invalid strings are also tested to ensure that the validation fails
    /// </summary>
    [TestClass]
    public class ImoNumberValidatorTests
    {

        [DataRow("IMO1234567", ImoNumberType.Ship)]
        [DataRow("IMO0289239", ImoNumberType.Company)]
        [DataRow("IMO1234560", ImoNumberType.Invalid)]
        [TestMethod]
        public void ValidateImoNumberReturnsExpectedType(string imoNumber, ImoNumberType expectedNumberType)
        {
            var result = ImoNumberValidator.ValidateImoNumber(imoNumber);

            Assert.IsTrue(result == expectedNumberType);
        }


        [DataRow("IMO6342360")] //  JUNGHAUS C MAERSK MOLLER 
        [DataRow("IMO5808451")] // MAERSK A/S 
        [DataRow("IMO0344771")] //  EVERGREEN MARINE CORP 
        [DataRow("IMO0152944")] //  MSC MEDITERRANEAN SHIPPING CO 
        [TestMethod]
        public void TestKnownValidCompanyImoNumberReturnsValid(string imoNumber)
        {
            bool result = ImoNumberValidator.ValidateCompanyRegistrationNumber(imoNumber);

            Assert.IsTrue(result);
        }
        [DataRow("IMO6342360")] //  JUNGHAUS C MAERSK MOLLER 
        [DataRow("IMO5808451")] // MAERSK A/S 
        [DataRow("IMO0344771")] //  EVERGREEN MARINE CORP 
        [DataRow("IMO0152944")] //  MSC MEDITERRANEAN SHIPPING CO 
        [TestMethod]
        public void TestKnownValidCompanyImoNumberReturnsSameResult(string imoNumber)
        {
            bool resultValidate = ImoNumberValidator.ValidateCompanyRegistrationNumber(imoNumber);
            bool resultIsValid = ImoNumberValidator.IsValidCompanyRegistrationNumber(imoNumber);

            Assert.AreEqual(resultValidate, resultIsValid);
        }

        [DataRow("IMO6342361")] //  invalid JUNGHAUS C MAERSK MOLLER 
        [DataRow("IMO5808452")] //  invalid MAERSK A/S 
        [DataRow("IMO0344773")] //  invalid EVERGREEN MARINE CORP 
        [DataRow("IMO0152946")] // invalid  MSC MEDITERRANEAN SHIPPING CO 
        [TestMethod]
        public void TestKnownInvalidCompanyImoNumberReturnsValid(string imoNumber)
        {
            bool result = ImoNumberValidator.IsValidCompanyRegistrationNumber(imoNumber);

            Assert.IsFalse(result);
        }




        [DataRow("IMO1234567")]
        [DataRow("IMO9375989")] // Nordic Sola
        [DataRow("IMO9346512")] // Nordic Saga
        [DataRow("IMO9378735")] // STEN BOTHNIA

        [TestMethod]
        public void TestKnownValidShipImoNumberReturnsValid(string imoNumber)
        {
            bool result = ImoNumberValidator.IsValidShipRegistrationNumber(imoNumber);

            Assert.IsTrue(result);
        }


        [DataRow("IMO1234567")]
        [DataRow("IMO9375989")]
        [DataRow("IMO1234567")]
        [DataRow("IMO1234567")]
        [DataRow("IMO1234567")]
        [DataRow("IMO1234567")]
        [DataRow("IMO1234567")]
        [DataRow("IMO1234567")]
        [DataRow("IMO1234567")]
        [DataRow("IMO1234567")]
        [TestMethod]
        public void TestKnownValidShip_ValidateAndIsValidMethodsReturnSameResult(string imoNumber)
        {
            bool resultValidate = ImoNumberValidator.ValidateShipRegistrationNumber(imoNumber);
            bool resultIsValid = ImoNumberValidator.IsValidShipRegistrationNumber(imoNumber);

            Assert.AreEqual(resultValidate, resultIsValid);
        }


        [DataRow("IMO1234561")]
        [DataRow("IMO1234562")]
        [DataRow("IMO1234563")]
        [DataRow("IMO1234564")]
        [DataRow("IMO1234565")]
        [DataRow("IMO1234566")]
        [DataRow("IMO1234568")]
        [DataRow("IMO1234569")]
        [TestMethod]
        public void TestKnownInvalidShipImoNumberReturnsInvalid(string imoNumber)
        {
            bool result = ImoNumberValidator.IsValidShipRegistrationNumber(imoNumber);
            Assert.IsFalse(result);
        }



        [DataRow("ABC1234561")]
        [DataRow("DEF1234561")]
        [DataRow("XYZ1234561")]
        [TestMethod]
        public void TestValidationAllowsCaseInsensitiveForShips(string imoNumber)
        {
            try
            {
                bool result = ImoNumberValidator.ValidateShipRegistrationNumber(imoNumber);
            }
            catch (ImoNumberValidationException ex)
            {
                StringAssert.Equals(ex.Message, "IMO number must start with 'IMO'");
                return;
            }
            Assert.Fail("Expected exception was not thrown");
        }


        [DataRow("ABC1234561")]
        [DataRow("DEF1234561")]
        [DataRow("XYZ1234561")]
        [TestMethod]
        public void TestValidationAllowsCaseInsensitiveForCompanies(string imoNumber)
        {
            try
            {
                bool result = ImoNumberValidator.ValidateCompanyRegistrationNumber(imoNumber);
            }
            catch (ImoNumberValidationException ex)
            {
                StringAssert.Equals(ex.Message, "IMO number must start with 'IMO'");
                return;
            }
            Assert.Fail("Expected exception was not thrown");
        }



        [DataRow("IMO1234567")]
        [DataRow("Imo1234567")]
        [DataRow("iMO1234567")]
        [DataRow("imO1234567")]
        [DataRow("imo1234567")]
        [DataRow("ImO1234567")]
        [TestMethod]
        public void TestStringMustStartWithImo(string imoNumber)
        {
            bool result = ImoNumberValidator.IsValidShipRegistrationNumber(imoNumber);
            Assert.IsTrue(result);
        }

        [DataRow("IMO12345610000")]
        [DataRow("IMO123456111111111")]
        [DataRow("IMO123456122222")]
        [DataRow("IMO1234")]
        [TestMethod]
        public void TestImoNumbersMustBe10CharactersLongForShips(string imoNumber)
        {
            try
            {
                bool result = ImoNumberValidator.ValidateShipRegistrationNumber(imoNumber);
            }
            catch (ImoNumberValidationException ex)
            {
                StringAssert.Contains(ex.Message, "IMO number must be 10 characters long. Received");
                return;
            }
            Assert.Fail("Expected exception was not thrown");
        }


        [DataRow("IMO12345610000")]
        [DataRow("IMO123456111111111")]
        [DataRow("IMO123456122222")]
        [DataRow("IMO1234")]
        [TestMethod]
        public void TestImoNumbersMustBe10CharactersLongForCompanies(string imoNumber)
        {
            try
            {
                bool result = ImoNumberValidator.ValidateCompanyRegistrationNumber(imoNumber);
            }
            catch (ImoNumberValidationException ex)
            {
                StringAssert.Contains(ex.Message, "IMO number must be 10 characters long. Received");
                return;
            }
            Assert.Fail("Expected exception was not thrown");
        }

    }
}