namespace RedBlueGames.LiteFSM
{
    using System;
    using System.Collections.Generic;

    public class StateMachine<T> : IStateMachine<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private Dictionary<T, IState<T>> states;
        private T currentStateID;

        private IState<T> CurrentState
        {
            get
            {
                return this.states[this.currentStateID];
            }
        }

        public StateMachine(IStateReflector<T> reflector, T initialStateID)
        {
            var states = reflector.GetStates();
            this.Initialize(states, initialStateID);
        }

        public StateMachine(IState<T>[] states, T initialStateID)
        {
            this.Initialize(states, initialStateID);
        }

        public void ChangeState(T desiredStateID)
        {
            // TODO: Validate desiredState is in the dictionary

            // Can't exit and re-enter the same state
            if (desiredStateID.Equals(this.currentStateID))
            {
                return;
            }

            this.CurrentState.Exit();
            this.currentStateID = desiredStateID;
            this.CurrentState.Enter();
        }

        public void Update(float deltaTime)
        {
            this.CurrentState.Update(deltaTime);
        }

        private void Initialize(IState<T>[] states, T initialStateID)
        {
            /*
            // Validate that all entries of T are supplied
            // TODO: Test for missing entries
            var missingEntries = this.GetMissingIDs(states);
            foreach (var missingEntry in missingEntries)
            {
                UnityEngine.Debug.Log("Missing: " + missingEntry.ToString());
            }

            // TODO: Too many entries (doubled up)

            // TODO: Entries of Different types of T? Like, two different enums?

            // TODO: Test it's an enum (typeof(T).IsEnum)
            */

            this.states = new Dictionary<T, IState<T>>();
            foreach (var state in states)
            {
                this.states.Add(state.ID, state);
            }

            // TODO: Validate desiredState is in the dictionary
            this.currentStateID = initialStateID;
            this.CurrentState.Enter();
        }

        /*
        private T[] GetMissingIDs(IState<T>[] states)
        {
            var foundTs = new List<T>();
            foreach (var state in states)
            {
                foundTs.Add(state.ID);
            }

            var entriesInT = Enum.GetValues(typeof(T));
            var missingTs = new List<T>();
            for (int i = 0; i < entriesInT.Length; i++)
            {
                var entry = (T)entriesInT.GetValue(i);
                if (!foundTs.Contains(entry))
                {
                    missingTs.Add(entry);
                }
            }

            return missingTs.ToArray();
        }*/
    }
}