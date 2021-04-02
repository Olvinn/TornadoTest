using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolving : MonoBehaviour
{
    [SerializeField] Material mat;
    public float r = 2;

    private void Update()
    {
        mat.SetVector("Pos", transform.position);
        mat.SetFloat("Radius", r);
    }
}
