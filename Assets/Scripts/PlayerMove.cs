using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float runSpeed;
    public float rotationSpeed = 250;

    public Animator animator;

    private float x, y;
    private float xR, zR;

    public Joystick joystickMovimiento;
    public Joystick joystickRotacion;

    public GameObject[] armas;
    public GameObject armaActiva;

    public ParticleSystem fire;

    bool onFire = false;
    bool fireActive = false;


    public AudioClip[] audios;
    public AudioSource controlAudio;


    public float vidaMaxima = 1000;
    public float vidaActual = 1000;

    public bool isDead = false;



    // Start is called before the first frame update
    void Start()
    {
        runSpeed = 3;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false) {
        
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
        
            // CON JOYSTICK
            // Movimiento

            x = joystickMovimiento.Horizontal;
            y = joystickMovimiento.Vertical;

            transform.Translate(x * Time.deltaTime * runSpeed, 0 , 0, Space.World);
            transform.Translate(0, 0, y * Time.deltaTime * runSpeed, Space.World);

            // Rotación

            xR = joystickRotacion.Horizontal;
            zR = joystickRotacion.Vertical;

            Vector3 direccion = new Vector3(xR, 0, zR);

            if (xR != 0 || zR != 0)
            {
                Quaternion aRotar = Quaternion.LookRotation(direccion, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, aRotar, rotationSpeed * Time.deltaTime);
            }

            //transform.Rotate(0, xR * Time.deltaTime * rotationSpeed, 0, Space.Self);
            //transform.Rotate(0, yR * Time.deltaTime * rotationSpeed, 0, Space.Self);



            animator.SetFloat("VelX", x);
            animator.SetFloat("VelY", y);

       

            if (xR != 0f && fireActive == false)
            {
                onFire = true;
            
            }
        
            if (xR == 0f) {
                fireActive = false;
            }
        

            if (onFire) {
                StartCoroutine(disparar());
                fireActive = true;
                onFire = false;
            }

            if (fireActive == false) {
                StopAllCoroutines();
            }

            float rotacion = this.transform.rotation.y;


            if ((rotacion >= 0.45f && rotacion <= 0.85f) || (rotacion <= -0.45f && rotacion >= -0.85f))
            {
                animator.SetBool("Lateral", true);
            }
            else {
                animator.SetBool("Lateral", false);
            }

        }

        if (vidaActual <= 0 && isDead == false)
        {
            morir();
        }


    }

    public void cambiarArma()
    {

        GameObject o1 = GameObject.FindGameObjectWithTag("Rifle");

        if (armas[1].activeSelf == false)
        {
            armas[0].SetActive(false);
            armas[1].SetActive(true);
            armaActiva = armas[1];
        }
        else
        {
            armas[0].SetActive(true);
            armas[1].SetActive(false);
            armaActiva = armas[0];
        }
    }

    IEnumerator disparar() {
        while (true) { 
        disparo();
        yield return new WaitForSeconds(0.1f);
        }
    }
    public void disparo() {
        fire.Play();
        //controlAudio.PlayOneShot(audios[0]);
        armaActiva.GetComponent<Arma>().disparar();
        
    }

    public void recibirDaño(float daño) {
        if (vidaActual >= daño)
        {
            vidaActual = vidaActual - daño;
        }
        else {
            vidaActual = 0;
        }
    }

    public void morir() {
        isDead = true;
        animator.SetBool("Dead", true);
    }
}
