using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Brick : MonoBehaviour
{
    public int PositionX
    {
        get { return (int)this.transform.localPosition.x; }
        private set
        {
            var position = this.transform.localPosition;
            position.x = value;
            this.transform.localPosition = position;
        }
    }

    public int PositionY
    {
        get { return (int)this.transform.localPosition.y; }
        private set
        {
            var position = this.transform.localPosition;
            position.y = value;
            this.transform.localPosition = position;
        }
    }
    
    [SerializeField] private GameObject[] bricks = new GameObject[16];

    private BrickSettings _settings;
    private int _maskIndex;

    public void Apply(BrickSettings settings, int maskIndex)
    {
        _settings = settings;
        _maskIndex = maskIndex;

        ushort mask = settings.rotationMasks[maskIndex];

        Repaint(mask);

        base.name = settings.name;
    }

    private void Repaint(ushort mask)
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            bool active = Convert.ToBoolean(mask & 1 << i);
            bricks[i].SetActive(active);
        }
    }

    public bool TryMove(int x, int y)
    {
        int dx = x - PositionX;
        int dy = y - PositionY;

        List<Vector2> newPartPositions = new List<Vector2>();

        for (int i = 0; i < bricks.Length; i++)
        {
            var brick = bricks[i];
            if (brick.activeSelf)
            {
                Vector2 partPosition = brick.transform.position;
                partPosition.x += dx;
                partPosition.y += dy;
                newPartPositions.Add(partPosition);
            }
        }

        if (CellMatrix.IsCellsAvailable(newPartPositions))
        {
            PositionX = x;
            PositionY = y;
            return true;
        }

        return false;
    }


    [ContextMenu("Rotate")]
    public void Rotate()
    {
        ++_maskIndex;
        ushort maskNext = _settings.rotationMasks[_maskIndex % _settings.rotationMasks.Length];
        Repaint(maskNext);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            Gizmos.DrawWireCube(bricks[i].GetComponent<Renderer>().bounds.center, bricks[i].transform.localScale);
        }
    }

}
