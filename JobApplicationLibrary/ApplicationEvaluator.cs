using JobApplicationLibrary.Model;
using JobApplicationLibrary.Model.Enum;
using JobApplicationLibrary.Services;

namespace JobApplicationLibrary;
public class ApplicationEvaluator
{
    private int _minAge = 18;
    private int autoAcceptedYearExperience = 10;
    private List<string> techStackList = new List<string> { "C#", "RabbitMQ", "Microservice", "Visual Studio" };
    private IIdentityValidator identityValidator;
    public ApplicationEvaluator(IIdentityValidator identityValidator)
    {
        this.identityValidator = identityValidator;
    }
    public ApplicationResult Evaulate(JobApplication form)
    {
         var validIdentiy = identityValidator.IsValid(form.Application.IdentityNumber);

        if (!validIdentiy)
            return ApplicationResult.TransferredToHr;

        if(identityValidator.CountryDataProvider.CountryData.Country != "TURKEY")
            return ApplicationResult.TransferredToLead;

        if (form.Application.Age < _minAge)
            return ApplicationResult.AutoRejected;

        if(form.OfficeLocation != "ISTANBUL")
            return ApplicationResult.TransferredToCTO;
       
        var sr = GetTechSmilarityRate(form.TechStackList);
        if (sr <= 25)
            return ApplicationResult.AutoRejected;

        if (sr >= 75 && form.YearsOfExperience > autoAcceptedYearExperience)
            return ApplicationResult.AutoAccepted;

        return ApplicationResult.AutoAccepted;
    }

    public int GetTechSmilarityRate(List<string> techStacks)
    {
        var matchCount = techStacks.Where(i => techStackList.Contains(i, StringComparer.OrdinalIgnoreCase)).Count();
        return (int)(((double)matchCount / techStackList.Count) * 100);
    }
}
