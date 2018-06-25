# unity-lite-fsm
LiteFSM is a lightweight finite state machine framework in C#. It's intended for use within Unity game engine, though it is not required.

# Overview
State machines can be a very readable and maintainable way to organize and manage state. LiteFSM is intended for very small classes whose state (member variables) can easily be thought about as a state machine. For me this is often in UI elements, where it is difficult to avoid state, and whose behavior is easily visualized as a state machine. An example of this would be button, which might have Normal, Pressed, and Focused / Highlighted states.

## Using LiteFSM
Here is a simple example of how to create and use a StateMachine to manage state.

```C#
public class OnOffUnreflected : MonoBehaviour
{
    private bool isOn;
    private StateMachine<OnOffStateID> stateMachine;

    private enum OnOffStateID
    {
        Off = 0,
        On = 1
    }

    private void Awake()
    {
        var stateList = new List<State<OnOffStateID>>();
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
        this.isOn = false;
    }

    private void UpdateOff(float dT)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            this.stateMachine.ChangeState(OnOffStateID.On);
    }

    private void EnterOn()
    {
        this.isOn = true;
    }

    private void UpdateOn(float dT)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            this.stateMachine.ChangeState(OnOffStateID.Off);
    }
}
```

As you can see from the above snippet, LiteFSM States are `Enum` based. You do not need to create a new class for every state. Instead, each State and StateMachine is assigned an enum that represents all of its states. States are still objects, but they only contain an ID (enum entry), and references to three optional callbacks: `Enter`, `Exit`, and `Update`.

There is also a StateReflector object that you can use to help reduce boilerplate of creating the state array:

```C#
private void Awake()
{
    // Reflected Setup
    var reflector = new StateReflector<OnOffStateID>(this);
    this.stateMachine = new StateMachine<OnOffStateID>(reflector.GetStates(), OnOffStateID.Off);
}

private void EnterOn()
{
    this.isOn = true;
}

private void ExitOn()
{
    this.isOn = false;
}
...
```

## Why LiteFSM?
There are many different ways to implement State Machines. At RedBlueGames we use a much more powerful, class based Hierarchical State Machine framework for our bigger projects. But often on smaller projects we'll have a simple object that doesn't need to grow or be extended much. In these cases we often found ourselves using simple `switch` statement based state machines, with manual calls to "Enter" and "Exit" methods. This was brittle, and required a lot of boilerplate. So we wanted to make the simplest possible solution to solve those problems.

LiteFSM is heavily influenced by [Prime31's StateKitLite](https://github.com/prime31/StateKit/blob/master/Assets/StateKit/StateKitLite.cs). StateKitLite is also an enum based StateMachine framework, but it uses inheritance, and under-the-hood reflection, which we wanted to avoid. Inheritance requires that StateMachines derive from MonoBehaviour, which is less flexible. Having it auto-reflect for methods is a perfectly valid design choice, but we prefer to be more explicit, even if it is more code.

# Installation
There are two ways you can install LiteFSM.
* [Todo] Via Unity Custom Package:
  1. Download the latest package in the Releases section.
  2. From your project in Unity, install the custom package through `Assets/Import Package/Custom Package`
* Via git:
  1. Clone this project or download the source
  2. Copy the contents of Assets/RedBlueGames/ folder into your project folder in Unity

# Contributing
We don't immediately have need for contributions to this repository, as it's as featureful as we'd like. Bugfixes, tech debt, and documentation fixes are welcome, but we likely won't change the core API or functionality.

You can also help out with our [other open source projects](https://github.com/redbluegames), or just support us by checking out the [RedBlueGames website](http://redbluegames.com/) and [blog](http://blog.redbluegames.com/) and following us on [Twitter](https://twitter.com/redbluegames).
