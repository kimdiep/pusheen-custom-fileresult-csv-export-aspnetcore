using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using PusheenCustomExportCsv.Web.Data;
using PusheenCustomExportCsv.Web.Controllers;
using PusheenCustomExportCsv.Web.Models;
using PusheenCustomExportCsv.Web.Services;

namespace PusheenCustomExportCsv.Tests.Controllers
{
    [TestFixture]
    public class PusheenControllerShould
    {
        private PusheenController _controller;
        private Mock<IPusheenService> _mockPusheenService;
        private Mock<IConfiguration> _mockConfig;
        private DbContextOptions<PusheenCustomExportCsvContext> _testDbOptions;
        private PusheenCustomExportCsvContext _testDbContext;
        
        [SetUp]
        public void Setup()
        {
            _mockPusheenService = new Mock<IPusheenService>();
            _controller = new PusheenController(_mockPusheenService.Object);
        }
        
        [Test]
        public void ExportCsv_Returns_CsvResult()
        {
            //Arrange
            var data = new List<Pusheen>()
            {
                new Pusheen() { Id = 1, Name = "Pusheen", FavouriteFood = "Ice cream", SuperPower = "Baking delicious cookies" },
                new Pusheen() { Id = 2, Name = "Pusheenosaurus", FavouriteFood = "Leaves", SuperPower = "Roarrrrr!" },
                new Pusheen() { Id = 3, Name = "Pusheenicorn", FavouriteFood = "Butterfly muffins", SuperPower = "Making rainbow poop" }
                
            }.AsQueryable();

            _mockPusheenService.Setup(p => p.GetAllPusheens()).Returns(data);

            //Act
            var result = _controller.ExportCsv();

            //Assert
            result.Should().BeOfType(typeof(PusheenCsvResult));
            result.FileDownloadName.Should().Be("pusheen.csv");
            result.ContentType.Should().Be("text/csv");

        }

        
    }
}
