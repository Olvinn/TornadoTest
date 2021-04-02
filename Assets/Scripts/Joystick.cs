using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    public Vector2 axis { get { return _axis; } }

    private float fieldZ = -7;

    [SerializeField]              private Image stick;
    [SerializeField]              private float radius = 3;
    [SerializeField]              private Camera relativeCamera;

    private Vector2 _axis, fieldStart;
    private Image field;
    private int fingerId;

    void Awake()
    {
        field = GetComponent<Image>();
        fingerId = -1;
        fieldZ = transform.position.z;
    }

    void Update()
    {
        //ProcessTouches();
        if (fingerId == -1)
            ProcessClick();
    }

    public void SetVisible(bool value)
    {
        Color goal = field.color;
        goal.a = value ? .75f : 0;
        stick.color = goal;
        goal.a = value ? .25f : 0;
        field.color = goal;
    }

    private void ProcessClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject == gameObject)
            {
                Vector3 fPos = Input.mousePosition;
                Vector3 sPos = Input.mousePosition;
                fieldStart = fPos;
                fPos.z = fieldZ;
                transform.position = fPos;
                stick.transform.position = sPos;
                _axis = Vector2.zero;
                StopAllCoroutines();
                StartCoroutine(FadeIn());
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            fieldStart = Vector3.zero;
            _axis = Vector2.zero;
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
        else if (Input.GetButton("Fire1"))
        {
            if (fieldStart != Vector2.zero)
            {
                Vector3 sPos = Input.mousePosition;
                Vector3 resultFieldPos = fieldStart;
                resultFieldPos.z = fieldZ;
                transform.position = resultFieldPos;

                if ((sPos - transform.position).magnitude > radius)
                {
                    stick.transform.position = transform.position + (sPos - transform.position).normalized * radius;
                }
                else
                {
                    stick.transform.position = sPos;
                }

                _axis = (stick.transform.position - transform.position).normalized * Vector3.Distance(stick.transform.position, transform.position) / radius;
            }
        }
    }

    //private void ProcessTouches()
    //{
    //    foreach (Touch touch in Input.touches)
    //    {
    //        switch (touch.phase)
    //        {
    //            case TouchPhase.Began:
    //                {
    //                    if (fingerId == -1 && (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject == gameObject))
    //                    {
    //                        Vector3 fPos = touch.position;
    //                        Vector3 sPos = touch.position;
    //                        fieldStart = fPos;
    //                        fPos.z = fieldZ;
    //                        transform.position = fPos;
    //                        stick.transform.position = sPos;
    //                        fingerId = touch.fingerId;
    //                        _axis = Vector2.zero;
    //                        StopAllCoroutines();
    //                        StartCoroutine(FadeIn());
    //                    }
    //                    break;
    //                }
    //            case TouchPhase.Canceled:
    //            case TouchPhase.Ended:
    //                {
    //                    if (fingerId != -1 && touch.fingerId == fingerId)
    //                    {
    //                        //stick.transform.position = transform.position;
    //                        fingerId = -1;
    //                        _axis = Vector2.zero;
    //                        StopAllCoroutines();
    //                        StartCoroutine(FadeOut());
    //                    }
    //                    break;
    //                }
    //            case TouchPhase.Moved:
    //            case TouchPhase.Stationary:
    //                {
    //                    if (fingerId != -1 && touch.fingerId == fingerId)
    //                    {
    //                        Vector3 sPos = touch.position;
    //                        Vector3 resultFieldPos = (Vector3)fieldStart;
    //                        resultFieldPos.z = fieldZ;
    //                        transform.position = resultFieldPos;

    //                        _axis = (sPos - transform.position).normalized;

    //                        if ((sPos - transform.position).magnitude > radius)
    //                        {
    //                            stick.transform.position = transform.position + (sPos - transform.position).normalized * radius;
    //                        }
    //                        else
    //                        {
    //                            stick.transform.position = sPos;
    //                        }
    //                    }
    //                    break;
    //                }
    //        }
    //    }
    //    Vector3 pos = stick.transform.position;
    //    pos.z = field.transform.position.z - .1f;
    //    stick.transform.position = pos;
    //}

    private IEnumerator FadeIn()
    {
        Color goal;
        while (stick.color.a < .7f)
        {
            goal = field.color;
            goal.a = .75f;
            stick.color = Color.Lerp(stick.color, goal, Time.unscaledDeltaTime * 20);
            goal.a = .25f;
            field.color = Color.Lerp(field.color, goal, Time.unscaledDeltaTime * 20);
            yield return new WaitForEndOfFrame();
        }
        goal = field.color;
        goal.a = .75f;
        stick.color = goal;
        goal.a = .25f;
        field.color = goal;
    }

    private IEnumerator FadeOut()
    {
        Color goal;
        while (field.color.a > .05f)
        {
            goal = field.color;
            goal.a = 0;
            field.color = Color.Lerp(field.color, goal, Time.unscaledDeltaTime * 5);
            stick.color = field.color;
            yield return new WaitForEndOfFrame();
        }
        goal = field.color;
        goal.a = 0;
        stick.color = goal;
        field.color = goal;
    }
}
