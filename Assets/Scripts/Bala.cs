using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

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
}
