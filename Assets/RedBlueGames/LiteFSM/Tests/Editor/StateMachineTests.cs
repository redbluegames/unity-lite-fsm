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
            Assert.IsTrue(stubStates[0].EnterCalled);
            Assert.IsFalse(stubStates[1].EnterCalled);
        }
    }
}