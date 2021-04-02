using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] InputController _ic;
    [SerializeField] Image _field, _stick;
    [SerializeField] float _radius = 3;

    private Vector2 _dir, _prevDir;

    private void Start()
    {
        if (!_ic)
            Debug.LogWarning($"Joystick ({name}): InputController is empty");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 fPos = eventData.position;
        Vector3 sPos = eventData.position;
        fPos.z = _field.transform.position.z;
        _field.transform.position = fPos;
        _stick.transform.position = sPos;
        _dir = Vector2.zero;
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 sPos = eventData.position;

        if ((sPos - _field.transform.position).magnitude > _radius)
        {
            _stick.transform.position = _field.transform.position + (sPos - _field.transform.position).normalized * _radius;
        }
        else
        {
            _stick.transform.position = sPos;
        }

        _prevDir = _dir;
        _dir = (_stick.transform.position - _field.transform.position) / _radius;

        if (_prevDir != _dir)
            _ic.onDirChanged?.Invoke(_dir);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dir = Vector2.zero;
        StopAllCoroutines();
        StartCoroutine(FadeOut());

        _ic.onDirChanged?.Invoke(Vector2.zero);
    }

    private IEnumerator FadeIn()
    {
        Color goal;
        while (_stick.color.a < .7f)
        {
            goal = _field.color;
            goal.a = .75f;
            _stick.color = Color.Lerp(_stick.color, goal, Time.unscaledDeltaTime * 20);
            goal.a = .25f;
            _field.color = Color.Lerp(_field.color, goal, Time.unscaledDeltaTime * 20);
            yield return new WaitForEndOfFrame();
        }
        goal = _field.color;
        goal.a = .75f;
        _stick.color = goal;
        goal.a = .25f;
        _field.color = goal;
    }

    private IEnumerator FadeOut()
    {
        Color goal;
        while (_field.color.a > .05f)
        {
            goal = _field.color;
            goal.a = 0;
            _field.color = Color.Lerp(_field.color, goal, Time.unscaledDeltaTime * 5);
            _stick.color = _field.color;
            yield return new WaitForEndOfFrame();
        }
        goal = _field.color;
        goal.a = 0;
        _stick.color = goal;
        _field.color = goal;
    }
}
