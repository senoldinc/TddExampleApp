using NUnit.Framework;
using JobApplicationLibrary.Model;
using JobApplicationLibrary.Model.Enum;
using System.Collections.Generic;
using Moq;
using JobApplicationLibrary.Services;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {
        [Test]
        public void Application_ShouldTransferredAutoRejected_WithUnderAge()
        {
            //arrange
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("TURKEY");

            var evaluater = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication(new Application { Age = 17 }, 0, new System.Collections.Generic.List<string> { "c#" }, "ISTANBUL");

            //action
            var result = evaluater.Evaulate(form);

            //assert
            Assert.AreEqual(ApplicationResult.AutoRejected, result);
        }

        [Test]
        public void Application_ShouldTransferredAutoRejected_WithNotMatchTechStack()
        {
            //arrange
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("TURKEY");

            var evaluater = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication(new Application { Age = 19 }, 0, new System.Collections.Generic.List<string> { "c#" }, "ISTANBUL");

            //action
            var result = evaluater.Evaulate(form);

            //assert
            Assert.AreEqual(ApplicationResult.AutoRejected, result);
        }

        [Test]
        public void Application_ShouldTransferredAutoAccepted_WithAllMatchTechStackAndExperienceYear()
        {
            //arrange
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("TURKEY");

            var evaluater = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication(
                    application: new Application { Age = 32 },
                    yearsOfExperience: 11, 
                    techStackList: new List<string> { "C#", "RabbitMQ", "Microservice", "Visual Studio"  },
                    officeLocation: "ISTANBUL"
                    );

            //action
            var result = evaluater.Evaulate(form);

            //assert
            Assert.AreEqual(ApplicationResult.AutoAccepted, result);
        }

        [Test]
        public void Application_ShouldTransferredHr_WithNoneValidIdentityNumber()
        {
            //arrange
             var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("TURKEY");

            var evaluater = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication(
                    application: new Application { Age = 32, IdentityNumber = "111" },
                    yearsOfExperience: 11, 
                    techStackList: new List<string> { "C#", "RabbitMQ", "Microservice", "Visual Studio"  },
                    officeLocation: "ISTANBUL"
                    );

            //action
            var result = evaluater.Evaulate(form);

            //assert
            Assert.AreEqual(ApplicationResult.TransferredToHr, result);
        }

        [Test]
        public void Application_ShouldTransferredCTO_WithDifferentOfficeLocation()
        {
            //arrange
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("TURKEY");

            var evaluater = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication(
                    application: new Application { Age = 32, IdentityNumber = "111" },
                    yearsOfExperience: 11, 
                    techStackList: new List<string> { "C#", "RabbitMQ", "Microservice", "Visual Studio"  },
                    officeLocation: "BURSA"
                    );

            //action
            var result = evaluater.Evaulate(form);

            //assert
            Assert.AreEqual(ApplicationResult.TransferredToCTO, result);
        }

         [Test]
        public void Application_ShouldTransferredLead_WithDifferentCountry()
        {
            //arrange
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("ITALY");
            
            var evaluater = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication(
                    application: new Application { Age = 32, IdentityNumber = "111" },
                    yearsOfExperience: 11, 
                    techStackList: new List<string> { "C#", "RabbitMQ", "Microservice", "Visual Studio"  },
                    officeLocation: "BURSA"
                    );

            //action
            var result = evaluater.Evaulate(form);

            //assert
            Assert.AreEqual(ApplicationResult.TransferredToLead, result);
        }


    }
}