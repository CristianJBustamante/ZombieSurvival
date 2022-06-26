using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    Animator anim;
    public GameObject jugador;


    public float velocidad;
    public float rotationSpeed;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("aRango") == false) {
            transform.position = Vector3.MoveTowards(transform.position, jugador.transform.position, velocidad * Time.deltaTime);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, jugador.transform.position) < 0.001f)
            {
                // Swap the position of the cylinder.
                jugador.transform.position *= -1.0f;
            }
        }

        //Vector3 direccion = new Vector3(jugador.transform.position.x, 0, jugador.transform.position.z);
        //Quaternion aRotar = Quaternion.LookRotation(direccion, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, aRotar, rotationSpeed * Time.deltaTime);



        //find the vector pointing from our position to the target
        _direction = (jugador.transform.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
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
}
