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
    public void MuestraTexto(string textoAMostrar)
    {
        dialogo.text = "";
        StartCoroutine(EscribeTexto(textoAMostrar));
    }

    //Muestra el texto poco a poco
    IEnumerator EscribeTexto(string texto)
    {
        for(int i = 0; i < texto.Length; i++)
        {
             yield return new WaitForSeconds(0.1f);
             dialogo.text += texto[i];
        }
    }
}
