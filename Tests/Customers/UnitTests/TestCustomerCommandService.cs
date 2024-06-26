﻿using Moq;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.Customers.Services;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Customers.Helpers;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.Appointments.Repository.interfaces;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Services.Repository.interfaces;

namespace Tests.Customers.UnitTests
{
    public class TestCustomerCommandService
    {
        private readonly Mock<IRepositoryCustomer> _mock;
        private readonly Mock<IRepositoryAppointment> _mockApp;
        private readonly Mock<IRepositoryOption> _mockOption;
        private readonly Mock<IRepositoryService> _mockService;

        private readonly ICommandServiceCustomer _commandService;

        public TestCustomerCommandService()
        {
            _mock = new Mock<IRepositoryCustomer>();
            _mockApp = new Mock<IRepositoryAppointment>();
            _mockOption = new Mock<IRepositoryOption>();
            _mockService = new Mock<IRepositoryService>();
            _commandService = new CommandServiceCustomer(_mock.Object,_mockService.Object,_mockApp.Object,_mockOption.Object);

        }

        [Fact]
        public async Task CreateCustomer_InvalidName()
        {
            var createRequest = new CreateCustomerRequest
            {
                Name = "",
                Password = "1234",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            _mock.Setup(repo => repo.CreateCustomer(createRequest)).ReturnsAsync((CustomerResponse)null);
            Exception exception = await Assert.ThrowsAsync<InvalidName>(() => _commandService.CreateCustomer(createRequest));

            Assert.Equal(Constants.InvalidName, exception.Message);
        }

        [Fact]
        public async Task CreateCustomer_ReturnCustomer()
        {
            var createRequest = new CreateCustomerRequest
            {
                Name = "test50",
                Password = "1234",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(50);

            _mock.Setup(repo => repo.CreateCustomer(It.IsAny<CreateCustomerRequest>())).ReturnsAsync(customer);

            var result = await _commandService.CreateCustomer(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Name, createRequest.Name);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Name = "Test",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((CustomerResponse)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.UpdateCustomer(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidName()
        {
            var updateRequest = new UpdateCustomerRequest
            {

                Name = "",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(1);
            customer.Name = updateRequest.Name;
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            var exception = await Assert.ThrowsAsync<InvalidName>(() => _commandService.UpdateCustomer(1, updateRequest));

            Assert.Equal(Constants.InvalidName, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData_ReturnCustomer()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Name = "Test",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mock.Setup(repo => repo.UpdateCustomer(It.IsAny<int>(), It.IsAny<UpdateCustomerRequest>())).ReturnsAsync(customer);

            var result = await _commandService.UpdateCustomer(1, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(customer, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).ReturnsAsync((CustomerResponse)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.DeleteCustomer(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var restul = await _commandService.DeleteCustomer(1);

            Assert.NotNull(restul);
            Assert.Equal(customer, restul);
        }


    }
}
