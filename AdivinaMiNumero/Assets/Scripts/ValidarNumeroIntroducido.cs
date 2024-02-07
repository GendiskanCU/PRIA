using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ValidarNumeroIntroducido : MonoBehaviour
{
    private TMP_InputField entradaNumero;
    // Start is called before the first frame update
    void Start()
    {
        entradaNumero = GetComponent<TMP_InputField>();
        //Inicialmente, el recuadro se rellenará con un número por defecto aunque el usuario lo podrá cambiar
        entradaNumero.text = Random.Range(1, 101).ToString();
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
            //Si falla la conversión a número genera un aleatorio entre 1 y 100 y lo introduce en sustitución de lo escrito por el usuario
            entradaNumero.text = Random.Range(1, 101).ToString();
        }      
        
    }
}
