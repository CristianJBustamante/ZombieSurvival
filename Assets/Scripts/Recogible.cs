using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recogible : MonoBehaviour
{

    public GameObject gameManager;
    public float rotationSpeed = 5;
    public GameObject otraArma;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = new Vector3(0, Time.deltaTime * rotationSpeed, 0, Space.Self);
        //Vector3 direcion = new Vector3(0, Time.deltaTime * rotationSpeed, 0);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, direcion, rotationSpeed * Time.deltaTime);
        transform.Rotate(0, 0.2f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jugador" && this.gameObject.tag == "Barril") {
            gameManager.GetComponent<GameManager>().objetosMision--;
            gameManager.GetComponent<GameManager>().cambiarTextoMision();
            Destroy(transform.GetChild(0).gameObject,0);
            Destroy(this, 0);
        }
        if (other.gameObject.tag == "Jugador" && this.gameObject.tag != "Barril") {
            other.gameObject.GetComponent<PlayerMove>().cambiarArma();
            otraArma.SetActive(true);
            this.gameObject.SetActive(false);
        }
        

    }
}
