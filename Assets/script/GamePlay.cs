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

    [SerializeField] private int _fieldWidth = 10;

    private State _currentState;
    private Brick _currentBrick;

    public static State CurrentState    { get { return Instance._currentState;      } }
    public static Brick CurrentBrick    { get { return Instance._currentBrick;      } }
    public static int   FieldWidth      { get { return Instance._fieldWidth;        } }

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

                _currentBrick = BrickGenerator.GenerateNext();

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
