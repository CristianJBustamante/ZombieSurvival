using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float objetosMision = 1;
    bool juegoGanado = false;
    public GameObject textoMision;
    
    
    public GameObject zonaFinal;
    
    float t = 0f;
    float duration;

    public GameObject camara;
    public GameObject jugador;
    public GameObject tanque;

    public GameObject panelJuego;
    public GameObject panelDerrota;
    public GameObject panelVictoria;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (juegoGanado) { 
            tanque.transform.Translate( 0 , 0, Time.deltaTime * 2, Space.World);
        }
    }

    public void ganarJuego()
    {
        Debug.Log("Has Ganado!!!");
        camara.GetComponent<SeguimientoCamara>().objetivo = tanque;
        Destroy(jugador);
        juegoGanado = true;
        Destroy(tanque.transform.GetChild(4).gameObject);
        panelJuego.SetActive(false);
        panelVictoria.SetActive(true);
        LeanTween.scale(panelVictoria.transform.GetChild(0).gameObject, new Vector3(60, 60, 60), 3f).setEase(LeanTweenType.easeOutElastic);
        //LeanTween.alpha(panelVictoria.transform.GetChild(1).gameObject, 1f, 5f);
        Image r = panelVictoria.transform.GetChild(1).gameObject.GetComponent<Image>();
        LeanTween.value(gameObject, 0, 1, 5).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
    }

    public void perderJuego() {
        panelJuego.SetActive(false);
        panelDerrota.SetActive(true);
        LeanTween.scale(panelDerrota.transform.GetChild(0).gameObject, new Vector3(30, 30, 30), 5f).setEase(LeanTweenType.easeOutElastic);
        //LeanTween.alpha(panelVictoria.transform.GetChild(1).gameObject, 1f, 5f);
        Image r = panelDerrota.transform.GetChild(1).gameObject.GetComponent<Image>();
        LeanTween.value(gameObject, 0, 1, 5).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        });

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

    public void reiniciarJuego() {
        StartCoroutine(cargarEscena());
    }

    IEnumerator cargarEscena() {

        Scene scene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    

    
}
