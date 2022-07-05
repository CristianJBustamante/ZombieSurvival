using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{

    public GameObject objetivo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posObjetivo = new Vector3(objetivo.transform.position.x, objetivo.transform.position.y + 10, objetivo.transform.position.z -2);
        transform.position = Vector3.Lerp(transform.position, posObjetivo, Time.deltaTime * 2);

    }
}
