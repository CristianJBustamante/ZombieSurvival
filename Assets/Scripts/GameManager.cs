using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float objetosMision = 10;
    bool juegoGanado = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (juegoGanado == false && objetosMision == 0) {
            ganarJuego();
        }
    }

    private void ganarJuego() 
    {
        Debug.Log("Has Ganado!!!");
    }
    
}
