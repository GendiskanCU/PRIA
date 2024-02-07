using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

//Script que controla las acciones del panel del jugador

public class AccionesJugador : MonoBehaviourPunCallbacks
{
       [SerializeField] private GameObject  botonEnviar;
       [SerializeField] private TMP_InputField cuadroTexto;

        private ControlDialogo dialogo;//Para controlar los textos que van a mostrarse

        private int variableJugador;//Para guardar el número que introduzca el jugador

    private void Start() {
        //Captura el script de control de diálogo del jugador
        dialogo = transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();
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

        //Envía el número del jugador al GameManager tanto del Jugador como del Anfitrión
        variableJugador = int.Parse(cuadroTexto.text);
        Debug.Log("Variable del jugador: " + variableJugador);
        photonView.RPC(nameof(SincronizaNumeroJugador), RpcTarget.AllBuffered, variableJugador);        

        GameObject.FindObjectOfType<GameManager>().ProximoEnJugar("anfitrión", "responde_al_jugador");
    }

    //El jugador recibe una respuesta del anfitrión
    public void RecibirRespuesta()
    {
        StartCoroutine(dialogo.MuestraTexto("¡Ya tenemos una respuesta!. El anfitrión dice que tu número "));

        //TODO: implementar diferente diálogo y acciones según la respuesta recibida        
        
    }

    //El jugador puede comenzar a jugar
    public void IniciaJuego()
    {
        ActivaCuadroTexto();
        ActivaBotonEnviar();

        StartCoroutine(dialogo.MuestraTexto("El anfitrión ha pensado en un número entre 1 y 100. Escribe en la caja " + 
        "de texto qué número crees que es y pulsa Enviar."));
    }

    [PunRPC]
    private void SincronizaNumeroJugador(int valor)
    {
        GameObject.FindObjectOfType<GameManager>().NumeroDelJugador = valor;
        Debug.Log(GameObject.FindObjectOfType<GameManager>().NumeroDelJugador);
    }
   
}
