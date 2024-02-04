using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script que controla las acciones del panel del jugador

public class AccionesJugador : MonoBehaviour
{
       [SerializeField] private GameObject  botonEnviar;
       [SerializeField] private TMP_InputField cuadroTexto;

    

    public void ActivaCuadroTexto()
    {
        cuadroTexto.readOnly = false;;
    }

    public void DesactivaCuadroTexto()
    {
        cuadroTexto.readOnly = true;
    }

    public void ActivaBotonEnviar()
    {
        botonEnviar.gameObject.SetActive(true);
    }

    public void DesactivaBotonEnviar()
    {
        botonEnviar.gameObject.SetActive(false);
    }    
   
}
