using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerDeath : MonoBehaviour
{
    ObjectsSpawner _objectsSpawner;
    DefenceButton _defenceButton;
    BoxCollider _boxCollider;
    bool _trigger = true;

    void Awake()
    {
        _objectsSpawner = FindObjectOfType<ObjectsSpawner>();
        _defenceButton = FindObjectOfType<DefenceButton>();
        _boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    void OnEnable()
    {
        _defenceButton.ButtonDown += NotDie;
        _defenceButton.ButtonUp += CanDie;
    }

    void OnDisable()
    {
        _defenceButton.ButtonDown -= NotDie;
        _defenceButton.ButtonUp -= CanDie;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_trigger)
        {
            _trigger = false;
            _objectsSpawner.SpawnPlayer();
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        _trigger = true;
    }

    void NotDie()
    {
        _boxCollider.isTrigger = false;
    }

    void CanDie()
    {
        _boxCollider.isTrigger = true;
    }
}
