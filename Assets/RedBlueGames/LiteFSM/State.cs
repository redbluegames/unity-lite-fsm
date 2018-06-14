namespace RedBlueGames.LiteFSM
{
    using System;

    public class State<T> : IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private EnterMethod enter;
        private ExitMethod exit;
        private UpdateMethod update;

        public delegate void EnterMethod();
        public delegate void ExitMethod();
        public delegate void UpdateMethod(float deltaTime);

        public T ID { get; private set; }

        public State(T ID, EnterMethod enter, ExitMethod exit, UpdateMethod update)
        {
            this.ID = ID;
            this.enter = enter;
            this.exit = exit;
            this.update = update;
        }

        public void Enter()
        {
            if (this.enter != null)
            {
                this.enter();
            }
        }

        public void Exit()
        {
            if (this.exit != null)
            {
                this.exit();
            }
        }

        public void Update(float deltaTime)
        {
            if (this.update != null)
            {
                this.update(deltaTime);
            }
        }
    }
}
