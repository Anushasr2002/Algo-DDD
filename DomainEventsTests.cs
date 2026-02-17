using System;
using Xunit;
using AlgoDDD.SharedKernel;

namespace AlgoDDD.SharedKernel.Tests
{
    public class DomainEventsTests
    {
        [Fact]
        public void Raise_Should_Invoke_Registered_Handler()
        {
            // Arrange
            var wasCalled = false;
            DomainEvents.ClearHandlers();
            DomainEvents.Register<TestEvent>(e => wasCalled = true);

            // Act
            DomainEvents.Raise(new TestEvent("UnitTest"));

            // Assert
            Assert.True(wasCalled);
        }

        [Fact]
        public void Raise_Should_Not_Throw_When_No_Handler_Registered()
        {
            // Arrange
            DomainEvents.ClearHandlers();

            // Act & Assert
            var ex = Record.Exception(() => DomainEvents.Raise(new TestEvent("NoHandler")));
            Assert.Null(ex); // No exception expected
        }

        [Fact]
        public void ClearHandlers_Should_Remove_All_Handlers()
        {
            // Arrange
            var wasCalled = false;
            DomainEvents.Register<TestEvent>(e => wasCalled = true);
            DomainEvents.ClearHandlers();

            // Act
            DomainEvents.Raise(new TestEvent("AfterClear"));

            // Assert
            Assert.False(wasCalled);
        }

        // Sample event for testing
        private class TestEvent : DomainEvent
        {
            public string Name { get; }
            public TestEvent(string name) => Name = name;
        }
    }
}
