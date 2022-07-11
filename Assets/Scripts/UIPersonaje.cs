using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPersonaje : MonoBehaviour
{
   
    PlayerMove jugador;

    private Camera camara;

    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        enfocar();
        
    }

    void enfocar() {

        transform.rotation = Quaternion.LookRotation(transform.position - camara.transform.position);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
    }
}
