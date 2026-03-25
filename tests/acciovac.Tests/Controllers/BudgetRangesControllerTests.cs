using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using acciovac.API.Controllers;
using acciovac.Application.Behaviors.BudgetRanges.Commands.AddBudgetRange;
using acciovac.Application.Behaviors.BudgetRanges.Commands.UpdateBudgetRange;
using acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRangeById;
using acciovac.Application.Behaviors.BudgetRanges.Queries.GetBudgetRanges;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using acciovac.API.Common;

namespace acciovac.Tests.Controllers
{
    public class BudgetRangesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BudgetRangesController _controller;

        public BudgetRangesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BudgetRangesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Add_ReturnsCreated_WhenSuccessful()
        {
            // Arrange
            var request = new AddBudgetRangeRequest(1, 100, 500, true);
            var resultValue = 1;
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddBudgetRangeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<int>.Success(resultValue));

            // Act
            var response = await _controller.Add(request);

            // Assert
            var result = response.Should().BeOfType<CreatedAtActionResult>().Subject;
            result.ActionName.Should().Be(nameof(BudgetRangesController.GetById));
            result.RouteValues["id"].Should().Be(resultValue);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenFailed()
        {
            // Arrange
            var request = new AddBudgetRangeRequest(1, 100, 500, true);
            var error = "Validation failed";
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddBudgetRangeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<int>.Failure(error));

            // Act
            var response = await _controller.Add(request);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithData()
        {
            // Arrange
            var ranges = new List<BudgetRange>
            {
                new BudgetRange((byte)1, 100m, 500m, true) { Id = 1 }
            };
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetBudgetRangesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<IReadOnlyList<BudgetRange>>.Success(ranges));

            // Act
            var response = await _controller.GetAll();

            // Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenFound()
        {
            // Arrange
            var id = 1;
            var range = new BudgetRange((byte)1, 100m, 500m, true) { Id = id };
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetBudgetRangeByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<BudgetRange>.Success(range));

            // Act
            var response = await _controller.GetById(id);

            // Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenFailed()
        {
            // Arrange
            var id = 1;
            var error = "Not found";
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetBudgetRangeByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<BudgetRange>.Failure(error));

            // Act
            var response = await _controller.GetById(id);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            var request = new UpdateBudgetRangeRequest(1, 100, 500, true);
            var resultValue = 1;
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateBudgetRangeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<int>.Success(resultValue));

            // Act
            var response = await _controller.Update(id, request);

            // Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenBudgetRangeNotFound()
        {
            // Arrange
            var id = 1;
            var request = new UpdateBudgetRangeRequest(1, 100, 500, true);
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateBudgetRangeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<int>.Failure("Budget range not found"));

            // Act
            var response = await _controller.Update(id, request);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
