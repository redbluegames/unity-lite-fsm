namespace RedBlueGames.ReflectedEnumFSM
{
    using System;

    public interface IStateMachine<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        void ChangeState(T desiredStateID);

        void Update(float deltaTime);
    }
}