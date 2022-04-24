namespace JobApplicationLibrary.Services
{
    public interface IIdentityValidator
    {
         bool IsValid(string identityNumber);

         ICountryDataProvider CountryDataProvider {get;}
    }

    public interface ICountryDataProvider
    {
        ICountryData CountryData { get;}
    }

    public interface ICountryData
    {
        string Country {get;}
    }

}