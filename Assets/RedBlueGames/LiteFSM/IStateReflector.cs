namespace RedBlueGames.LiteFSM
{
    using System;

    public interface IStateReflector<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        State<T>[] GetStates();
    }
}
