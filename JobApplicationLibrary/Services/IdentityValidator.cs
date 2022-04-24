namespace JobApplicationLibrary.Services
{
    public class IdentityValidator : IIdentityValidator
    {
        public ICountryDataProvider CountryDataProvider => throw new NotImplementedException();

        public bool IsValid(string identityNumber)
        {
            if(string.IsNullOrEmpty(identityNumber) || identityNumber.Length != 11 )
                return false;

            return true;
        }
    }
}