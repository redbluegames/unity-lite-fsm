namespace RedBlueGames.ReflectedEnumFSM
{
    using System;

    public interface IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        T ID { get; }

        void Enter();

        void Exit();

        void Update(float dt);
    }
}