using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocity = 5;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private bool coli = false;
    private int cont = 0;
    
    private float temp = 0;

    private float videnemy = 3;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2(velocity, _rigidbody2D.velocity.y);
        _spriteRenderer.flipX = false;
        if (coli==true)
        {
            _spriteRenderer.flipX = true;
            _rigidbody2D.velocity = new Vector2(velocity*-1, _rigidbody2D.velocity.y);
        }
    }
  

    private void OnTriggerEnter2D(Collider2D col)
    {
        var tag = col.gameObject.tag;
        if (tag == "tope")
        {
            //  Debug.Log("colision");
            coli = true;
            cont += 1;
            if (cont==2)
            {
                coli = false;
                cont = 0;
            }
        }
        if (tag=="bola1")
        {
            videnemy -= 1;
            Debug.Log(videnemy);
            if (videnemy<=0)
            {
                Destroy(this.gameObject);
                
            }
        }
        if (tag=="bola2")
        {
            videnemy -= 2;
            Debug.Log(videnemy);
            if (videnemy<=0)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
