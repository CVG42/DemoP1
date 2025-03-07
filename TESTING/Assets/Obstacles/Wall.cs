using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private BoxCollider2D _boxCollider;

    public bool isNormal;


    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        isNormal = true;
        GameManager.Instance.onPlayerStateChanged += OnPlayerStateChanged;
    }

    void Update()
    {
        if (!isNormal)
        {
            _boxCollider.isTrigger = true;
        }
    }
    void OnPlayerStateChanged(PLAYER_STATE _gs)
    {
        isNormal = _gs == PLAYER_STATE.NORMAL;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
