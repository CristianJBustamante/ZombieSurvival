using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public float objetosMision = 1;
    bool juegoGanado = false;
    public GameObject textoMision;
    
    
    public GameObject zonaFinal;
    public GameObject panel;
    float t = 0f;
    float duration;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeSelf) {
        float value = Mathf.Lerp(0f, 1f, t);
            t += Time.deltaTime / duration;
        panel.GetComponent<Image>().color = new Color(0,0,0,value);
        }

    }

    public void ganarJuego()
    {
        Debug.Log("Has Ganado!!!");
        panel.gameObject.SetActive(true);
    }

    public void cambiarTextoMision() {
        if (objetosMision > 0)
        {
            textoMision.GetComponent<TMPro.TextMeshProUGUI>().text = "Collect " + objetosMision;
        }
        else {
            Destroy(textoMision.transform.GetChild(0).gameObject);
            activarFinal();
            textoMision.GetComponent<TMPro.TextMeshProUGUI>().text = "Return to the tack and scape!.";
        }

    }

    private void activarFinal() {
        zonaFinal.gameObject.SetActive(true);
    }

    
}
