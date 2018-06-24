namespace RedBlueGames.LiteFSM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// State machine manages a collection of states, enforcing that only one is entered
    /// at any time. It calling each State's Enter and Exit method as the StateMachine
    /// moves between states.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of a <see cref="T:RedBlueGames.LiteFSM.StateMachine`1"/>.
        /// Immediately Enters (calls the Enter method on) the supplied Initial state.
        /// </summary>
        /// <param name="states">States to use. All entries in T must be represented in this array.</param>
        /// <param name="initialStateID">Initial state identifier. This will be entered immediately.</param>
        public StateMachine(IState<T>[] states, T initialStateID)
        {
            this.Initialize(states, initialStateID);
        }

        /// <summary>
        /// Change the StateMachine to the specified state. Exits (calls the
        /// Exit method on) the current state and Enters the desired one.
        /// </summary>
        /// <param name="desiredStateID">ID of the desired state.</param>
        public void ChangeState(T desiredStateID)
        {
            // Can't exit and re-enter the same state
            if (desiredStateID.Equals(this.currentStateID))
            {
                return;
            }

            this.CurrentState.Exit();
            this.currentStateID = desiredStateID;
            this.CurrentState.Enter();
        }

        /// <summary>
        /// Update the StateMachine with the specified deltaTime.
        /// </summary>
        /// <param name="deltaTime">Delta time - time elapsed between updates.</param>
        public void Update(float deltaTime)
        {
            this.CurrentState.Update(deltaTime);
        }

        private void Initialize(IState<T>[] states, T initialStateID)
        {
            this.VerifyTIsEnum();
            this.VerifyStatesRepresentAllEntriesOfT(states);

            this.states = new Dictionary<T, IState<T>>();
            foreach (var state in states)
            {
                this.states.Add(state.ID, state);
            }

            this.currentStateID = initialStateID;
            this.CurrentState.Enter();
        }

        private void VerifyTIsEnum()
        {
            if (!typeof(T).IsEnum)
            {
                var message = string.Concat(
                    "StateMachine trying to initialize with an invalid generic type. " +
                    "Generic type (T) is not an Enum. Type: " + typeof(T).ToString());
                throw new System.ArgumentException(message);
            }
        }

        private void VerifyStatesRepresentAllEntriesOfT(IState<T>[] states)
        {
            this.VerifyStatesArentMissing(states);
            this.VerifyNoStatesAreDuplicates(states);
        }

        private void VerifyStatesArentMissing(IState<T>[] states)
        {
            var missingEntries = GetMissingIDs(states);
            if (missingEntries.Length > 0)
            {
                var message = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Not enough states passed in. Missing states: ");
                foreach (var missingEntry in missingEntries)
                {
                    message = string.Concat(message, missingEntry.ToString());
                    if (!missingEntry.Equals(missingEntries[missingEntries.Length - 1]))
                    {
                        message = string.Concat(message, ", ");
                    }
                }

                throw new System.ArgumentException(message);
            }
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

        private void VerifyNoStatesAreDuplicates(IState<T>[] states)
        {
            var duplicateIDs = GetDuplicateIDs(states);
            if (duplicateIDs.Length > 0)
            {
                var message = string.Concat(
                    "StateMachine trying to initialize with an invalid set of states. " +
                    "Duplicate states passed in. Duplicate states: ");
                foreach (var duplicateEntry in duplicateIDs)
                {
                    message = string.Concat(message, duplicateEntry.ToString());
                    if (!duplicateEntry.Equals(duplicateIDs[duplicateIDs.Length - 1]))
                    {
                        message = string.Concat(message, ", ");
                    }
                }

                throw new System.ArgumentException(message);
            }
        }

        private static T[] GetDuplicateIDs(IState<T>[] states)
        {
            var extraStates = new List<T>();
            foreach (var state in states)
            {
                extraStates.Add(state.ID);
            }

            var entriesInT = Enum.GetValues(typeof(T));
            for (int i = 0; i < entriesInT.Length; i++)
            {
                var entry = (T)entriesInT.GetValue(i);
                if (extraStates.Contains(entry))
                {
                    extraStates.Remove(entry);
                }
            }

            return extraStates.ToArray();
        }
    }
}