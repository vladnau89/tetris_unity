using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : SingletonMonoBehaviour<GamePlay>
{
    public enum State
    {
        Ready,
        Playing,
        GameOver
    }


    private State _currentState;

    public static State CurrentState { get { return Instance._currentState; } }

    private void Awake()
    {
        SetState(State.Ready);
    }


    private void SetState(State state)
    {
        Debug.LogError("SetState " + state);

        _currentState = state;

        switch (state)
        {
            case State.Ready:
                SetState(State.Playing);
                break;
            case State.Playing:

                var brick = BrickGenerator.GenerateNext();


                break;
            case State.GameOver:
                break;
        }

    }

    private void Update()
    {
        if (_currentState != State.Playing) return;



    }

}
