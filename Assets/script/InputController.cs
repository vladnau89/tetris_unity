using System.Collections;
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
            int posX = GamePlay.CurrentBrick.PositionX;
            posX -= 1;
            GamePlay.CurrentBrick.PositionX = Mathf.Clamp(posX, -GamePlay.FieldWidth / 2, +GamePlay.FieldWidth / 2);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int posX = GamePlay.CurrentBrick.PositionX;
            posX += 1;
            GamePlay.CurrentBrick.PositionX = Mathf.Clamp(posX, -GamePlay.FieldWidth / 2, +GamePlay.FieldWidth / 2);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GamePlay.CurrentBrick.PositionY -= 1;
        }


    }

}
