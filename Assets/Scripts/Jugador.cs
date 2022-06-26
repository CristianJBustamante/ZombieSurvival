using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float rotationSpeed = 250;
    public Joystick joystickRotacion;
    private float xR, yR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        xR = joystickRotacion.Horizontal;

        transform.Rotate(0, xR * Time.deltaTime * rotationSpeed, 0, Space.Self);

    }

    
}
