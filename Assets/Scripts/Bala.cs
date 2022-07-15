using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float damage;
    //public float speed = 960f;
    public float speed = 2000f;
    public float ttl = 2f;
    public GameObject trail;
    public GameObject firepoint;

    // Start is called before the first frame update
    void Start()
    {
        trail = this.gameObject.transform.GetChild(0).gameObject;
        speed = 1000f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void desactivacion()
    {
        StartCoroutine(desactivar());
    }

    IEnumerator desactivar()
    {
        yield return new WaitForSeconds(ttl);
        StopCoroutine(desactivar());
        StartCoroutine(volverAPosInicial());
    }

    private void OnTrigerEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponent<Zombie>().reducirVida(damage);
            StopCoroutine(desactivar());
            StartCoroutine(volverAPosInicial());
            GameObject sangre = transform.GetChild(1).gameObject;
            sangre.transform.rotation = transform.rotation;
            sangre.GetComponent<ParticleSystem>().Play();
            Vector3 dir = transform.position - other.transform.position;
            dir = dir.normalized;
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir * 300);
        }
        else {
            if(other.gameObject.tag != "Bala")
            { 
                StopCoroutine(desactivar());
                StartCoroutine(volverAPosInicial());
            }
        }
    }

    IEnumerator volverAPosInicial()
    {
        trail.gameObject.GetComponent<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        transform.position = firepoint.transform.position;
        this.gameObject.SetActive(false);
        StopCoroutine(volverAPosInicial());
    }

}
