using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : SingletonMonoBehaviour<GamePlay>
{
    public enum State
    {
        Ready,
        GenerateNext,
        GameOver
    }


    private State _currentState;
    private Brick _currentBrick;

    public static State CurrentState    { get { return Instance._currentState;      } }
    public static Brick CurrentBrick    { get { return Instance._currentBrick;      } }


    private void Awake()
    {
        Application.targetFrameRate = 60;
        SetState(State.Ready);
    }

    private void SetState(State state)
    {
        Debug.LogWarning("SetState " + state);

        _currentState = state;

        switch (state)
        {
            case State.Ready:
                SetState(State.GenerateNext);
                break;
            case State.GenerateNext:

                var x = CellMatrix.Width / 2;
                var y = 0;
                _currentBrick = BrickGenerator.GenerateNext(x , y);

                break;
            case State.GameOver:
                break;
        }
    }

    public static void FixBrick(Brick brick)
    {
        brick.FixBrick();
        CellMatrix.CheckLine();
        Instance.SetState(State.GenerateNext);
    }

    private void Update()
    {
        if (_currentState != State.GenerateNext) return;



    }

}
