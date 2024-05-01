namespace EtiveMor.OpenImoRegistrationNumberValidator
{
    public class ImoNumberValidationException : Exception
    {
        public ImoNumberValidationException()
        {
        }

        public ImoNumberValidationException(string message)
            : base(message)
        {
        }

        public ImoNumberValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
