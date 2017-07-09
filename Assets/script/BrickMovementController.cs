using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrickMovementController : MonoBehaviour
{
    [SerializeField] private float _gravityPerSeconds = 10;

    float _prevtime;

    private void Start()
    {
        _prevtime = Time.unscaledTime + +1 / _gravityPerSeconds;
    }


    private void Update()
    {
        if (GamePlay.CurrentState != GamePlay.State.GenerateNext) return;

        var brick = GamePlay.CurrentBrick;
        if (brick != null)
        {
            var time = Time.unscaledTime;
            if (time > _prevtime + 1 / _gravityPerSeconds)
            {
                _prevtime = time;
                if (MoveBrick(MoveDirection.Down) == false)
                {
                    GamePlay.FixBrick(brick);
                }
            }          
        }
    }

    private void OnEnable()
    {
        InputController.OnChangeDirection += OnChangeDirection;
    }

    private void OnDisable()
    {
        InputController.OnChangeDirection -= OnChangeDirection;
    }

    private void OnChangeDirection(MoveDirection direction)
    {
        if (GamePlay.CurrentState != GamePlay.State.GenerateNext) return;

        MoveBrick(direction);
    }


    private static bool MoveBrick(MoveDirection direction)
    {
        if (direction == MoveDirection.Left || direction == MoveDirection.Right)
        {
            int dx = direction == MoveDirection.Left ? -1 : +1;
            int dy = 0;

            return TryMoveBrick(dx, dy);
        }
        else if (direction == MoveDirection.Down)
        {
            int dx = 0;
            int dy = -1;

            return TryMoveBrick(dx, dy);
        }
        else if (direction == MoveDirection.Up)
        {
            return GamePlay.CurrentBrick.TryRotate(Settings.WallKick);
        }

        return false;
    }


    private static bool TryMoveBrick(int dx, int dy)
    {
        int posX = GamePlay.CurrentBrick.PositionX;
        int posY = GamePlay.CurrentBrick.PositionY;

        return GamePlay.CurrentBrick.TryMove(posX + dx, posY + dy);
    }

}
