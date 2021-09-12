using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool movingRight;
    [SerializeField] GameManager gm;
    [SerializeField] int puntosdevida;
    float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        maxX = esquinaInfDer.x;
        minX = esquinaInfIzq.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight)
        {
            Vector2 movimiento = new Vector2(speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        else
        {
            Vector2 movimiento = new Vector2(-speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        

        if(transform.position.x >= maxX)
        {
            movingRight = false;
        }
        else if(transform.position.x <= minX)
        {
            movingRight = true;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.timeScale < 1)
        {

            if (collision.gameObject.CompareTag("Disparo"))
            {
                gm.ReducirNumEnemigos();
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Disparo"))
            {
                puntosdevida--;
                if (puntosdevida <= 0)
                {
                    Destroy(this.gameObject);
                    gm.ReducirNumEnemigos();
                }
            }
        }
    }
}