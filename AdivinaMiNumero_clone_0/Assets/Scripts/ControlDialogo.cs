using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlDialogo : MonoBehaviour
{
    //Cuadro de diálogo
    [SerializeField] private TMP_Text dialogo;


    /// <summary>
    /// Muestra un texto en el cuadro de diálogo
    /// </summary>
    /// <param name="textoAMostrar"></param>
    public IEnumerator MuestraTexto(string textoAMostrar)
    {
        dialogo.text = "";
        yield return StartCoroutine(EscribeTexto(textoAMostrar));//Lanza la corutina EscribeTexto y espera a que finalice        
    }

    //Muestra los caracteres del texto con un intervalo de tiempo
    IEnumerator EscribeTexto(string texto)
    {
        for(int i = 0; i < texto.Length; i++)
        {
             yield return new WaitForSeconds(0.025f);
             dialogo.text += texto[i];
        }
    }
}
