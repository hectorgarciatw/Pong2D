using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;
    // Start is called before the first frame update

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = Vector2.left *speed;
    }

    float hitFactor(Vector2 ballPos, Vector2 paddlePos,float paddleHeight) {
        Debug.Log((ballPos.y - paddlePos.y) / paddleHeight);
        return (ballPos.y - paddlePos.y) / paddleHeight;
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Hit the LeftPaddle?
        if (col.gameObject.name == "LeftPaddle") {
            // Calculate hit Factor
            float y = hitFactor(transform.position,col.transform.position,col.collider.bounds.size.y);
            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            rb.velocity = dir * speed;
        }

        // Hit the right RightPaddle?
        if (col.gameObject.name == "RightPaddle") {
            // Calculate hit Factor
            float y = hitFactor(transform.position,col.transform.position,col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set Velocity with dir * speed
            rb.velocity = dir * speed;
        }
    }

}
