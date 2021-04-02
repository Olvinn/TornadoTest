using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update all gameobjects that using Dissolve shader with this gameobject position
/// </summary>
public class Dissolver : MonoBehaviour
{
    [SerializeField] Material _mat;
    public float r = 2;

    private void Start()
    {
        if (!_mat)
            Debug.LogWarning($"Dissolver ({name}): material is empty");
    }

    private void Update()
    {
        if (_mat)
        {
            _mat.SetVector("Pos", transform.position);
            _mat.SetFloat("Radius", r);
        }
    }
}
