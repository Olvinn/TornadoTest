using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wrapper for processing input signals 
/// Now it's just contains MovementEvent
/// </summary>
[CreateAssetMenu(fileName = "InputCOntroller")]
public class InputController : ScriptableObject
{
    public MovementEvent onDirChanged = new MovementEvent();
}

public class MovementEvent : UnityEvent<Vector2>
{ }
