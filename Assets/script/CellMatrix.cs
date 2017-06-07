using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellMatrix : SingletonMonoBehaviour<CellMatrix>
{
    [Serializable]
    public class Cell
    {
        public int x;
        public int y;
        public bool isFilled;
    }

    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 20;
    [SerializeField] private Cell[] cells;

    public static int Width     { get { return Instance._width; } }
    public static int Height    { get { return Instance._height; } }

    private void Awake()
    {
        cells = new Cell[_width * _height];

        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                cells[i * _width + j] = new Cell { x = j, y = -i, isFilled = false };

            }
        }

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            var cell = cells[i];

            if (cell.isFilled)
            {
                var oldColor = Gizmos.color;
                Gizmos.color = Color.green;
                Gizmos.DrawCube(new Vector3(cell.x, cell.y, 0), Vector3.one);
                Gizmos.color = oldColor;
            }
            else
            {
                Gizmos.DrawWireCube(new Vector3(cell.x, cell.y, 0), Vector3.one);
            }

           
        }
    }

}
