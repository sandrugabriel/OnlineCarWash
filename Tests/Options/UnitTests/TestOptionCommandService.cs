using Moq;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Options.Services.interfaces;
using OnlineCarWash.Options.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Options.Helpers;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.System.Exceptions;
using OnlineCarWash.System.Constatns;

namespace Tests.Options.UnitTests
{
    public class TestOptionCommandService
    {
        private readonly Mock<IRepositoryOption> _mock;
        private readonly IOptionCommandService _command;

        public TestOptionCommandService()
        {
            _mock = new Mock<IRepositoryOption>();
            _command = new OptionCommandService(_mock.Object);
        }

        [Fact]
        public async Task CreateOption_ReturnData()
        {
            var option = TestOptionFactory.CreateOption(1);
            var create = new CreateOptionRequest
            {
                Name = "test",
                Price = 10
            };
            option.Price = create.Price;
            option.Name = create.Name;
            _mock.Setup(repo=>repo.CreateOption(create)).ReturnsAsync(option);

            var result = await _command.CreateOption(create);

            Assert.NotNull(result);
            Assert.Equal("test",result.Name);
        }

        [Fact]
        public async Task CreateOption_InvalidName()
        {
            var create = new CreateOptionRequest
            {
                Name = "",
                Price = 10
            };
            _mock.Setup(repo => repo.CreateOption(create)).ReturnsAsync((Option)null);


            var result = await Assert.ThrowsAsync<InvalidName>(() => _command.CreateOption(create));

            Assert.NotNull(result);
            Assert.Equal(Constants.InvalidName, result.Message);
        }

        [Fact]
        public async Task CreateOption_InvalidPrice()
        {
            var create = new CreateOptionRequest
            {
                Name = "test",
                Price = 0
            };
            _mock.Setup(repo => repo.CreateOption(create)).ReturnsAsync((Option)null);


            var result = await Assert.ThrowsAsync<InvalidPrice>(() => _command.CreateOption(create));

            Assert.NotNull(result);
            Assert.Equal(Constants.InvalidPrice, result.Message);
        }

        [Fact]
        public async Task UpdateOption_ReturnData()
        {
            var option = TestOptionFactory.CreateOption(1);
            var update = new UpdateOptionRequest
            {
                Name = "test"
            };
            option.Name = update.Name;
            _mock.Setup(repo => repo.GetByIdOption(1)).ReturnsAsync(option);
            _mock.Setup(repo => repo.UpdateOption(1,update)).ReturnsAsync(option);

            var result = await _command.UpdateOption(1,update);

            Assert.NotNull(result);
            Assert.Equal("test", result.Name);
        }


        [Fact]
        public async Task UpdateOption_ItemDoNotExist()
        {
            var update = new UpdateOptionRequest
            {
                Name = "test"
            };
            _mock.Setup(repo => repo.UpdateOption(1, update)).ReturnsAsync((Option)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_command.UpdateOption(1,update));

            Assert.NotNull(result);
            Assert.Equal(Constants.ItemDoesNotExist, result.Message);
        }

        [Fact]
        public async Task DeleteOption_ReturnData()
        {
            var option = TestOptionFactory.CreateOption(1);
            _mock.Setup(repo => repo.GetByIdOption(1)).ReturnsAsync(option);
            _mock.Setup(repo => repo.DeleteOption(1)).ReturnsAsync(option);

            var result = await _command.DeleteOption(1);

            Assert.NotNull(result);
            Assert.Equal("test1", result.Name);
        }


        [Fact]
        public async Task DeleteOption_ItemDoNotExist()
        {
            _mock.Setup(repo => repo.DeleteOption(1)).ReturnsAsync((Option)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _command.DeleteOption(1));

            Assert.NotNull(result);
            Assert.Equal(Constants.ItemDoesNotExist, result.Message);
        }

    }
}
