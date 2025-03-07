using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

[RequireComponent (typeof (Rigidbody2D))]
public class StopTime : MonoBehaviour
{
    Rigidbody2D _rb;
    bool isOnAir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        isOnAir = false;
    }

    void Update()
    {
        StopOnAir();

        if (isOnAir) 
        {
            if (Input.GetKeyUp(KeyCode.Space)) 
            {
                isOnAir = false;
            }
        }
    }

    void StopOnAir()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
            isOnAir = true;
        }
    }
}
