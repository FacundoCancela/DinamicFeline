using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCanvas : MonoBehaviour
{
    public GameObject canvasActual;
    public GameObject canvasSiguiente;

    public void CambiarCanvas()
    {
        canvasActual.SetActive(false);
        canvasSiguiente.SetActive(true);
    }

}
