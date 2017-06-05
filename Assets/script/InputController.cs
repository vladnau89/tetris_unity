using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public static event Action<MoveDirection> OnChangeDirection;


    const float DEAD_ZONE = 0.6f;

    [SerializeField] private string _horizontalAxe  = "Horizontal";
    [SerializeField] private string _verticalAxe    = "Vertical";

    [SerializeField] private int _actionsPerSeconds = 10;

    private MoveDirection _prevDirection;
    private float _prevTime;


    private void Update()
    {      
        Vector2 movement = Vector2.zero;
        float x = Input.GetAxisRaw(_horizontalAxe);
        float y = Input.GetAxisRaw(_verticalAxe);

        MoveDirection direction = GetMoveDirection(x, y);

        if (direction != MoveDirection.None)
        {
            float time = Time.unscaledTime;

            if (_prevDirection == direction)
            {              
                bool allow = time > _prevTime + 1f / _actionsPerSeconds;

                if (!allow) return;
            }

            _prevTime = time;
            //MoveBrick(direction);

            if (OnChangeDirection != null) OnChangeDirection(direction);

        }

        _prevDirection = direction;
    }

    public static MoveDirection GetMoveDirection(float x , float y)
    {
        return GetMoveDirection(x, y, DEAD_ZONE);
    }

    public static MoveDirection GetMoveDirection(float x, float y , float deadZone)
    {
        if ( new Vector2(x , y).sqrMagnitude < deadZone * deadZone)
        {
            return MoveDirection.None;
        }

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            return x > 0 ? MoveDirection.Right : MoveDirection.Left;
        }
        else
        {
            return y > 0 ? MoveDirection.Up : MoveDirection.Down;
        }

    }

}
