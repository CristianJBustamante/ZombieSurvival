using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float vidaMaxima = 200;
    public float vida = 200;
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
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
                transform.position = Vector3.MoveTowards(transform.position, jugador.transform.position, velocidad * Time.deltaTime);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, jugador.transform.position) < 0.001f)
                {
                    // Swap the position of the cylinder.
                    jugador.transform.position *= -1.0f;
                }
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
            transform.position = new Vector3(transform.position.x, transform.position.y-0.001f, transform.position.z);
        }

        if (transform.position.y <= -1) {
            descomponiendo = false;
        }


        if (vida <= 0 && isDead==false) {
            morir();
        }

    }

    // ATAQUE ZOMBIE

    private void OnCollisionEnter(Collision collision)
    {
        
        StartCoroutine("dañar");
        if (collision.gameObject.tag == "Jugador") {
            
            anim.SetBool("aRango", true);

        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Jugador")
        {
            
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
        vida = vida - daño;
    }

    public void morir() {
        
        isDead = true;
        this.GetComponent<Rigidbody>().detectCollisions = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        anim.SetBool("isDead", true);
        anim.Play("Zombie Die");
        StartCoroutine("descomponer");
    }

    public float getVida() {
        return vida;
    }

    // MECANICA DE SPAWWN
    public void spawnear() 
    {
        vida = vidaMaxima;
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
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);

    }

    


}
