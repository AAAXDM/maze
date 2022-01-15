using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using System;

[RequireComponent(typeof(PathFinder))]
public class PlayerMovement : MonoBehaviour
{
    ParticleSystem _particle;
    PathFinder _pathFinder;
    MazeSpawner _mazeSpawner;
    ObjectsSpawner _objectsSpawner;
    Vector3 _endPos;
    Vector3 _startPoint;
    Vector3 _wayPoint;
    bool _canChangePoint = true;

    public Vector3 EndPos => _wayPoint;
    public Vector3 Startpoint => _startPoint;

    public Action Win;

    void Awake()
    {
        _pathFinder = gameObject.GetComponent<PathFinder>();
        _mazeSpawner = FindObjectOfType<MazeSpawner>();
        _objectsSpawner = FindObjectOfType<ObjectsSpawner>();
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    void OnEnable()
    {
        _objectsSpawner.FinishSpawn += MoveToWinPoint;
    }

    void OnDisable()
    {
        _objectsSpawner.FinishSpawn -= MoveToWinPoint;
    }

    void MoveToWinPoint()
    {
        Invoke(nameof(MoveToPoint), 2);
    }

     async void MoveToPoint()
    {
        _pathFinder.FindPath(_mazeSpawner.GeneratedMaze, _mazeSpawner.Width,_mazeSpawner.Height);
        for (int i = 1; i < _pathFinder.PathPoints.Count; i++)
        {
            _startPoint = new Vector3(_pathFinder.PathPoints[i-1]._x, 0, _pathFinder.PathPoints[i-1]._z);
            _endPos = new Vector3(_pathFinder.PathPoints[i]._x, transform.position.y, _pathFinder.PathPoints[i]._z);
            _wayPoint = new Vector3(_endPos.x, 0, _endPos.z);
            StartCoroutine(Move());
            while (_canChangePoint == false)
            {
                await Task.Yield();
            }
        }
        _particle.Play();
        Win();
        Invoke(nameof(FinishLevel),2);
    }

    IEnumerator Move()
    {
        _canChangePoint = false;
        while(transform.position != _endPos)
        {
          
            transform.position = Vector3.MoveTowards(transform.position, _endPos, 0.01f);
            yield return new WaitForFixedUpdate(); 
        }
        _canChangePoint = true;
    }

    void FinishLevel()
    {
        Destroy(gameObject);
    }
    public void Stop()
    {
        StopAllCoroutines();
        _canChangePoint = true;
    }
}
