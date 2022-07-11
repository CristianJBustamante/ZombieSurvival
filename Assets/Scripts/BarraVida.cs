
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{

    private Image barraVida;
    GameObject personaje;
    float vidaActual;
    float vidaMaxima;
    public Image barraVidaBack;


    // Start is called before the first frame update
    void Start()
    {
        
       barraVida = GetComponent<Image>();
       personaje = this.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;



    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf && barraVidaBack!=null) {

            if (personaje.gameObject.activeSelf) { 
            buscarVidas();
            }
            
            barraVida.fillAmount = vidaActual / vidaMaxima;
            if (barraVida.fillAmount < barraVidaBack.fillAmount)
            {

                barraVidaBack.fillAmount = barraVidaBack.fillAmount - 0.005f;
            }
            if (barraVida.fillAmount > barraVidaBack.fillAmount)
            {

                barraVidaBack.fillAmount = barraVida.fillAmount;
            }
            if (barraVida.fillAmount == 0 && barraVidaBack.fillAmount > 0)
            {
                barraVidaBack.fillAmount = barraVidaBack.fillAmount - 0.015f;
            }
        }
        
    }


    void buscarVidas() {

        switch (personaje.tag)
        {
            
            case "Jugador":
                vidaActual = personaje.GetComponent<PlayerMove>().vidaActual;
                vidaMaxima = personaje.GetComponent<PlayerMove>().vidaMaxima;
                break;
            case "Enemigo":
                vidaActual = personaje.GetComponent<Zombie>().vidaActual;
                vidaMaxima = personaje.GetComponent<Zombie>().vidaMaxima;
                break;

        }
    }
}
