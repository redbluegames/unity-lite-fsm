namespace RedBlueGames.LiteFSM
{
    using System;

    /// <summary>
    /// State reflector interface.
    /// </summary>
    public interface IStateReflector<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        /// <summary>
        /// Gets a collection of states, with an enum Identifier
        /// </summary>
        /// <returns>The states.</returns>
        State<T>[] GetStates();
    }
}
