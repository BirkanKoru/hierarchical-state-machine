using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineFactory
{
    Dictionary<string, State> states;

    public State currState { get; private set; }
    public string currStateName { get; private set; }

    public void AddStates(Dictionary<string, State> states)
    {
        this.states = states;
    }

    public State SetCurrState(string stateName)
    {
        foreach(KeyValuePair<string, State> kvp in states)
        {
            if(kvp.Key.Equals(stateName))
            {
                currStateName = kvp.Key;
                currState = kvp.Value;
            }
        }

        return currState;
    }
}
