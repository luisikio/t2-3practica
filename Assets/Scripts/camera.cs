using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jugador;
       void Start()
       {
           
       }
   
       // Update is called once per frame
       void Update()
       {
           transform.position = jugador.transform.position + new Vector3(0,2,-10);
       }
}
