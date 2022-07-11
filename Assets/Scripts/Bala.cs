using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float damage;
    public float speed = 960f;
    public float ttl = 2f;

    // Start is called before the first frame update
    void Start()
    {
       
        
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
        this.gameObject.SetActive(false);
        StopCoroutine(desactivar());


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
            this.gameObject.SetActive(false);
        }
        else {
            if(other.gameObject.tag != "Bala")
            { 
            StopCoroutine(desactivar());
            this.gameObject.SetActive(false);
            }
        }
    }
}
