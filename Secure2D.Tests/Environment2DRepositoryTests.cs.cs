using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Secure2D.Models;
using Secure2D.Repositories;

namespace Secure2D.Tests
{
    [TestClass]
    public class Environment2DRepositoryTests
    {
        private Mock<IEnvironment2DRepository> _mockRepo;
        private List<Environment2D> _mockData;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IEnvironment2DRepository>();
            _mockData = new List<Environment2D>
            {
                new Environment2D { Id = Guid.NewGuid(), Name = "TestEnv1", MaxHeight = 100, MaxLength = 200 },
                new Environment2D { Id = Guid.NewGuid(), Name = "TestEnv2", MaxHeight = 150, MaxLength = 250 }
            };
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnListOfEnvironments()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_mockData);

            // Act
            var result = await _mockRepo.Object.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectEnvironment()
        {
            // Arrange
            var testId = _mockData[0].Id;
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(_mockData[0]);

            // Act
            var result = await _mockRepo.Object.GetByIdAsync(testId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testId, result.Id);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueWhenEnvironmentIsDeleted()
        {
            // Arrange
            var testId = _mockData[0].Id;
            _mockRepo.Setup(repo => repo.DeleteAsync(testId)).ReturnsAsync(true);

            // Act
            var result = await _mockRepo.Object.DeleteAsync(testId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
