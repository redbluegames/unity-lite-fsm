namespace RedBlueGames.LiteFSM.Tests
{
    using System;

    public class StateStubNoEnum : IState<NotAnEnum>
    {
        public NotAnEnum ID { get; private set; }

        public StateStubNoEnum(NotAnEnum id)
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