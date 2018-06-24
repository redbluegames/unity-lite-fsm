namespace RedBlueGames.LiteFSM.Tests
{
    using System;

    public class StubStateWithInvalidEnum : IState<NotAnEnum>
    {
        public NotAnEnum ID { get; private set; }

        public StubStateWithInvalidEnum(NotAnEnum id)
        {
            this.ID = id;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Update(float dT)
        {
        }
    }
}