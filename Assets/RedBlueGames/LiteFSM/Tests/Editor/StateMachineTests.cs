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
            var log = new StateMachineDiagonosticsLog<TwoStatesID>();
            var stubStates = new StubStateWithDiagnostics<TwoStatesID>[]
            {
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.One, log),
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.Two, log),
            };

            var expectedLog = new StateMachineDiagonosticsLog<TwoStatesID>();
            expectedLog.AddEntry(StateMachineDiagonosticsLog<TwoStatesID>.LogEntry.Callback.Enter, TwoStatesID.One);

            // Act
            new StateMachine<TwoStatesID>(stubStates, TwoStatesID.One);

            // Assert
            Assert.That(log, Is.EqualTo(expectedLog));
        }

        [Test]
        public void CtorNoReflection_NotEnoughStates_Throws()
        {
            // Arrange
            var log = new StateMachineDiagonosticsLog<TwoStatesID>();
            var stubStates = new StubStateWithDiagnostics<TwoStatesID>[]
            {
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.One, log)
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
        public void CtorNoReflection_TooManyStates_Throws()
        {
            // Arrange
            var log = new StateMachineDiagonosticsLog<TwoStatesID>();
            var stubStates = new StubStateWithDiagnostics<TwoStatesID>[]
            {
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.One, log),
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.One, log),
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.Two, log),
            };

            var expectedMessage = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Duplicate states passed in. Duplicate states: One");

            // Act / Assert
            var exception = Assert.Throws<System.ArgumentException>(
                () => new StateMachine<TwoStatesID>(stubStates, TwoStatesID.One));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void CtorNoReflection_CorrectNumberOfStatesButDuplicates_Throws()
        {
            // Arrange
            var log = new StateMachineDiagonosticsLog<TwoStatesID>();
            var stubStates = new StubStateWithDiagnostics<TwoStatesID>[]
            {
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.One, log),
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.One, log),
            };

            // Both "DuplicateStates" and "NotEnoughStates" would be valid errors
            var notEnoughMessage = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Not enough states passed in. Missing states: Two");

            var duplicatesMessage = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Duplicate states passed in. Duplicate states: One");

            // Act / Assert
            var exception = Assert.Throws<System.ArgumentException>(
                () => new StateMachine<TwoStatesID>(stubStates, TwoStatesID.One));
            Assert.That(exception.Message,
                        Is.EqualTo(notEnoughMessage).Or.EqualTo(duplicatesMessage));
        }

        [Test]
        public void CtorNoReflection_NotAnEnum_Throws()
        {
            // Arrange
            var stubStates = new StubStateWithInvalidEnum[] { };

            var expectedMessage = string.Concat(
                    "StateMachine trying to initialize with an invalid generic type. " +
                    "Generic type (T) is not an Enum. Type: NotAnEnum");

            // Act / Assert
            var exception = Assert.Throws<System.ArgumentException>(
                () => new StateMachine<NotAnEnum>(stubStates, default(NotAnEnum)));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void ChangeState_ValidState_ExitsThenEnters()
        {
            // Arrange
            var log = new StateMachineDiagonosticsLog<TwoStatesID>();
            var stubStates = new StubStateWithDiagnostics<TwoStatesID>[]
            {
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.One, log),
                new StubStateWithDiagnostics<TwoStatesID>(TwoStatesID.Two, log),
            };

            var stateMachine = new StateMachine<TwoStatesID>(stubStates, TwoStatesID.One);
            var expectedLog = new StateMachineDiagonosticsLog<TwoStatesID>();
            expectedLog.AddEntry(StateMachineDiagonosticsLog<TwoStatesID>.LogEntry.Callback.Enter, TwoStatesID.One);
            expectedLog.AddEntry(StateMachineDiagonosticsLog<TwoStatesID>.LogEntry.Callback.Exit, TwoStatesID.One);
            expectedLog.AddEntry(StateMachineDiagonosticsLog<TwoStatesID>.LogEntry.Callback.Enter, TwoStatesID.Two);

            // Act
            stateMachine.ChangeState(TwoStatesID.Two);

            // Assert
            Assert.That(log, Is.EqualTo(expectedLog));
        }
    }
}