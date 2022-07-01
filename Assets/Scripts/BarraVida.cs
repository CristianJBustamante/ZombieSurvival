
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{

    private Image barraVida;
    PlayerMove jugador;


    // Start is called before the first frame update
    void Start()
    {
        barraVida = GetComponent<Image>();
        jugador = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.fillAmount = jugador.vidaActual / jugador.vidaMaxima;
    }
}
