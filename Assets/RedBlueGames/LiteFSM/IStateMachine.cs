namespace RedBlueGames.LiteFSM
{
    using System;

    /// <summary>
    /// State machine interface
    /// </summary>
    public interface IStateMachine<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        /// <summary>
        /// Change the StateMachine to the specified state.
        /// </summary>
        /// <param name="desiredStateID">Desired state.</param>
        void ChangeState(T desiredStateID);

        /// <summary>
        /// Update the StateMachine with the specified deltaTime.
        /// </summary>
        /// <param name="deltaTime">Delta time - time elapsed between updates.</param>
        void Update(float deltaTime);
    }
}