﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    private void Update()
    {
        if (GamePlay.CurrentState != GamePlay.State.Playing) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            GamePlay.CurrentBrick.Rotate();
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GamePlay.CurrentBrick.MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GamePlay.CurrentBrick.MoveRight();
        }


    }

}
