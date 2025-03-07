using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStateMachine : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            GameManager.Instance.ChangePlayerState(PLAYER_STATE.FIRE);
        }
    }
}

