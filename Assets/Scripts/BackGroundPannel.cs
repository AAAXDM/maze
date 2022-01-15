using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackGroundPannel : MonoBehaviour
{
    Image _image;
    Color _color = new Color(0, 0, 0, 0);
    float _transparencyStep = 0.05f;
    float _imageTransparency;
    int _maxTransperency = 1;
    
    void Awake()
    {
        _image = gameObject.GetComponent<Image>();
        _image.color = _color;
        _imageTransparency = 0;
    }

    public void ShowPanel()
    {
        StartCoroutine(FadeINRoutine());
    }

    IEnumerator FadeINRoutine()
    {
        float stepTime = 0.05f;
        while (_imageTransparency < _maxTransperency)
        {
            _imageTransparency += _transparencyStep;
            _color.a = _imageTransparency;
            _image.color = _color;
            yield return new WaitForSecondsRealtime(stepTime);
        }
        yield return null;
    }

    public void ClosePanel()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        float stepTime = 0.1f;
        while (_imageTransparency > 0)
        {
            _imageTransparency -= _transparencyStep;
            _color.a = _imageTransparency;
            _image.color = _color;
            yield return new WaitForSecondsRealtime(stepTime);
        }
        yield return null;
    }
}
