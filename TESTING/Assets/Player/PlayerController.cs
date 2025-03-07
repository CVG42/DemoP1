using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxForce = 20f; 
    public float forceIncreaseRate = 5f; 
    public float throwAngle = 0f; 
    public float force = 0f; 
    public int trajectoryPoints = 20; 
    private bool isCharging = false; 
    private float chargeTime = 0f; 
    private Rigidbody2D rb; 
    private LineRenderer lineRenderer; 
    public float gravity = -9.81f;
    private float _minRbVelocity = 1f;
    //public float groundDragCoefficient = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = trajectoryPoints;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        HandleInput();
        if (isCharging)
        {
            UpdateTrajectory();
        }

        
    }

    bool isMoving()
    {
        return rb.velocity.magnitude > _minRbVelocity;
    }

    /*
    void ApplyGroundDrag()
    {
        if (IsGrounded() && rb.velocity.magnitude > 0.1f)
        {
            rb.drag = groundDragCoefficient;
        }
        else
        {
            rb.drag = 0f;
        }

        if(rb.velocity.magnitude < .5f)
        {
            rb.velocity = Vector2.zero;
        }
    }

    bool IsGrounded()
    {
        // Check if the ball is on the ground using a small raycast or collision check
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f); // Adjust the 0.1f as needed for the ball's radius
    }
    */

    void HandleInput()
    {
        if(isMoving()) return;

        Vector2 joystickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (joystickInput.magnitude > 0.1f)
        {
            throwAngle = Mathf.Atan2(joystickInput.y, joystickInput.x) * Mathf.Rad2Deg; 
        }

        if (Input.GetButton("Jump")) 
        {
            if (!isCharging)
            {
                //rb.drag = 0f;
                isCharging = true;
                chargeTime = 0f;
                lineRenderer.enabled = true;
            }

            chargeTime += Time.deltaTime;
            force = Mathf.Min(chargeTime * forceIncreaseRate, maxForce); 
        }
        else if (isCharging)
        {
            isCharging = false;
            lineRenderer.enabled = false;

            ThrowBall();
        }
    }

    void ThrowBall()
    {
        Vector2 throwDirection = new Vector2(Mathf.Cos(throwAngle * Mathf.Deg2Rad), Mathf.Sin(throwAngle * Mathf.Deg2Rad));

        rb.AddForce(throwDirection * force, ForceMode2D.Impulse);

        force = 0f;
        GameManager.Instance.strokesAmount++;
    }

    void UpdateTrajectory()
    {
        Vector3[] trajectory = new Vector3[trajectoryPoints];
        Vector2 startPosition = transform.position;

        Vector2 throwDirection = new Vector2(Mathf.Cos(throwAngle * Mathf.Deg2Rad), Mathf.Sin(throwAngle * Mathf.Deg2Rad));

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * 0.1f;
            float x = startPosition.x + throwDirection.x * force * time;
            float y = startPosition.y + throwDirection.y * force * time + 0.5f * gravity * time * time;

            trajectory[i] = new Vector3(x, y, 0f); 
        }

        lineRenderer.SetPositions(trajectory);
    }
}
