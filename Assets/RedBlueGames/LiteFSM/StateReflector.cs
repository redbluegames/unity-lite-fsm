namespace RedBlueGames.LiteFSM
{
    using System;

    public class StateReflector<T> : IStateReflector<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private object instanceToReflect;

        private string enterMethodPattern;
        private string exitMethodPattern;
        private string updateMethodPattern;

        public StateReflector(object instanceToReflect) : this(instanceToReflect, "Enter{0}", "Exit{0}", "Update{0}") { }

        public StateReflector(object instanceToReflect, string enterMethodPattern, string exitMethodPattern, string updateMethodPattern)
        {
            this.instanceToReflect = instanceToReflect;
            this.enterMethodPattern = enterMethodPattern;
            this.exitMethodPattern = exitMethodPattern;
            this.updateMethodPattern = updateMethodPattern;
        }

        public State<T>[] GetStates()
        {
            var enumValues = Enum.GetValues(typeof(T));
            var enumNames = Enum.GetNames(typeof(T));
            var states = new State<T>[enumNames.Length];
            for (int i = 0; i < states.Length; ++i)
            {
                var state = this.CreateStateByName(enumNames[i], (T)enumValues.GetValue(i));
                states[i] = state;
            }

            return states;
        }

        private State<T> CreateStateByName(string enumName, T enumValue)
        {
            var enterMethodName = string.Format(this.enterMethodPattern, enumName);
            var enterMethod = this.FindEnterDelegateByName(this.instanceToReflect, enterMethodName);

            var exitMethodName = string.Format(this.exitMethodPattern, enumName);
            var exitMethod = this.FindExitDelegateByName(this.instanceToReflect, exitMethodName);

            var updateMethodName = string.Format(this.updateMethodPattern, enumName);
            var updateMethod = this.FindUpdateDelegateByName(this.instanceToReflect, updateMethodName);

            var state = new State<T>(enumValue, enterMethod, exitMethod, updateMethod);
            return state;
        }

        private State<T>.EnterMethod FindEnterDelegateByName(Object classInstanceToReflect, string methodName)
        {
            return CreateDelegateForMethodByname(classInstanceToReflect, typeof(State<T>.EnterMethod), methodName) as State<T>.EnterMethod;
        }

        private State<T>.ExitMethod FindExitDelegateByName(Object classInstanceToReflect, string methodName)
        {
            return CreateDelegateForMethodByname(classInstanceToReflect, typeof(State<T>.ExitMethod), methodName) as State<T>.ExitMethod;
        }

        private State<T>.UpdateMethod FindUpdateDelegateByName(Object classInstanceToReflect, string methodName)
        {
            return CreateDelegateForMethodByname(classInstanceToReflect, typeof(State<T>.UpdateMethod), methodName) as State<T>.UpdateMethod;
        }

        private static Delegate CreateDelegateForMethodByname(Object classInstanceToReflect, Type delegateType, string methodName)
        {
            var methodInfo = classInstanceToReflect.GetType().GetMethod(methodName,
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            if (methodInfo != null)
            {
                return Delegate.CreateDelegate(delegateType, classInstanceToReflect, methodInfo);
            }

            return null;
        }
    }
}