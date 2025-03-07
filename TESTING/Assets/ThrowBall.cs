using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [Header("Physics parameters")]
    [SerializeField] float force = 3f;
    [SerializeField] float maxAngle = 180f;
    [SerializeField] float angleChangeSpeed = 50f;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float dragAmount = 1f;

    private Rigidbody2D _rb;  
    private float _currentAngle = 0f;
    private float _minRbVelocity = 0.01f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SelectAngle();
        ApplyForce();
        ApplyDrag();        
    }

    void SelectAngle()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        #region Generic inputs
        if (horizontalInput < 0)
        {
            _currentAngle += angleChangeSpeed * Time.deltaTime;
        }
        else if (horizontalInput > 0)
        {
            _currentAngle -= angleChangeSpeed * Time.deltaTime; 
        }
        #endregion

        _currentAngle = Mathf.Clamp(_currentAngle, 0f, maxAngle);
    }

    bool isMoving()
    {
        return _rb.velocity.magnitude > _minRbVelocity;
    }

    void ApplyForce()
    {
        if (!isMoving())
        {
            if (Input.GetButtonDown("Jump"))
            {

                float throwForce = Mathf.Clamp(Vector2.Distance(transform.position, Vector2.zero), 0, maxDistance) * force;

                Vector2 throwDirection = GetDirection(_currentAngle);

                _rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }
        }
    }

    void ApplyDrag()
    {
        if (_rb.velocity.magnitude > 0)
        {
            _rb.velocity = _rb.velocity * (1f - dragAmount * Time.deltaTime);
        }

        // So it reaches zero completely
        if (_rb.velocity.magnitude < _minRbVelocity)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    Vector2 GetDirection(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector2 throwDirection = GetDirection(_currentAngle);

        float trajectoryLength = maxDistance;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + throwDirection * trajectoryLength);
    }

    void SetGravityScale()
    {
        _rb.gravityScale = 1;
    }
}
