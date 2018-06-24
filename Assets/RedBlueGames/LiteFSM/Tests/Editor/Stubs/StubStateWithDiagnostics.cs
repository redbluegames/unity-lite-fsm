namespace RedBlueGames.LiteFSM.Tests
{
    using System;

    public class StubStateWithDiagnostics<T> : IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        public T ID { get; private set; }

        public StateMachineDiagonosticsLog<T> Log { get; set; }

        public StubStateWithDiagnostics(T id, StateMachineDiagonosticsLog<T> diagonosticsLog)
        {
            this.ID = id;
            this.Log = diagonosticsLog;
        }

        public void Enter()
        {
            this.Log.AddEntry(StateMachineDiagonosticsLog<T>.LogEntry.Callback.Enter, this.ID);
        }

        public void Exit()
        {
            this.Log.AddEntry(StateMachineDiagonosticsLog<T>.LogEntry.Callback.Exit, this.ID);
        }

        public void Update(float dT)
        {
            this.Log.AddEntry(StateMachineDiagonosticsLog<T>.LogEntry.Callback.Update, this.ID, dT);
        }
    }
}