using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    List<Point> _pathPoints = new List<Point>();
    int _xPos = 0;
    int _zPos = 0;

    public IList<Point> PathPoints => _pathPoints;

    public void FindPath(MazeGeneratorCellInfo[,] maze, int width, int height)
    {
        Point point = new Point(_xPos, _zPos);
        _pathPoints.Add(point);
        int i = 0;


        while (_xPos < width - 1 || _zPos < height -1 )
        {
            if (!maze[_xPos, _zPos].ExistWalls[1])
            {
                if (TryToChangePosition(new Point(_xPos + 1, _zPos), _xPos, 1, i, out _xPos))
                {
                    i++;
                    continue;
                }
            }
            if (!maze[_xPos, _zPos].ExistWalls[2])
            {
                if (TryToChangePosition(new Point(_xPos, _zPos + 1), _zPos, 1, i, out _zPos))
                {
                    i++;
                    continue;
                }
            }
            if (!maze[_xPos, _zPos].ExistWalls[0])
            {
                if (TryToChangePosition(new Point(_xPos - 1, _zPos), _xPos, -1, i, out _xPos))
                {
                    i++;
                    continue;
                }
            }
            if(!maze[_xPos, _zPos].ExistWalls[3])
            {
                if (TryToChangePosition(new Point(_xPos, _zPos - 1), _zPos, -1, i, out _zPos))
                {
                    i++;
                    continue;
                }
            }
        }

    }

    bool TryToChangePosition(Point point, int pos, int coef, int iteration, out int position)
    {
        if (iteration > 0)
        {
            if (point._x == _pathPoints[iteration - 1]._x && point._z == _pathPoints[iteration -1]._z )
            {
                position = pos;
                return false;
            }
            else
            {
                position = ChangePosition(pos, coef);
                _pathPoints.Add(point);
                return true;
            }
        }
        else
        {
            position = ChangePosition(pos, coef);
            _pathPoints.Add(point);
            return true;
        }
    }

    int ChangePosition(int pos, int change) => pos + change;  
}

public class Point
{
    public readonly int _x;
    public readonly int _z;

    public Point(int x, int z)
    {
        _x = x;
        _z = z;
    }
}