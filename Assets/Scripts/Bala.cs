using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float damage;
    public float speed = 240f;
    public float ttl = 1f;

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
        StopCoroutine(desactivar());
        yield return new WaitForSeconds(ttl);
        this.gameObject.SetActive(false);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo")) {
            collision.gameObject.GetComponent<Zombie>().reducirVida(damage);
            Debug.Log(collision.gameObject.GetComponent<Zombie>().getVida());
            StopCoroutine(desactivar());
            this.gameObject.SetActive(false);
        }
    }
}
