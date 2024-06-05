using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineCarWash.Customers.Controllers.intefaces;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Options.Controllers;
using OnlineCarWash.Options.Controllers.interfaces;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Options.Services.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Customers.Helpers;
using Tests.Options.Helpers;

namespace Tests.Options.UnitTests
{
    public class TestOptionController
    {

        private readonly Mock<IOptionQueryService> _optionQueryService;
        private readonly Mock<IOptionCommandService> _optionCommand;
        private readonly ControllerAPIOption _controller;

        public TestOptionController()
        {
            _optionQueryService = new Mock<IOptionQueryService>();
            _optionCommand = new Mock<IOptionCommandService>();
            _controller = new ControllerOption(_optionCommand.Object, _optionQueryService.Object);
        }

        [Fact]
        public async Task GetAllOptin_ReturnData()
        {
            var optins = TestOptionFactory.CreateOptions(5);
            _optionQueryService.Setup(repo => repo.GetAllOption()).ReturnsAsync(optins);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var alloptins = Assert.IsType<List<Option>>(okResult.Value);

            Assert.Equal(5, alloptins.Count);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task GetByIdOptin_ReturnData()
        {
            var optins = TestOptionFactory.CreateOption(1);
            _optionQueryService.Setup(repo => repo.GetByIdOption(1)).ReturnsAsync(optins);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var alloptins = Assert.IsType<Option>(okResult.Value);

            Assert.Equal(1, alloptins.Id);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task GetByIdOptin_ItemDoesNotExist()
        {
            _optionQueryService.Setup(repo => repo.GetByIdOption(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await _controller.GetById(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(Constants.ItemDoesNotExist,notFound.Value);
            Assert.Equal(404, notFound.StatusCode);

        }

        [Fact]
        public async Task UpdateOptin_ReturnData()
        {
            var optins = TestOptionFactory.CreateOption(1);
            _optionQueryService.Setup(repo => repo.GetByIdOption(1)).ReturnsAsync(optins);

            var update = new UpdateOptionRequest
            {
                Price = 10
            };
            optins.Price = update.Price.Value;
            _optionCommand.Setup(repo=>repo.UpdateOption(1,update)).ReturnsAsync(optins);
            var result = await _controller.Update(1,update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var alloptins = Assert.IsType<Option>(okResult.Value);

            Assert.Equal(1, alloptins.Id);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task UpdateOptin_ItemDoesNotExist()
        {
            _optionCommand.Setup(repo => repo.UpdateOption(1,It.IsAny<UpdateOptionRequest>())).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await _controller.Update(1,It.IsAny<UpdateOptionRequest>());

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFound.Value);
            Assert.Equal(404, notFound.StatusCode);

        }

        [Fact]
        public async Task DeleteOptin_ReturnData()
        {
            var optins = TestOptionFactory.CreateOption(1);
            _optionQueryService.Setup(repo => repo.GetByIdOption(1)).ReturnsAsync(optins);

            _optionCommand.Setup(repo => repo.DeleteOption(1)).ReturnsAsync(optins);
            var result = await _controller.Delete(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var alloptins = Assert.IsType<Option>(okResult.Value);

            Assert.Equal(1, alloptins.Id);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task DeleteOptin_ItemDoesNotExist()
        {
            _optionCommand.Setup(repo => repo.DeleteOption(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await _controller.Delete(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFound.Value);
            Assert.Equal(404, notFound.StatusCode);

        }

    }
}
