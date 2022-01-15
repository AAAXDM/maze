using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    #region Fields
    int _width;
    int _height;
    #endregion

    #region Construct
    public MazeGenerator(int width, int height )
    {
        _width = width;
        _height = height;
    }
    #endregion

    #region Support Methods
    public MazeGeneratorCellInfo[,] GenerateMaze()
    {
        MazeGeneratorCellInfo[,] maze = new MazeGeneratorCellInfo[_width, _height];

        for (int i = 0; i < maze.GetLength(0); i ++)
        {
            for(int j = 0; j < maze.GetLength(1); j ++)
            {
                maze[i, j] = new MazeGeneratorCellInfo(i, j);
            }
        }

        RemoveWallsWithBackTracker(maze);

        return maze;
    }

    void RemoveWallsWithBackTracker(MazeGeneratorCellInfo[,] maze)
    {
        MazeGeneratorCellInfo current = maze[0, 0];
        Stack<MazeGeneratorCellInfo> stack = new Stack<MazeGeneratorCellInfo>();
        current.Visit();
        stack.Push(current);

        while(stack.Count > 0)
        {
            List<MazeGeneratorCellInfo> unvisitedNeighbours = new List<MazeGeneratorCellInfo>();
            int x = current.X;
            int z = current.Z;

            if (x > 0 && !maze[x - 1, z].IsVisited) unvisitedNeighbours.Add(maze[x - 1, z]);
            if (z > 0 && !maze[x, z - 1].IsVisited) unvisitedNeighbours.Add(maze[x, z - 1]);
            if (x < _width - 1 && !maze[x + 1, z].IsVisited) unvisitedNeighbours.Add(maze[x + 1, z]);
            if (z < _height - 1 && !maze[x, z + 1].IsVisited) unvisitedNeighbours.Add(maze[x, z + 1]);

            if(unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCellInfo chozen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWalls(current, chozen);
                chozen.Visit();
                stack.Push(chozen);
                current = chozen;
            }

            else
            {
               current = stack.Pop();
            }
        }
    }

    void RemoveWalls(MazeGeneratorCellInfo first, MazeGeneratorCellInfo second)
    {
        if(first.X == second.X)
        {
            if(first.Z > second.Z) 
            {
                RemoveChosenWalls(first, second, 3, 2);
            }
            else
            {
                RemoveChosenWalls(first, second, 2, 3);
            }
        }
        else
        {
            if(first.X > second.X)
            {
                RemoveChosenWalls(first, second, 0, 1);
            }
            else
            {
                RemoveChosenWalls(first, second, 1, 0);
            }
        }
    }

    void RemoveChosenWalls(MazeGeneratorCellInfo first, MazeGeneratorCellInfo second, int firstNumber, int secondNumber)
    {
        first.RemoveWall(firstNumber);
        second.RemoveWall(secondNumber);
    }
    #endregion
}
