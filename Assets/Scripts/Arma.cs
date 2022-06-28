using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public int cargador;
    public int reserva;
    public float daño;

    public GameObject bala;
    public GameObject firepoint;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void disparar() {

        GameObject bulletALanzar = PoolDisparos.sharedInstance.GetPool();
        if (bulletALanzar != null)
        {
            bulletALanzar.SetActive(true);
            bulletALanzar.transform.position = firepoint.transform.position;
            bulletALanzar.transform.rotation = Quaternion.identity;
            bulletALanzar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bulletALanzar.GetComponent<Rigidbody>().AddForce(firepoint.transform.forward * bulletALanzar.GetComponent<Bala>().speed);
            bulletALanzar.GetComponent<Bala>().damage = daño;
            bulletALanzar.GetComponent<Bala>().desactivacion();
        }
        else {
            Debug.Log("No encunetro la bala");
        }


        

    }
}
