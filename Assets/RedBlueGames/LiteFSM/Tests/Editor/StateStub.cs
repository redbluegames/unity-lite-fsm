namespace RedBlueGames.LiteFSM.Tests
{
    using System;

    public class StubState<T> : IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        public bool EnterCalled { get; private set; }
        public bool ExitCalled { get; private set; }
        public bool UpdateCalled { get; private set; }
        public float LastDeltaTime { get; private set; }

        public T ID { get; private set; }

        public StubState(T id)
        {
            this.ID = id;
        }

        public void Enter()
        {
            this.EnterCalled = true;
        }

        public void Exit()
        {
            this.ExitCalled = true;
        }

        public void Update(float dT)
        {
            this.UpdateCalled = true;
            this.LastDeltaTime = dT;
        }
    }
}