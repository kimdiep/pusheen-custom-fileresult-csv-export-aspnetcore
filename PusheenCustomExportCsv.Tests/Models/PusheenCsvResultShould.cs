using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PusheenCustomExportCsv.Web.Models;

namespace PusheenCustomExportCsv.Tests.Models
{
    [TestFixture]
    public class PusheenCsvResultShould
    {
        private PusheenCsvResult _pusheenCsvResult;
        private string _fileDownloadName;
        private string _expectedResponseText;
        private DefaultHttpContext _httpContext;
        private ActionContext _fakeActionContext;

        [SetUp]
        public void Setup()
        {
            _httpContext = new DefaultHttpContext();

            _fileDownloadName = "pusheen.csv";

            _fakeActionContext = new ActionContext()
            {
                HttpContext = _httpContext
            };
        }
        
        [Test]
        public async Task GivenActionContext_ExecuteResultAsync_ShouldWriteLineToHttpResponseBody()
        {
            
            //Arrange
            var data = new List<Pusheen>()
            {
                new Pusheen() { Id = 1, Name = "Pusheen", FavouriteFood = "Ice cream", SuperPower = "Baking delicious cookies" },
                new Pusheen() { Id = 2, Name = "Pusheenosaurus", FavouriteFood = "Leaves", SuperPower = "Roarrrrr!" },
                new Pusheen() { Id = 3, Name = "Pusheenicorn", FavouriteFood = "Butterfly muffins", SuperPower = "Making rainbow poop" }
                
            }.AsQueryable();

            PusheenCsvResult _pusheenCsvResult = new PusheenCsvResult(data, _fileDownloadName);

            _expectedResponseText = System.IO.File.ReadAllText(TestContext.CurrentContext.TestDirectory + @"/TestData/expectedCsv.txt");

            var memoryStream = new MemoryStream();
            _httpContext.Response.Body = memoryStream;

            //Act
            await _pusheenCsvResult.ExecuteResultAsync(_fakeActionContext);
            var streamText = System.Text.Encoding.Default.GetString(memoryStream.ToArray());

            //Assert
            streamText.Should().Be(_expectedResponseText);
        }

    }
}