using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System;

//Script que controla las acciones del panel del jugador

public class AccionesJugador : MonoBehaviourPunCallbacks
{
       [SerializeField] private GameObject  botonEnviar;
       [SerializeField] private TMP_InputField cuadroTexto;

        private ControlDialogo dialogo;//Para controlar los textos que van a mostrarse

        private GameManager gameManager;//Script que controla el juego 

        private int variableJugador;//Para guardar el número que introduzca el jugador

    private void Start() {
        //Captura el script de control de diálogo del jugador
        dialogo = transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();

        //Captura el script que controla el juego
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

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

    
    //El jugador pulsa sobre el botón Enviar
    public void BotonEnviar()
    {
        DesactivaBotonEnviar();
        DesactivaCuadroTexto();
        StartCoroutine(dialogo.MuestraTexto("¡Perfecto!!! Envío tu respuesta al anfitrión. Vamos a esperar a ver qué nos dice sobre ella..."));

        
        variableJugador = int.Parse(cuadroTexto.text);
        Debug.Log("Variable del jugador: " + variableJugador);
        //Cambia el número del jugador en el GameManager tanto del Jugador como del Anfitrión
        gameManager.CambiaValorJugador(variableJugador);

        //Indica al manager de quién es el turno siguiente y la acción que debe realizar        
        gameManager.ProximoEnJugar("anfitrión", "responde_al_jugador");
    }

    //El jugador recibe una respuesta del anfitrión
    public void RecibirRespuesta(string resultado)
    {
        switch(resultado)
        {
            case("mayor"):
                StartCoroutine(dialogo.MuestraTexto("¡Ya tenemos respuesta!: el anfitrión dice que su número es más grande. Prueba de nuevo con uno mayor"));
                ActivaCuadroTexto();
                ActivaBotonEnviar();
            break;

            case("menor"):
                StartCoroutine(dialogo.MuestraTexto("¡Ya tenemos una respuesta!: el anfitrión dice que su número es menor... Prueba a poner uno más  pequeño"));
                ActivaCuadroTexto();
                ActivaBotonEnviar();
            break;

            case("igual"):
                StartCoroutine(dialogo.MuestraTexto("¡Ya tenemos una respuesta!. El anfitrión dice que...¡Has adivinado el número!!! ¡¡Enhorabuena!!!"));

                //Indica al manager que el juego ha finalizado, el anfitrión podría comenzar un nuevo juego
                gameManager.ProximoEnJugar("anfitrión", "fin_juego");
            break;
        }        
        
    }

    //El jugador puede comenzar a jugar
    public void IniciaJuego()
    {
        ActivaCuadroTexto();
        ActivaBotonEnviar();

        StartCoroutine(dialogo.MuestraTexto("El anfitrión ha pensado en un número entre 1 y 100. Escribe en la caja " + 
        "de texto qué número crees que es y pulsa Enviar."));
    }

   
}
