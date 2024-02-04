using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script que conterola las acciones del panel del anfitrión

public class AccionesAnfitrion : MonoBehaviour
{
    [SerializeField] private GameObject botonIniciar, botonMayor, botonMenor, botonAcierto;
    [SerializeField] private TMP_InputField cuadroTexto;
   
    private ControlDialogo dialogo;//Para controlar los textos que van a mostrarse


    private void Start() {
        //Captura el script de control de diálogo del anfitrión
        dialogo = transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();
    }

    public void ActivaCuadroTexto()
    {
        cuadroTexto.readOnly = false;
    }

    public void DesactivaCuadroTexto()
    {
        cuadroTexto.readOnly = true;
    }

    public void ActivaBotonIniciar()
    {
        botonIniciar.gameObject.SetActive(true);
    }

    public void DesactivaBotonIniciar()
    {
        botonIniciar.gameObject.SetActive(false);
    }

    public void ActivaBotonMayor()
    {
        botonMayor.gameObject.SetActive(true);
    }

    public void DesactivaBotonMayor()
    {
        botonMayor.gameObject.SetActive(false);
    }

    public void ActivaBotonMenor()
    {
        botonMenor.gameObject.SetActive(true);
    }

    public void DesactivaBotonMenor()
    {
        botonMenor.gameObject.SetActive(false);
    }

    public void ActivaBotonAcierto()
    {
        botonAcierto.gameObject.SetActive(true);
    }

    public void DesactivaBotonAcierto()
    {
        botonAcierto.gameObject.SetActive(false);
    }

    public void ButtonIniciar()
    {
        DesactivaBotonIniciar();
        DesactivaCuadroTexto();

        StartCoroutine(dialogo.MuestraTexto("El juego ha comenzado.\nEspera a que el jugador envíe su respuesta al número que cree que estás pensando..."));
    }
    
}
