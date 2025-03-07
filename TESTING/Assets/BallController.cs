using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float maxForce = 5f;  
    [SerializeField] float maxAngle = 180f;
    [SerializeField] float angleChangeSpeed = 50f;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float dragAmount = 1f;

    private Rigidbody2D _rb;
    private float _currentAngle = 0f;
    private float _minRbVelocity = 0.01f;

    private float holdTime = 0f; 
    private float maxHoldTime = 5f; 

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
                // Reset the hold time when the jump button is pressed
                holdTime = 0f;
            }

            if (Input.GetButton("Jump"))
            {
                // Increase the hold time as long as the button is held down
                holdTime += Time.deltaTime;

                // Clamp the hold time to the max value to avoid exceeding max force
                holdTime = Mathf.Clamp(holdTime, 0f, maxHoldTime);
            }

            if (Input.GetButtonUp("Jump"))
            {
                // Calculate the force based on how long the button was held (0 to 5 range)
                float forceAmount = Mathf.Lerp(0, maxForce, holdTime / maxHoldTime); // Lerp to smoothly transition force to max value

                // Calculate the throw direction
                Vector2 throwDirection = GetDirection(_currentAngle);

                // Apply the force to the Rigidbody
                _rb.AddForce(throwDirection * forceAmount, ForceMode2D.Impulse);
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

}
