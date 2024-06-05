using Moq;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Customers.Services;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Customers.Helpers;

namespace Tests.Customers.UnitTests
{
    public class TestCustomerQueryService
    {

        private readonly Mock<IRepositoryCustomer> _mock;
        private readonly IQueryServiceCustomer _queryServiceCustomer;

        public TestCustomerQueryService()
        {
            _mock = new Mock<IRepositoryCustomer>();
            _queryServiceCustomer = new QueryServiceCustomer(_mock.Object);
        }

        [Fact]
        public async Task GetAllCustomer_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync( new List<CustomerResponse>());

            var result = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _queryServiceCustomer.GetAllAsync());

            Assert.Equal(Constants.ItemsDoNotExist,result.Message);

        }

        [Fact]
        public async Task GetAllCustomer_ReturnCustomer()
        {
            var customers = TestCustomerFactory.CreateCustomers(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            var result = await _queryServiceCustomer.GetAllAsync();

            Assert.Equal(5,result.Count);

        }

        [Fact]
        public async Task GetByIdCustomer_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((CustomerResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryServiceCustomer.GetByIdAsync(1));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByIdCustomer_ReturnCustomer()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            var result = await _queryServiceCustomer.GetByIdAsync(1);

            Assert.Equal("test1",result.Name);

        }


        [Fact]
        public async Task GetByNameCustomer_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync((CustomerResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryServiceCustomer.GetByNameAsync("test"));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByNameCustomer_ReturnCustomer()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);
            _mock.Setup(repo => repo.GetByNameAsync("test1")).ReturnsAsync(customer);

            var result = await _queryServiceCustomer.GetByNameAsync("test1");

            Assert.Equal("test1", result.Name);

        }
    }
}
