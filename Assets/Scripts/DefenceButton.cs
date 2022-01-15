using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Button))]
public class DefenceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject _player;
    ObjectsSpawner _objectsSpawner;
    Renderer rend;
    Color _defenceColor = new Color(0.68f, 1, 0.18f);
    Color _defaultColor = new Color(1, 1, 0);
    float _time = 0;
    bool _buttonIsPressed = false;

    public Action ButtonDown;
    public Action ButtonUp;
    void Awake()
    {
        _objectsSpawner = FindObjectOfType<ObjectsSpawner>();
    }

    void Update()
    {
        if(_buttonIsPressed) _time += Time.deltaTime;
        if(_time > 2) ResetDefence();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _buttonIsPressed = true;
        rend.material.color = _defenceColor;
        ButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetDefence();
    }

    void ResetDefence()
    {
        _buttonIsPressed = false;
        _time = 0;
        rend.material.color = _defaultColor;
        ButtonUp();
    }

    void OnEnable()
    {
        _objectsSpawner.FinishSpawn += GetPlayer;
    }

    void OnDisable()
    {
        _objectsSpawner.FinishSpawn -= GetPlayer;
    }

    void GetPlayer()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
        rend = _player.GetComponent<Renderer>();
    }
}
