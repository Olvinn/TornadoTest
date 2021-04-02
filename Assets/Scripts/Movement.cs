using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float speed;

    [SerializeField] InputController _ic;
    CharacterController _cc;
    Vector3 _move;
    float _movY;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _move = new Vector3();

        if (!_ic)
            Debug.LogWarning($"Movement ({name}): InputController is empty");
        else
            _ic.onDirChanged.AddListener(MovementProcessing);
    }

    void FixedUpdate()
    {
        FallingProcessing();

        _cc.Move(_move);
    }

    void MovementProcessing(Vector2 dir)
    {
        if (dir.magnitude > 1)
            dir.Normalize();

        float h = dir.x;
        float v = dir.y;

        Vector3 newMove = transform.localToWorldMatrix * new Vector3(h, 0, v);
        newMove.y = 0;
        float y = _move.y;
        _move = newMove * speed * Time.fixedDeltaTime;
        _move.y = y;
    }

    void FallingProcessing()
    {
        if (_cc.isGrounded)
            _movY = -.01f;
        else
            _movY -= Physics.gravity.magnitude * Time.deltaTime;
        _move.y = _movY * Time.deltaTime;
    }
}
