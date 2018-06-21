namespace RedBlueGames.LiteFSM.Tests
{
    using NUnit.Framework;
    using RedBlueGames.LiteFSM;

    public class StateMachineTests
    {
        [Test]
        public void CtorNoReflection_OneState_EntersOnlyInitialState()
        {
            // Arrange
            var stubStates = new StubState<TwoStatesID>[]
            {
                new StubState<TwoStatesID>(TwoStatesID.One),
                new StubState<TwoStatesID>(TwoStatesID.Two),
            };

            // Act
            new StateMachine<TwoStatesID>(stubStates, TwoStatesID.One);

            // Assert
            Assert.That(stubStates[0].EnterCalled, Is.True);
            Assert.That(stubStates[1].EnterCalled, Is.False);
        }

        [Test]
        public void CtorNoReflection_NotEnoughStates_Throws()
        {
            // Arrange
            var stubStates = new StubState<TwoStatesID>[]
            {
                new StubState<TwoStatesID>(TwoStatesID.One)
            };

            var expectedMessage = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Not enough states passed in. Missing states: Two");

            // Act / Assert
            var exception = Assert.Throws<System.ArgumentException>(
                () => new StateMachine<TwoStatesID>(stubStates, TwoStatesID.One));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void CtorNoReflection_DuplicateStates_Throws()
        {
            // Arrange
            var stubStates = new StubState<TwoStatesID>[]
            {
                new StubState<TwoStatesID>(TwoStatesID.One),
                new StubState<TwoStatesID>(TwoStatesID.One),
                new StubState<TwoStatesID>(TwoStatesID.Two),
            };

            var expectedMessage = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Duplicate states passed in. Duplicate states: One");

            // Act / Assert
            var exception = Assert.Throws<System.ArgumentException>(
                () => new StateMachine<TwoStatesID>(stubStates, TwoStatesID.One));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }
    }
}