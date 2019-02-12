using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightPaddle : MonoBehaviour
{
    private float verticalDir;
    public float speed;
    private float offset = 1f;
    private Vector2 pos;
    private Vector2 topRight;
    private Vector2 topLeft;
    private Rigidbody2D rb;
    public string axis = "Vertical";

    void Start()
    {   
        speed = 5;  
        //Fijo la posición del Paddle izquierdo en el lugar correcto
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        pos = new Vector2(topRight.x - offset,0);
        transform.position = pos;
        rb = GetComponent<Rigidbody2D>();
        //Desactivo la gravedad para que el Paddle no se caiga
        rb.gravityScale = 0.0f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        verticalDir = Input.GetAxisRaw(axis);
        rb.velocity = new Vector2(0, verticalDir) * speed;
    }
}
