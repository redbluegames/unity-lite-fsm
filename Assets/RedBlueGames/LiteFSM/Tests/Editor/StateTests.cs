namespace RedBlueGames.LiteFSM.Tests
{
    using NUnit.Framework;

    public class StateTests
    {
        private enum StateID
        {
            SingleState,
        }

        [Test]
        public void Enter_ValidState_InvokesCallback()
        {
            // Arrange
            var enterCalled = false;
            State<StateID>.EnterMethod enterMethod = () => enterCalled = true;
            var state = new State<StateID>(StateID.SingleState, enterMethod, null, null);

            // Act
            state.Enter();

            // Assert
            Assert.IsTrue(enterCalled);
        }

        [Test]
        public void Enter_NullEnterMethod_DoesNotThrow()
        {
            // Arrange
            var state = new State<StateID>(StateID.SingleState, null, null, null);

            // Assert
            Assert.DoesNotThrow(() => state.Enter());
        }

        [Test]
        public void Exit_ValidState_InvokesCallback()
        {
            // Arrange
            var exitCalled = false;
            State<StateID>.ExitMethod exitMethod = () => exitCalled = true;
            var state = new State<StateID>(StateID.SingleState, null, exitMethod, null);

            // Act
            state.Exit();

            // Assert
            Assert.IsTrue(exitCalled);
        }

        [Test]
        public void Exit_NullExitMethod_DoesNotThrow()
        {
            // Arrange
            var state = new State<StateID>(StateID.SingleState, null, null, null);

            // Assert
            Assert.DoesNotThrow(() => state.Exit());
        }

        [Test]
        public void Update_ValidState_InvokesCallback()
        {
            // Arrange
            var updateCalled = false;
            State<StateID>.UpdateMethod updateMethod = (dT) => updateCalled = true;
            var state = new State<StateID>(StateID.SingleState, null, null, updateMethod);

            // Act
            state.Update(0.0f);

            // Assert
            Assert.IsTrue(updateCalled);
        }

        [Test]
        public void Update_ValidDeltaTime_PaasesValidTime()
        {
            // Arrange
            var expectedDeltaTime = 1.1f;
            var reportedTime = 0.0f;
            State<StateID>.UpdateMethod updateMethod = (dT) => reportedTime = dT;
            var state = new State<StateID>(StateID.SingleState, null, null, updateMethod);

            // Act
            state.Update(1.1f);

            // Assert
            Assert.AreEqual(expectedDeltaTime, reportedTime);
        }

        [Test]
        public void Update_NullUpdateMethod_DoesNotThrow()
        {
            // Arrange
            var state = new State<StateID>(StateID.SingleState, null, null, null);

            // Assert
            Assert.DoesNotThrow(() => state.Update(0.0f));
        }
    }
}