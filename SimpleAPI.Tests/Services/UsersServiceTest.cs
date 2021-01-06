using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleAPI.DTOs;
using SimpleAPI.Helpers;
using SimpleAPI.Models;
using SimpleAPI.Services.Classes;
using SimpleAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleAPI.Tests.Services
{
    public class UsersServiceTest
    {
        private MockRepository mockRepository;
        private Mock<IUserRepository> _usersRepositoryMock;
        private UsersService usersService;

        public UsersServiceTest()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);

            _usersRepositoryMock = mockRepository.Create<IUserRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var loggerMoq = Mock.Of<ILogger<UsersService>>();

            usersService = new UsersService(_usersRepositoryMock.Object, loggerMoq, mapper);
        }

        private List<User> GetTestUsers()
        {
            var result = new List<User>();

            result.Add(new User { Id = 1, Name = "John", LastName = "Miller", Email = "email@gmai.com", Phone = "+370620171" });
            result.Add(new User { Id = 2, Name = "Randy", LastName = "Orton", Email = "randy@gmai.com", Phone = "+370615517" });

            return result;
        }

        [Fact]
        public async Task GetAll_RetunsAllList()
        {

            //Arrange
            _usersRepositoryMock.Setup(service => service.GetAllAsync()).ReturnsAsync(GetTestUsers());

            //Act
            var result = await usersService.GetAllAsync();

            //Assert
            var bookingResult = Assert.IsType<List<GetUserDTO>>(result.Data);
            Assert.True(result.Success);
            Assert.Equal(2, bookingResult.Count);
        }


        [Fact]
        public async Task GetAll_ReturnsErrorMessageIfServerErrror()
        {

            //Arrange
            _usersRepositoryMock.Setup(service => service.GetAllAsync()).ThrowsAsync(new Exception("Unexpected error while retrieving data"));

            //Act
            var result = await usersService.GetAllAsync();

            //Assert
            Assert.False(result.Success);
            Assert.Equal("Unexpected error while retrieving data", result.ErrorMessage);
        }
    }
}
