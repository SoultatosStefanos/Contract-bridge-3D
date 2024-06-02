using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public abstract void OnEnter();

    public abstract void OnExit();
}