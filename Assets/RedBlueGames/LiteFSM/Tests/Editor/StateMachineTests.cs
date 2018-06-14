namespace RedBlueGames.LiteFSM.Tests
{
    using UnityEngine;
    using UnityEngine.TestTools;
    using NUnit.Framework;
    using System.Collections;
    using RedBlueGames.LiteFSM;

    public class StateMachineTests
    {
        [Test]
        public void Ctor_NoReflection_EntersInitialState()
        {
            // Arrange
            var stubStates = new StubState<SingleStateID>[]
            {
                new StubState<SingleStateID>(SingleStateID.Init)
            };

            // Act
            new StateMachine<SingleStateID>(stubStates, SingleStateID.Init);

            // Assert
            Assert.IsTrue(stubStates[0].EnterCalled);
        }
    }
}