using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    
    public float daño;
    public float firerate;

    public GameObject bala;
    public GameObject firepoint;

    public float desviacionEscopeta=200;
    public float perdigones=8;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void disparar() {


            switch (this.gameObject.tag) {

                case "Escopeta":
                    for (int i = 0; i < perdigones; i++)
                    {
                        disparo(getBulletDirection());

                    }
                    break;

                case "Rifle":
                    disparo(firepoint.transform.forward);
                    break;

            }

    }
    private void disparo(Vector3 direcion) {
        GameObject bulletALanzar = PoolDisparos.sharedInstance.GetPool();
        if (bulletALanzar != null)
        {
                    bulletALanzar.SetActive(true);
                    bulletALanzar.transform.position = firepoint.transform.position;
                    bulletALanzar.transform.rotation = Quaternion.identity;
                    bulletALanzar.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bulletALanzar.GetComponent<Rigidbody>().AddForce(direcion * bulletALanzar.GetComponent<Bala>().speed); ;
                    bulletALanzar.GetComponent<Bala>().damage = daño;
                    bulletALanzar.GetComponent<Bala>().desactivacion();
        }
        else
        {
            Debug.Log("No encunetro la bala");
        }
    }

    private Vector3 getBulletDirection() {
        Vector3 direction = new Vector3(
            firepoint.transform.forward.x + Random.Range(-desviacionEscopeta, desviacionEscopeta),
            firepoint.transform.forward.y + Random.Range(-desviacionEscopeta, desviacionEscopeta),
            firepoint.transform.forward.z + Random.Range(-desviacionEscopeta, desviacionEscopeta)
            );
        
        return direction;
    }

}
