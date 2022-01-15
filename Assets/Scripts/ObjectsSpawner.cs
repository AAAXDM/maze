using UnityEngine;
using System;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _winZone;
    MazeSpawner _mazeSpawner;
    float y = 0.3f;

    public Action FinishSpawn;
    void Awake()
    {
        _mazeSpawner = FindObjectOfType<MazeSpawner>();       
    }

    void OnEnable()
    {
        _mazeSpawner.FinishSpawn += SpawnPlayer;
        _mazeSpawner.FinishSpawn += SpawnWinZone;
    }

    void OnDisable()
    {
        _mazeSpawner.FinishSpawn -= SpawnPlayer;
        _mazeSpawner.FinishSpawn -= SpawnWinZone;
    }

    public void SpawnPlayer()
    {
        Instantiate(_player, new Vector3(0, y, 0), Quaternion.identity);
        FinishSpawn();
    }

    void SpawnWinZone()
    {
        Instantiate(_winZone, new Vector3(_mazeSpawner.Width-1, 0, _mazeSpawner.Height-1), Quaternion.identity);
    }
}
