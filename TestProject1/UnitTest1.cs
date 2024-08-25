using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApplication1.Pages;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void OnPost_WithInvalidData_1()
        {
            // Arrange
            var pageModel = new IndexModelTest
            {
                Z= 100,
                Y1 = 0.1,
                Y2 = 0.2,
                HOURS = 2,
                number_of_ads = 4,
                missing_ad = 1,
                uploaded_file = null,
            };

            // Act
            var result = pageModel.OnPost();

            // Assert
            Assert.AreEqual(pageModel.error_message, "Please upload the budget for the rest of the ads!");

        }

        [Test]
        public void OnPost_WithInvalidData_2()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("AdNumber,Budget\r\nX1,0.2\r\nX2,0.3\r\nX3,-1\r\nX4,0.05")), 0, 49, "Data", "dummy.csv");

            // Arrange
            var pageModel = new IndexModelTest
            {
                Z = 100,
                Y1 = 0.1,
                Y2 = 0.2,
                HOURS = 2,
                number_of_ads = 4,
                missing_ad = 1,
                uploaded_file = file,
            };


            // Act
            var result = pageModel.OnPost();

            // Assert
            Assert.AreEqual(pageModel.error_message, "The missing ad No. should assigned -1 in the file!");

        }

        [Test]
        public void OnPost_WithInvalidData_3()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("AdNumber,Budget\r\nX1,0.2\r\nX2,0.3\r\nX3,-1\r\nX4,0.05")), 0, 49, "Data", "dummy.csv");

            // Arrange
            var pageModel = new IndexModelTest
            {
                Z = 100,
                Y1 = 0.1,
                Y2 = 0.2,
                HOURS = 2,
                number_of_ads = 4,
                missing_ad = 5,
                uploaded_file = file,
            };


            // Act
            var result = pageModel.OnPost();

            // Assert
            Assert.AreEqual(pageModel.error_message, "The number of ads must be greater or equal to the missing ad No.");

        }


        [Test]
        public void OnPost_1()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("AdNumber,Budget\r\nX1,0.2\r\nX2,0.3\r\nX3,-1\r\nX4,0.05")), 0, 49, "Data", "dummy.csv");
            
            // Arrange
            var pageModel = new IndexModelTest
            {
                Z = 100,
                Y1 = 0.1,
                Y2 = 0.2,
                HOURS = 2,
                number_of_ads = 4,
                missing_ad = 3,
                uploaded_file = file,
            };


            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(result.RouteValues["result"], 0.97);
        }

        [Test]
        public void OnPost_2()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("AdNumber,Budget\r\nX1,0.2\r\nX2,0.3\r\nX3,-1\r\nX4,0.05")), 0, 49, "Data", "dummy.csv");

            // Arrange
            var pageModel = new IndexModelTest
            {
                Z = 100,
                Y1 = 0.1,
                Y2 = 0.2,
                HOURS = 80,
                number_of_ads = 4,
                missing_ad = 3,
                uploaded_file = file,
            };


            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(result.RouteValues["result"], 0.20);
        }
    }
}