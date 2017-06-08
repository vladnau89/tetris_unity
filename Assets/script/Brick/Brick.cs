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
        bool rotate = false;
        return TryTranslate(x, y, rotate);
    }

    public bool TryRotate()
    {
        // don`t change position
        int x = PositionX;
        int y = PositionY;
        bool rotate = true;
        return TryTranslate(x, y, rotate);
    }

    private bool TryTranslate(int x, int y, bool rotate)
    {
        // increment index if rotate = true
        int mask_index_next = (_maskIndex + Convert.ToInt32(rotate)) % _settings.rotationMasks.Length;
        ushort mask = _settings.rotationMasks[mask_index_next];
        int dx = x - PositionX;
        int dy = y - PositionY;

        List<Vector2> newBricksPositions = GetNewBricksPosition(mask, dx, dy);

        if (CellMatrix.IsCellsAvailable(newBricksPositions))
        {
            if (dx != 0) PositionX = x;
            if (dy != 0) PositionY = y;

            if (_maskIndex != mask_index_next)
            {
                Repaint(mask);
                _maskIndex = mask_index_next;
            }           
            return true;
        }

        return false;
    }


    private List<Vector2> GetNewBricksPosition(ushort maskNext, int dx, int dy)
    {
        List<Vector2> newBricksPositions = new List<Vector2>();
        for (int i = 0; i < bricks.Length; i++)
        {
            bool active = Convert.ToBoolean(maskNext & 1 << i);
            if (active)
            {
                Vector2 partPosition = bricks[i].transform.position;
                partPosition.x += dx;
                partPosition.y += dy;
                newBricksPositions.Add(partPosition);
            }
        }
        return newBricksPositions;
    }

    [ContextMenu("Rotate")]
    private void Rotate()
    {
        _maskIndex = (_maskIndex + 1) % _settings.rotationMasks.Length;
        ushort maskNext = _settings.rotationMasks[_maskIndex];
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
