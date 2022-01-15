using UnityEngine;
using System;

public class Win : MonoBehaviour
{
    ObjectsSpawner _objectsSpawner;
    PlayerMovement _playerMovement;
    BackGroundPannel _pannel;

    public Action WinLevelel;
    public Action StartNewLevel;

    void Awake()
    {
        _objectsSpawner = FindObjectOfType<ObjectsSpawner>();
        _pannel = FindObjectOfType<BackGroundPannel>();
    }

    void OnEnable()
    {
        _objectsSpawner.FinishSpawn += SubscribeToWinEvent;
    }

    void OnDisable()
    {
        _objectsSpawner.FinishSpawn -= SubscribeToWinEvent;
        _playerMovement.Win -= FinishLevel;
    }

    void SubscribeToWinEvent()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerMovement.Win += FinishLevel;
    }

    void FinishLevel()
    {
        Invoke(nameof(ShowPannel), 1.5f);
        Invoke(nameof(WinAction),2.5f);
        Invoke(nameof(ChangeMaze), 4);
    }

    void ShowPannel()
    {
        _pannel.gameObject.SetActive(true);
        _pannel.ShowPanel();
    }
    void WinAction()
    {
        WinLevelel();
    }

    void ChangeMaze()
    {
        StartNewLevel();
        _pannel.ClosePanel();
        _pannel.gameObject.SetActive(false);
    }
}
