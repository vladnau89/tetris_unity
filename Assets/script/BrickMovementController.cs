using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrickMovementController : MonoBehaviour {

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
        if (GamePlay.CurrentState != GamePlay.State.Playing) return;

        MoveBrick(direction);
    }


    private static void MoveBrick(MoveDirection direction)
    {
        if (direction == MoveDirection.Left || direction == MoveDirection.Right)
        {
            int dx = direction == MoveDirection.Left ? -1 : +1;
            int dy = 0;

            TryMoveBrick(dx, dy);
        }
        else if (direction == MoveDirection.Down)
        {
            int dx = 0;
            int dy = -1;

            TryMoveBrick(dx, dy);
        }
        else if (direction == MoveDirection.Up)
        {
            GamePlay.CurrentBrick.Rotate();
        }
    }


    private static void TryMoveBrick(int dx, int dy)
    {
        int posX = GamePlay.CurrentBrick.PositionX;
        int posY = GamePlay.CurrentBrick.PositionY;

        GamePlay.CurrentBrick.TryMove(posX + dx, posY + dy);
    }

}
