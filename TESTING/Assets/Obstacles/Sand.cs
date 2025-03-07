using UnityEngine;

public class Sand : MonoBehaviour
{
    float OriginalDrag;

    private void OnTriggerEnter2D(Collider2D collision)
    {  
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

}
