using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float vidaMaxima = 100;
    public float vidaActual = 100;
    public float ataque = 100;

    Animator anim;
    public GameObject jugador;


    public float velocidad;
    public float rotationSpeed;


    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    public bool isDead = false;
    public bool isSpawning = false;
    public bool descomponiendo = false;

    GameObject Canvas;
    bool unactiveCanvas = false;

    private NavMeshAgent agente;
    

    // Start is called before the first frame update
    void Start()
    {
        velocidad = 2;
        anim = GetComponent<Animator>();

        // ********** PRUEBA ************
        Canvas = this.transform.GetChild(2).gameObject;

        // ******************************

        agente = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // SEGUIMIENTO JUGADOR
        float dist = Vector3.Distance(jugador.transform.position, transform.position);
        if (jugador.GetComponent<PlayerMove>().isDead == false || dist >= 1.5f)
        {

            if (anim.GetBool("aRango") == false && isDead == false && isSpawning == false)
            {
                //transform.position = Vector3.MoveTowards(transform.position, jugador.transform.position, velocidad * Time.deltaTime);
                agente.destination = jugador.transform.position;

                // Check if the position of the cube and sphere are approximately equal.
                //if (Vector3.Distance(transform.position, jugador.transform.position) < 0.001f)
                //{
                //    // Swap the position of the cylinder.
                //    jugador.transform.position *= -1.0f;
                //}
            }

            if (isDead == false)
            {
                //find the vector pointing from our position to the target
                _direction = (jugador.transform.position - transform.position).normalized;
                //create the rotation we need to be in to look at the target
                _lookRotation = Quaternion.LookRotation(_direction);
                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
            }

        }
        if (jugador.GetComponent<PlayerMove>().isDead == true && dist <= 1.5f)
        {
            anim.SetBool("biting", true);
        
        }


        if (isDead == true && descomponiendo == true) {
            transform.position = new Vector3(transform.position.x, transform.position.y-0.002f, transform.position.z);
        }

        if (transform.position.y <= -2) {
            descomponiendo = false;
        }


        if (vidaActual <= 0 && isDead==false) {
            morir();
            //setEnebled(true);
        }

        if (vidaActual < vidaMaxima) {
            this.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (isDead == true) {
            Canvas.SetActive(false);
        }

        



    }

    // ATAQUE ZOMBIE

    private void OnCollisionEnter(Collision collision)
    {
        
        StartCoroutine("dañar");
        if (collision.gameObject.tag == "Jugador") {
            
            StartCoroutine(reducirvelocidadJugador());
            anim.SetBool("aRango", true);
            Debug.Log(jugador.GetComponent<PlayerMove>().runSpeed);
        }
    }


    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Jugador")
        {
            
            Debug.Log(jugador.GetComponent<PlayerMove>().runSpeed);
            StartCoroutine("pasarCorrer");
        }
    }


    IEnumerator pasarCorrer() {
        yield return new WaitForSeconds(2);
        anim.SetBool("aRango", false);
        StopCoroutine("pasarCorrer");
    }

    IEnumerator dañar() {
        yield return new WaitForSeconds(1.5f);
        float dist = Vector3.Distance(jugador.transform.position, transform.position);
        if (dist <= 1.2f && isDead==false && isSpawning==false) {
            jugador.GetComponent<PlayerMove>().recibirDaño(ataque);
        }
        StopCoroutine("dañar");
    }

    // MUERTE ZOMBIE

    public void reducirVida(float daño) 
    {
        vidaActual = vidaActual - daño;
    }

    public void morir() {
        agente.enabled = false;
        isDead = true;
        this.GetComponent<Rigidbody>().detectCollisions = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        anim.SetBool("isDead", true);
        anim.Play("Zombie Die");
        StartCoroutine("descomponer");
        
    }

    public float getVida() {
        return vidaActual;
    }

    // MECANICA DE SPAWWN
    public void spawnear() 
    {
        vidaActual = vidaMaxima;
        isDead = false;
        isSpawning = true;
        descomponiendo = false;
        anim = GetComponent<Animator>();
        anim.Play("Zombie Spawn");
        StartCoroutine("spawning");
    }

    IEnumerator spawning()

    {
        yield return new WaitForSeconds(3f);
        this.GetComponent<Rigidbody>().detectCollisions = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
        isSpawning = false;
        StopCoroutine("spawning");
    }

    IEnumerator descomponer() {
        
        yield return new WaitForSeconds(8f);
        descomponiendo = true;
        transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
        
    }

    IEnumerator reducirvelocidadJugador()

    {
        float decremento = jugador.GetComponent<PlayerMove>().runSpeed * 0.2f;
        jugador.GetComponent<PlayerMove>().runSpeed = jugador.GetComponent<PlayerMove>().runSpeed - decremento;
        yield return new WaitForSeconds(2f);
        jugador.GetComponent<PlayerMove>().runSpeed = jugador.GetComponent<PlayerMove>().runSpeed + decremento;
        StopCoroutine(reducirvelocidadJugador());
    }














}
