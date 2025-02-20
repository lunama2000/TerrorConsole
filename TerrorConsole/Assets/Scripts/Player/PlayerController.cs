using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 _vMove;
    Rigidbody2D _rb;

    [SerializeField]
    int _vel;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        GetInputs();
    }


    private void FixedUpdate()
    {
        if (_vMove != Vector2.zero)
        {
            _rb.MovePosition((Vector2)transform.position + (_vMove.normalized * _vel * Time.fixedDeltaTime));
        }
    }

    private void GetInputs()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _vMove.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _vMove.x = -1;
        }
        else
        {
            _vMove.x = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _vMove.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _vMove.y = -1;
        }
        else
        {
            _vMove.y = 0;
        }
    }
}
