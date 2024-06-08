using Moq;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Options.Services;
using OnlineCarWash.Options.Services.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Tests.Options.Helpers;

namespace Tests.Options.UnitTests
{
    public class TestOptionQueryService
    {
        private readonly Mock<IRepositoryOption> _mock;
        private readonly IOptionQueryService _query;

        public TestOptionQueryService()
        {
            _mock = new Mock<IRepositoryOption>();
            _query = new OptionQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnData()
        {
            var option = TestOptionFactory.CreateOptions(5);
            _mock.Setup(repo=>repo.GetAllOption()).ReturnsAsync(option);

            var restul = await _query.GetAllOption();

            Assert.NotNull(restul);
            Assert.Equal(5,restul.Count);

        }

        [Fact]
        public async Task GetById_ReturnData()
        {
            var option = TestOptionFactory.CreateOption(1);
            _mock.Setup(repo => repo.GetByIdOption(1)).ReturnsAsync(option);

            var restul = await _query.GetByIdOption(1);

            Assert.NotNull(restul);
            Assert.Equal(1, restul.Id);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdOption(1)).ReturnsAsync((Option)null);

            var restul = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _query.GetByIdOption(1));

            Assert.NotNull(restul);
            Assert.Equal(Constants.ItemDoesNotExist, restul.Message);

        }

        [Fact]
        public async Task GetByName_ReturnData()
        {
            var option = TestOptionFactory.CreateOption(1);
            _mock.Setup(repo => repo.GetByNameOption("test1")).ReturnsAsync(option);

            var restul = await _query.GetByNameOption("test1");

            Assert.NotNull(restul);
            Assert.Equal(1, restul.Id);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameOption("test1")).ReturnsAsync((Option)null);

            var restul = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _query.GetByNameOption("test1"));
            
            Assert.NotNull(restul);
            Assert.Equal(Constants.ItemDoesNotExist, restul.Message);

        }
    }
}
