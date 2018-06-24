namespace RedBlueGames.LiteFSM.Tests
{
    using System;
    using System.Collections.Generic;

    public class StateMachineDiagonosticsLog<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private List<LogEntry> Entries { get; set; }

        public StateMachineDiagonosticsLog()
        {
            this.Entries = new List<LogEntry>();
        }

        public void AddEntry(LogEntry.Callback call, T state)
        {
            this.AddEntry(call, state, -1.0f);
        }

        public void AddEntry(LogEntry.Callback call, T state, float floatParam)
        {
            this.Entries.Add(new LogEntry() { State = state, Call = call, FloatParam = floatParam });
        }

        public override string ToString()
        {
            var fullLog = "StateMachineLog:\n";
            foreach (var logEntry in this.Entries)
            {
                fullLog = string.Concat(fullLog, logEntry, "\n");
            }
            fullLog = string.Concat(fullLog, "End of Log");
            return fullLog;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var otherDiagonostics = (StateMachineDiagonosticsLog<T>)obj;
            if (this.Entries.Count != otherDiagonostics.Entries.Count)
            {
                return false;
            }

            for (int i = 0; i < this.Entries.Count; ++i)
            {
                var thisEntry = this.Entries[i];
                var otherEntry = otherDiagonostics.Entries[i];
                if (!thisEntry.Equals(otherEntry))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.Entries.GetHashCode();
        }

        public struct LogEntry
        {
            public enum Callback
            {
                Enter,
                Exit,
                Update
            }

            public T State { get; set; }

            public Callback Call { get; set; }

            public float FloatParam { get; set; }

            public override string ToString()
            {
                return string.Concat("LogEntry: { Callback: ", Call, ", State: ", State, ", FloatParam: ", FloatParam);
            }
        }
    }
}
