namespace RedBlueGames.ReflectedEnumFSM
{
    using UnityEngine;

    public class EnumOnOffUnreflected : MonoBehaviour
    {
        private enum OnOffStateID
        {
            Off = 0,
            On = 1
        }

        private StateMachine<OnOffStateID> stateMachine;

        private void Awake()
        {
            // Unreflected Setup
            var stateList = new System.Collections.Generic.List<State<OnOffStateID>>();
            stateList.Add(new State<OnOffStateID>(OnOffStateID.Off, this.EnterOff, null, this.UpdateOff));
            stateList.Add(new State<OnOffStateID>(OnOffStateID.On, this.EnterOn, null, this.UpdateOn));
            this.stateMachine = new StateMachine<OnOffStateID>(stateList.ToArray(), OnOffStateID.Off);
        }

        private void Update()
        {
            this.stateMachine.Update(Time.deltaTime);
        }

        private void EnterOff()
        {
            Debug.Log(gameObject.name + "_EnterOff");
        }

        private void UpdateOff(float dT)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.stateMachine.ChangeState(OnOffStateID.On);
            }
        }

        private void EnterOn()
        {
            Debug.Log(gameObject.name + "_EnterOn");
        }

        private void UpdateOn(float dT)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.stateMachine.ChangeState(OnOffStateID.Off);
            }
        }
    }
}