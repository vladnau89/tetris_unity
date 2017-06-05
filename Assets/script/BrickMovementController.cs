﻿using System.Collections;
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
            int posX = GamePlay.CurrentBrick.PositionX;
            posX += direction == MoveDirection.Left ? -1 : +1;
            GamePlay.CurrentBrick.PositionX = Mathf.Clamp(posX, -GamePlay.FieldWidth / 2, +GamePlay.FieldWidth / 2);
        }
        else if (direction == MoveDirection.Down)
        {
            int posY = GamePlay.CurrentBrick.PositionY;
            posY -= 1;

            GamePlay.CurrentBrick.PositionY = Mathf.Clamp(posY, -GamePlay.FieldHeight / 2 + 2, +GamePlay.FieldHeight / 2);
        }
        else if (direction == MoveDirection.Up)
        {
            GamePlay.CurrentBrick.Rotate();
        }

    }


}
