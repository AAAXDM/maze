using System.Collections.Generic;

public class MazeGeneratorCellInfo 
{
    #region Fields
    Cell _cell; 
    int _x;
    int _z;
    bool _leftWallExist = true;
    bool _rightWallExist = true;
    bool _topWallExist = true;
    bool _bottomWallExist = true;
    bool _isVisited = false;
    bool[] _existWalls;
    #endregion

    #region Properties
    public Cell Cell => _cell;
    public int X => _x;
    public int Z => _z;
    public bool IsVisited => _isVisited;
    public IList<bool> ExistWalls => _existWalls;
    #endregion

    #region Construct
    public MazeGeneratorCellInfo(int x, int z)
    {
        _x = x;
        _z = z;
        _existWalls = new bool[] { _leftWallExist, _rightWallExist, _topWallExist, _bottomWallExist };
    }
    #endregion

    #region Support Methods
    public void AddCell(Cell cell)
    {
        _cell = cell;
    }
    public void Visit()
    {
        _isVisited = true;
    }

    public void RemoveWall(int wallNumber)
    {
        _existWalls[wallNumber] = false;
    }
    #endregion
}
