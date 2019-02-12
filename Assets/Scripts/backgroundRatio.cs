using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundRatio : MonoBehaviour
{

    void Awake(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //orthographicSize devuelve mitad de la altura de la camara en unidades de unity (no pixeles)
        float cameraHeight = Camera.main.orthographicSize * 2;
        //Basada en la formula ratio = width / height obtengo el width
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;   

        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y) { // Landscape 
            scale *= cameraSize.x / spriteSize.x;
        } else { // Portrait
            scale *= cameraSize.y / spriteSize.y;
        }
        
        transform.position = Vector2.zero;
        transform.localScale = scale;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
