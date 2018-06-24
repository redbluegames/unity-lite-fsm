namespace RedBlueGames.LiteFSM
{
    using System;

    /// <summary>
    /// State Interface
    /// </summary>
    public interface IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        /// <summary>
        /// Gets the identifier (enum entry) of the State
        /// </summary>
        T ID { get; }

        /// <summary>
        /// Enter the state
        /// </summary>
        void Enter();

        /// <summary>
        /// Exit the state
        /// </summary>
        void Exit();

        /// <summary>
        /// Update the state with a specified deltaTime.
        /// </summary>
        /// <param name="deltaTime">DeltaTime - the time elapsed between updates.</param>
        void Update(float deltaTime);
    }
}