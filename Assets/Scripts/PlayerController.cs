using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
   private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _renderer;
    private Animator _animator;
   
    
    public float JumpForce = 10;
    public float velocity = 10;
    
    public Text Vidas;
    public Text LLaves;
    public Text Enemigos;
    
   
    public int vida = 3;
    public int llave = 0;
    public int enemigo = 8;

    public float temp = 0;
 
    public GameObject bulletPrefabs;
    public GameObject bulletPrefabs2;
    
    private static readonly int right = 1;
    private static readonly int left = -1;
    
    private static readonly int Animation_idle = 0;
    private static readonly int Animation_run = 1;
    private static readonly int Animation_jump = 2;
    private static readonly int Animation_dead = 3;
    private static readonly int Animation_runshoot = 6;
    private static readonly int Animation_shoot = 4;
    private static readonly int Animation_slide = 5;
    private static readonly int fin = 7;

    
    private float time = 0f;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
            Vidas.text="VIDAS: "+vida;
            LLaves.text = "LLAVES: "+llave;
            Enemigos.text = "ENEMIGOS: "+enemigo;

           
        
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                ChangeAnimation(Animation_idle);
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Desplazarse(right);

                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Desplazarse(left);
                }
                if(Input.GetKeyUp(KeyCode.Space))
                {
                    _rigidbody2D.AddForce(Vector2.up*JumpForce,ForceMode2D.Impulse);
                    ChangeAnimation(Animation_jump);
                }
                if (Input.GetKeyUp(KeyCode.C))
                {
                    ChangeAnimation(Animation_slide);
                }
                if (Input.GetKeyUp(KeyCode.X))
                { 
                    Disparar();
                    ChangeAnimation(Animation_shoot);
                }
                timekey();
    
    }
    void timekey()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            time = Time.time;

        }
         
        if(Input.GetKeyUp(KeyCode.X))
        {
            time = Time.time - time;
            Debug.Log("Pressed for : " + time + " Seconds");
            if (time >= 2f)
            {
                ChangeAnimation(Animation_shoot);
                Disparar2();
            }

        }
    }
    
    private void Disparar()
    {
        //crear elementos en tiempo de ejecuccion
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var bullgo=Instantiate(bulletPrefabs,new Vector2(x,y),Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<BolaController>();
        
       controller.SetController(this);
        
        if (_renderer.flipX)
        {
            
            controller.velocity = controller.velocity * -1;
        }
    }
    private void Disparar2()
    {
        //crear elementos en tiempo de ejecuccion
        var x = this.transform.position.x;
        var y = this.transform.position.y;

        var bullgo=Instantiate(bulletPrefabs2,new Vector2(x,y),Quaternion.identity) as GameObject;
        var controller = bullgo.GetComponent<BolaController>();
        
        controller.SetController(this);
        
        if (_renderer.flipX)
        {
            
            controller.velocity = controller.velocity * -1;
        }
    }
    private void Desplazarse(int position)
    {
        _rigidbody2D.velocity = new Vector2(velocity * position, _rigidbody2D.velocity.y);
        _renderer.flipX = position == left;
        ChangeAnimation(Animation_run);
    }
    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger("Estado",animation);
    }
    public void Puntaje(int puntos)
    {
        //Puntuacion += puntos;
        
        //Debug.Log(Puntuacion);
    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        var tag = col.gameObject.tag;
        if (tag == "enemy")
        {
            vida -= 1;
           // Debug.Log(vida);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var tag = col.gameObject.tag;
        if (tag == "llave")
        {
            llave += 1;
            Destroy(col.gameObject);
            if (llave==2)
            {
                SceneManager.LoadScene(1);
            }
            

        }
    }
}
