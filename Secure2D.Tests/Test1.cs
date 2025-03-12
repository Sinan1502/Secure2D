using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Secure2D.Controllers;
using Secure2D.Models;
using Secure2D.Repositories;

namespace Secure2D.Tests
{
    [TestClass]
    public class Environment2DControllerTests
    {
        private Mock<IEnvironment2DRepository> _mockRepo;
        private Environment2DController _controller;
        private List<Environment2D> _mockData;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IEnvironment2DRepository>();
            _controller = new Environment2DController(_mockRepo.Object);
            _mockData = new List<Environment2D>
            {
                new Environment2D { Id = Guid.NewGuid(), Name = "TestEnv1", MaxHeight = 100, MaxLength = 200 },
                new Environment2D { Id = Guid.NewGuid(), Name = "TestEnv2", MaxHeight = 150, MaxLength = 250 }
            };
        }

        [TestMethod]
        public async Task Post_AddNewEnvironment_ReturnsCreatedEnvironment()
        {
            // Arrange
            var newEnvironment = new Environment2D { Id = Guid.NewGuid(), Name = "NewEnv", MaxHeight = 100, MaxLength = 200 };
            _mockRepo.Setup(repo => repo.AddAsync(newEnvironment)).Returns(Task.CompletedTask);

            // Act
            var response = await _controller.Post(newEnvironment);

            // Assert
            Assert.IsInstanceOfType(response, typeof(CreatedAtActionResult));
            var createdResult = response as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.IsInstanceOfType(createdResult.Value, typeof(Environment2D));
            var actualEnvironment = createdResult.Value as Environment2D;
            Assert.IsNotNull(actualEnvironment);
            Assert.AreEqual(newEnvironment.Id, actualEnvironment.Id);
        }
    }
}
