using UnityEngine;
using System.Collections.Generic;
using System;

public class MazeSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject _cellPrefab;
    GameObject _maze;
    MazeGeneratorCellInfo[,] _generatedMaze;
    Win _win;
    int _width = 8;
    int _height = 8;
    #endregion

    #region Properties
    public int Width => _width;
    public int Height => _height;
    public MazeGeneratorCellInfo[,] GeneratedMaze => _generatedMaze;

    #endregion

    #region Actions
    public Action FinishSpawn;
    #endregion

    #region CoreMethods
    void Start()
    {
        _win = FindObjectOfType<Win>();
        _win.WinLevelel += DeleteMaze;
        _win.StartNewLevel += StartMaze;
        StartMaze();
    }

    void OnDisable()
    {
        _win.WinLevelel -= DeleteMaze;
        _win.StartNewLevel -= StartMaze;
    }
    #endregion

    void StartMaze()
    {
        _maze = new GameObject("Maze");
        MazeGenerator mazeGenerator = new MazeGenerator(_width, _height);
        _generatedMaze = mazeGenerator.GenerateMaze();
        int xLength = _generatedMaze.GetLength(0);
        int zLenght = _generatedMaze.GetLength(1);
        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < xLength; j++)
            {
                Cell cell = Instantiate(_cellPrefab, new Vector3(i, 0, j), Quaternion.identity).GetComponent<Cell>();
                cell.RemoveWalls(_generatedMaze[i, j].ExistWalls);
                _generatedMaze[i, j].AddCell(cell);
                cell.transform.SetParent(_maze.transform);
                if (i == xLength - 1 && j == zLenght - 1) cell.RemoveGround();
            }
        }
        FinishSpawn();
    }

    void DeleteMaze()
    {
        Destroy(_maze);
    }
}
