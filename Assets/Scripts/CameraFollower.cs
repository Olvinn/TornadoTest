using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Vector3 offset;
    [SerializeField] Transform _target;
    Transform _camera;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + offset, Time.deltaTime * 10);
    }
}
