using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour{

    public float speed = 1f;
    private Rigidbody2D rb;
    private Vector2 centerPos;
    public AudioClip pointClip;
    public AudioClip bounceClip;
    public AudioClip spawnClip;
    public TextMeshProUGUI player1PointsText;
    public TextMeshProUGUI player2PointsText;

    private int player1Points;
    private int player2Points;
    private AudioSource audio;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    void Start(){   
        //Oculto la pelota hasta que comience el juego
        GetComponent<Renderer>().enabled = false;
        player1Points = 0;
        player2Points = 0;
        RefreshPoints(player1Points,player2Points);
        StartCoroutine(spawnBall(1, 3f));
    }

    //Determinación del ángulo de rebote similar al juego original
    float hitFactor(Vector2 ballPos, Vector2 paddlePos,float paddleHeight){
        return (ballPos.y - paddlePos.y) / paddleHeight;
    }

    IEnumerator spawnBall(int dir, float delayTime){      
        yield return new WaitForSeconds(delayTime);
        GetComponent<Renderer>().enabled = true;
        centerPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2,Screen.height/2));
        transform.position = centerPos;
        audio.PlayOneShot(spawnClip);
        if(dir == 1){
            rb.velocity = Vector2.right *speed;
        }
        else if(dir == -1){
            rb.velocity = Vector2.left *speed;
        }
    }

    void RefreshPoints(int player1Points,int player2Points){
        player1PointsText.text = player1Points.ToString();
        player2PointsText.text = player2Points.ToString();
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.name == "LeftPaddle") {
            audio.PlayOneShot(bounceClip);
            // Calculo el angulo de rebote
            float y = hitFactor(transform.position,col.transform.position,col.collider.bounds.size.y);
            // Me quedo con el vector unitario utilizando la normalizada
            Vector2 dir = new Vector2(1, y).normalized;
            rb.velocity = dir * speed;
        }

        if (col.gameObject.name == "RightPaddle") {
            audio.PlayOneShot(bounceClip);
            // Calculo el angulo de rebote
            float y = hitFactor(transform.position,col.transform.position,col.collider.bounds.size.y);
            // Me quedo con el vector unitario utilizando la normalizada
            Vector2 dir = new Vector2(-1, y).normalized;
            rb.velocity = dir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Punto ganado para el player 2
        if(other.gameObject.tag == "LeftWall"){
            audio.PlayOneShot(pointClip);
            RefreshPoints(player1Points,++player2Points);
            StartCoroutine(spawnBall(-1, 3f));

        }
        //Punto ganado para el player 1
        else if(other.gameObject.tag == "RightWall"){
            audio.PlayOneShot(pointClip);
            RefreshPoints(++player1Points,player2Points);
            StartCoroutine(spawnBall(1, 3f));
        }
    }

    private void Update(){
        if(player1Points > 999 || player2Points > 999){
            //Cargo la escena de GameOver (para volver a iniciar el juego)
            SceneManager.LoadScene(2);
        }
    }
}
