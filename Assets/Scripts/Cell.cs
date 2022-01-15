using UnityEngine;
using System.Collections.Generic;

public class Cell : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject[] _walls;
    [SerializeField] GameObject _ground;
    #endregion

    #region Properties
    public GameObject[] Walls => _walls;
    #endregion

    #region Support Methods
    public void RemoveWalls(IList<bool> activeWalls)
    {
        for(int i = 0; i < activeWalls.Count; i ++)
        {
            _walls[i].SetActive(activeWalls[i]);
        }
    }

    public void RemoveGround()
    {
        _ground.SetActive(false);
    }

    public void AddGround()
    {
        _ground.SetActive(true);
    }
    #endregion
}
