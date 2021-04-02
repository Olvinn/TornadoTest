using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float speed;

    [SerializeField] Joystick joystick;
    CharacterController _cc;
    Vector3 move;
    float movY;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        move = new Vector3();
    }

    void Update()
    {
        MovementProcessing();
        FallingProcessing();

        _cc.Move(move);
    }

    void MovementProcessing()
    {
        float h = joystick.axis.x;
        float v = joystick.axis.y;

        Vector3 newMove = transform.localToWorldMatrix * new Vector3(h, 0, v);
        newMove.y = 0;
        float y = move.y;
        move = newMove * speed * Time.deltaTime;
        move.y = y;
    }

    void FallingProcessing()
    {
        if (_cc.isGrounded)
        {
            movY = -.01f;
        }
        else
            movY -= Physics.gravity.magnitude * Time.deltaTime;
        move.y = movY * Time.deltaTime;
    }
}
