using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedTransparente : MonoBehaviour
{
    public Transform puntoCamara;
    public RaycastHit hitpoint = new RaycastHit();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Linecast(transform.position, puntoCamara.transform.position, out hitpoint)) {
            Debug.DrawLine(transform.position, puntoCamara.transform.position);
        }
    }
}
