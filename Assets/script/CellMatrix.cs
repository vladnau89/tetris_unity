using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class CellMatrix : SingletonMonoBehaviour<CellMatrix>
{
    public static event Action OnLineFilled;

    [Serializable]
    private sealed class Cell
    {
        public readonly int x;
        public readonly int y;
        public bool isFilled { get { return brickPart != null; } }
        public GameObject brickPart;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void ReleaseCell()
        {
            Destroy(brickPart);
            brickPart = null;
        }

        public void MoveDownBrickPart(int delta)
        {
            if (!isFilled) return;
            var pos = brickPart.transform.position;
            pos.y += delta;
            brickPart.transform.position = pos;
        }

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
                cells[i * _width + j] = new Cell(j, -i);
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

    private static bool IsCellAvailable(int x , int y)
    {
        int y_invert = -y;
        if (x < 0 || x >= Width) return false;
        if (y_invert < 0 || y_invert >= Height) return false;
        return Instance.cells[x + (-y * Width)].isFilled == false;
    }

    public static bool IsCellsAvailable(List<Vector2> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            var pos = positions[i];
            if (!IsCellAvailable((int)pos.x, (int)pos.y))
            {
                return false;
            }
        }
        return true;
    }

    public static void FixCell(List<Vector2> positions, List<GameObject> brickParts)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            var pos = positions[i];
            int x = (int)pos.x;
            int y = (int)pos.y;

            var cell = Instance.cells[x + (-y * Width)];
            cell.brickPart = brickParts[i];
        }
    }

    public static void CheckLine()
    {
        int height = Height;
        int width  = Width;
        var cells  = Instance.cells;

        int filledLinesCount = 0;
        int lastFilledLineIndex = 0;

        for (int i = 0; i < height; i++)
        {
            bool isLineFilled = true;
            for (int j = 0; j < width; j++)
            {
                var cell = cells[i * width + j];
                if (cell.isFilled == false)
                {
                    isLineFilled = false;
                    break;
                }
            }

            if (isLineFilled)
            {
                for (int j = 0; j < width; j++)
                {
                    var cell = cells[i * width + j];
                    cell.ReleaseCell();
                }

                if (OnLineFilled != null) OnLineFilled();

                filledLinesCount++;
                lastFilledLineIndex = i;
            }
        }

        MoveDownAllCells(lastFilledLineIndex, filledLinesCount);
    }

    private static void MoveDownAllCells(int lastFilledLineIndex, int filledLinesCount)
    {
        if (filledLinesCount <= 0) return;

        int height = Height;
        int width = Width;
        var cells = Instance.cells;

        for (int i = lastFilledLineIndex; i >= filledLinesCount; i--)
        {
            for (int j = 0; j < width; j++)
            {
                var cell = cells[i * width + j];
                var cell_upper = cells[(i - filledLinesCount) * width + j];

                cell.brickPart = cell_upper.brickPart;
                cell.MoveDownBrickPart(-filledLinesCount);

            }
        }
    }
}
