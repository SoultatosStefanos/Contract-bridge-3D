using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameState _currState;

    private void Start()
    {
        TransitionTo(GetComponent<SetupState>());
    }

    public void TransitionTo(GameState newState)
    {
        if (_currState)
        {
            Debug.Log("Exiting state: " + _currState);
            _currState.OnExit();
        }

        _currState = newState;

        if (_currState)
        {
            Debug.Log("Entering state: " + _currState);
            _currState.OnEnter();
        }
    }
}