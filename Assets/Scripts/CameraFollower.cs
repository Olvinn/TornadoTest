using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves camera by position of target
/// </summary>
public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float lerpSpeed = 10;

    private void Start()
    {
        if (!target)
            Debug.LogWarning($"CameraFollower ({name}): target is empty");
    }

    void LateUpdate()
    {
        if (target)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * lerpSpeed);
    }
}
