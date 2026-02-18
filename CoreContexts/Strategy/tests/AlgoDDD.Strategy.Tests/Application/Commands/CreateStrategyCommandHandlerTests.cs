using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Application.Commands;
using AlgoDDD.Strategy.Domain.Aggregates;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;
using Moq;
using Xunit;

namespace AlgoDDD.Strategy.Tests.Application.Commands
{
    public class CreateStrategyCommandHandlerTests
    {
        private readonly Mock<IStrategyRepository> _repositoryMock;
        private readonly CreateStrategyCommandHandler _handler;

        public CreateStrategyCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStrategyRepository>();
            _handler = new CreateStrategyCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateAndPersistStrategyEntity()
        {
            // Arrange
            var command = new CreateStrategyCommand
            {
                Name = "Momentum Strategy",
                Description = "Tests upward momentum",
                Type = StrategyType.Momentum,
                Parameters = new Dictionary<string, object> { { "threshold", 0.02m } },
                Symbols = new List<string> { "AAPL", "MSFT" },
                TimeFrame = TimeFrame.Daily,
                MaxPositionSize = 10000m,
                StopLoss = 0.05m,
                TakeProfit = 0.10m
            };

            var expectedEntity = new StrategyEntity(
                Guid.NewGuid(),
                command.Name,
                command.Type,
                command.Description,
                command.Parameters,
                command.Symbols,
                command.TimeFrame,
                command.MaxPositionSize,
                command.StopLoss,
                command.TakeProfit
            );

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<StrategyEntity>()))
                .ReturnsAsync(expectedEntity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Description, result.Description);
            Assert.Equal(command.Type, result.Type);
            Assert.Equal(command.Symbols, result.Symbols);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<StrategyEntity>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var command = new CreateStrategyCommand
            {
                Name = "Broken Strategy",
                Description = "Repository failure simulation",
                Type = StrategyType.MeanReversion,
                Symbols = new List<string> { "GOOGL" },
                TimeFrame = TimeFrame.Hourly
            };

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<StrategyEntity>()))
                .ThrowsAsync(new InvalidOperationException("Repository error"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _handler.Handle(command, CancellationToken.None)
            );
        }
    }
}
