using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneSpawner : MonoBehaviour
{
    [SerializeField] GameObject _deathZone;
    MazeSpawner _mazeSpawner;
    PlayerMovement _playerMovement;
    GameObject _deathZoneObject;
    float _changeTime = 2f;
    bool _canInstantiate = true;
    MazeGeneratorCellInfo[,] _maze;

    void Awake()
    {
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
    }

    void OnEnable()
    {
        _mazeSpawner.FinishSpawn += InstantiateDeathZone;
    }

    void OnDisable()
    {
        _mazeSpawner.FinishSpawn -= InstantiateDeathZone;
        _playerMovement.Win -= StopInstantiate;
    }

    void InstantiateDeathZone()
    {
        _canInstantiate = true;
        Invoke(nameof(InstantiateZones),2);
    }

    void InstantiateZones()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _maze = _mazeSpawner.GeneratedMaze;
        _playerMovement.Win += StopInstantiate;
        StartCoroutine(InstantiateDeathZoneRouitine());
    }

    void StopInstantiate()
    {
        StopAllCoroutines();
        _canInstantiate = false;
        Destroy(_deathZoneObject);
    }

    IEnumerator InstantiateDeathZoneRouitine()
    {
        int x;
        int z;

        while(_canInstantiate)
        {
            x = Random.Range(0, _mazeSpawner.Width - 1);
            z = Random.Range(0, _mazeSpawner.Height - 1);
            Vector3 pos = new Vector3(x, 0, z);
            if(pos == _playerMovement.EndPos || pos == _playerMovement.Startpoint)
            {
                while (pos == _playerMovement.EndPos || pos == _playerMovement.Startpoint)
                {
                    x = Random.Range(0, _mazeSpawner.Width - 1);
                    z = Random.Range(0, _mazeSpawner.Height - 1);
                    pos = new Vector3(x, 0, z);
                }
            }
            _deathZoneObject =  Instantiate(_deathZone, pos, Quaternion.identity);
            _maze[x,z].Cell.RemoveGround(); 
            yield return new WaitForSeconds(_changeTime);
            _maze[x, z].Cell.AddGround();
            Destroy(_deathZoneObject);
        }
    }
}
