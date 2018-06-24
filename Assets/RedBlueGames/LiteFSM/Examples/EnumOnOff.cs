namespace RedBlueGames.LiteFSM
{
    using UnityEngine;

    public class EnumOnOff : MonoBehaviour
    {
        private enum OnOffStateID
        {
            Off = 0,
            On = 1
        }

        private StateMachine<OnOffStateID> stateMachine;

        private void Awake()
        {
            // Reflected Setup
            var reflector = new StateReflector<OnOffStateID>(this);
            this.stateMachine = new StateMachine<OnOffStateID>(reflector, OnOffStateID.Off);
        }

        private void Update()
        {
            this.stateMachine.Update(Time.deltaTime);
        }

        private void EnterOff()
        {
            Debug.Log(gameObject.name + "_EnterOff");
        }

        private void ExitOff()
        {
            Debug.Log(gameObject.name + "_ExitOff");
        }

        private void UpdateOff(float dT)
        {
            //Debug.Log(gameObject.name + "_UpdateOff: " + dT);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.stateMachine.ChangeState(OnOffStateID.On);
            }
        }

        private void EnterOn()
        {
            Debug.Log(gameObject.name + "_EnterOn");
        }

        private void ExitOn()
        {
            Debug.Log(gameObject.name + "_ExitOn");
        }

        private void UpdateOn(float dT)
        {
            //Debug.Log(gameObject.name + "_UpdateOn: " + dT);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.stateMachine.ChangeState(OnOffStateID.Off);
            }
        }
    }
}