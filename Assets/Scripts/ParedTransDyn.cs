using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedTransDyn : MonoBehaviour
{

    private ParedTransparente paredtransparenteScript;
    private Renderer renderMaterial = new Renderer();

    // Start is called before the first frame update
    void Start()
    {
        GameObject transp = GameObject.Find("Main Camera");
        paredtransparenteScript = transp.GetComponent<ParedTransparente>();

        renderMaterial = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < renderMaterial.materials.Length; i++) {
            if (paredtransparenteScript.hitpoint.transform == transform) { 
                if (renderMaterial.materials[i].color.a > 0.4f) {
                    Color cor = renderMaterial.materials[i].color;
                    cor.a -= 0.02f;
                    renderMaterial.materials[i].color = cor;
                }
            }
            else if (renderMaterial.materials[i].color.a < 1)
            {

                Color cor = renderMaterial.materials[i].color;
                cor.a += 0.02f;
                renderMaterial.materials[i].color = cor;
            }
        
        }
    }
}
