using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct StateProperties
{
    public string state;
    public UnityEvent[] OnEnter;
    public UnityEvent[] OnExit;
}

public abstract class StateMachine<T> where T : MonoBehaviour
{
    public StateProperties properties { get; set; }

    public T main { get; set; }

    public static StateMachine<T> GetState(string pattern)
    {
        Type t = Type.GetType(pattern);
        if (t == null)
        {
            throw new Exception("Type " + pattern + " not found.");
        }

        return (StateMachine<T>)Activator.CreateInstance(t);
    }

    public virtual void Listener() { }

    public virtual void OnEnter()
    {
        foreach(var action in properties.OnEnter)
        {
            action.Invoke();
        }
    }

    public virtual void OnExit()
    {
        foreach (var action in properties.OnExit)
        {
            action.Invoke();
        }
    }
    public abstract void OnUpdate();
}
