namespace RedBlueGames.LiteFSM
{
    using System;

    /// <summary>
    /// State is a collection of callbacks to state methods, with an identifier.
    /// </summary>
    public class State<T> : IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private EnterMethod enter;
        private ExitMethod exit;
        private UpdateMethod update;

        public delegate void EnterMethod();
        public delegate void ExitMethod();
        public delegate void UpdateMethod(float deltaTime);

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public T ID { get; private set; }

        /// <summary>
        /// Initializes a new instance of a <see cref="T:RedBlueGames.LiteFSM.State`1"/>.
        /// </summary>
        /// <param name="ID">Identifier for the state.</param>
        /// <param name="enter">Enter callback.</param>
        /// <param name="exit">Exit callback.</param>
        /// <param name="update">Update callback.</param>
        public State(T ID, EnterMethod enter, ExitMethod exit, UpdateMethod update)
        {
            this.ID = ID;
            this.enter = enter;
            this.exit = exit;
            this.update = update;
        }

        /// <summary>
        /// Enter the state
        /// </summary>
        public void Enter()
        {
            if (this.enter != null)
            {
                this.enter();
            }
        }

        /// <summary>
        /// Exit the state
        /// </summary>
        public void Exit()
        {
            if (this.exit != null)
            {
                this.exit();
            }
        }

        /// <summary>
        /// Update the state with a specified deltaTime.
        /// </summary>
        /// <param name="deltaTime">DeltaTime - the time elapsed between updates.</param>
        public void Update(float deltaTime)
        {
            if (this.update != null)
            {
                this.update(deltaTime);
            }
        }
    }
}
