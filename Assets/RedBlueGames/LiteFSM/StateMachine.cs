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
            var reflectedStates = reflector.GetStates();
            this.Initialize(reflectedStates, initialStateID);
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
            // Validate that all entries of T are supplied
            var missingEntries = GetMissingIDs(states);
            if (missingEntries.Length > 0)
            {
                var message = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Not enough states passed in. Missing states:");
                foreach (var missingEntry in missingEntries)
                {
                    message = string.Concat(message, missingEntry.ToString(), ", ");
                }

                throw new System.ArgumentException(message);
            }

            /*
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

        private static T[] GetMissingIDs(IState<T>[] states)
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
        }
    }
}