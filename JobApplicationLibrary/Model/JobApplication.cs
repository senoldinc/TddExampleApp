namespace JobApplicationLibrary.Model
{
    public class JobApplication
    {
        public JobApplication(Application application, int yearsOfExperience, List<string> techStackList, string officeLocation)
        {
            this.Application = application;
            this.YearsOfExperience = yearsOfExperience;
            this.TechStackList = techStackList;
            this.OfficeLocation = officeLocation;
        }
        
        public Application Application { get; set; }
        public int YearsOfExperience { get; set; }
        public string OfficeLocation { get; set; }
        public List<string> TechStackList { get; set; }
    }
}