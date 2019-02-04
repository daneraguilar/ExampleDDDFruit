using Application.Contracts;
using Application.Implements;
using Domain.Abstracts;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers
{
    //https://dzone.com/articles/7-popular-unit-test-naming
    [TestClass]
    public class CountryControllerTest
    {
        private Mock<ICountryRepository> _mockRepository;
        private ICountryService _service;
        Mock<IUnitOfWork> _mockUnitWork;
        List<Country> listCountry;
        CountryController controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ICountryRepository>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new CountryService(_mockUnitWork.Object, _mockRepository.Object);
            listCountry = new List<Country>() {
               new Country() { Id = 1, Name = "US" },
               new Country() { Id = 2, Name = "India" },
               new Country() { Id = 3, Name = "Russia" }
            };
            controller = new CountryController(_service);
        }

        [TestMethod]
        public void Given_Tres_Paises_When_HTTP_GET_Then_Retorna_Tres_Paises()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(listCountry);

            //Act
            List<Country> results = controller.Get() as List<Country>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }
        [TestMethod]
        public void Given_NuevoCountry_When_HTTP_POST_Then_Crea_el_nuevo_country()
        {
            //Arrange
            int Id = 1;
            Country emp = new Country() { Name = "UK" };
            _mockRepository.Setup(m => m.Add(emp)).Returns((Country e) =>
            {
                e.Id = Id;
                return e;
            });

            //Act
            _service.Create(emp);

            //Assert
            Assert.AreEqual(Id, emp.Id);
            _mockUnitWork.Verify(m => m.Commit(), Times.Once);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var ServiceBaseURL = "http://localhost:65413/";
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(listCountry);
            //
            controller = new CountryController(_service)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "api/Country")
               }
            };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            
            // Act
            var response = controller.Get().ExecuteAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsCompleted);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);

            // Assertions on returned data
            List<Country> models;
            Assert.IsTrue(response.Result.TryGetContentValue<List<Country>>(out models));
            Assert.AreEqual(3, models.Count);

            Assert.AreEqual(1, models.First().Id);
        }
    }
}
