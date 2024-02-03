using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ValidarNumeroIntroducido : MonoBehaviour
{
    private TMP_InputField entradaNumero;
    // Start is called before the first frame update
    void Start()
    {
        entradaNumero = GetComponent<TMP_InputField>();

        
    }

    public void ValidaNumero()
    {
        //Primero intenta convertir el texto introducido a numero
        if(int.TryParse(entradaNumero.text, out int inputValue))
        {
            //Si es un numero, se asegura de que el valor sea entre 1 y 100
            entradaNumero.text = Mathf.Clamp(inputValue, 1, 100).ToString();
        }
        else
        {
            //Si falla la conversión a número lo deja en blanco
            entradaNumero.text = "";
        }
    }
}
