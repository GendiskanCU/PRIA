using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    //Paneles del anfitrión y del jugador
    [SerializeField] private GameObject panelAnfitrion;
    [SerializeField] private GameObject panelJugador;

    [SerializeField] private AccionesAnfitrion accionesAnfitrion;//Script con las acciones de cada participante
    [SerializeField] private AccionesJugador accionesJugador;  
    private ControlDialogo dialogo;//Para controlar los textos que van a mostrarse

    private int numeroAAdivinar;//Para guardar el número a adivinar    
    private int numeroDelJugador;//Para guardar el número que vaya escribiendo el jugador

    public int NumeroAAdivinar { get => numeroAAdivinar; set => numeroAAdivinar = value; }
    public int NumeroDelJugador { get => numeroDelJugador; set => numeroDelJugador = value; }
    

    // Start is called before the first frame update
    void Start()
    {
        //Comprueba si es el master o el jugador y activa el panel adecuado
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(ActivaPanelAnfitrion());
        }
        else
        {
            StartCoroutine(ActivaPanelJugador());
        }        
    }

    

    /// <summary>
    /// Activa el panel del Anfitrión, prepara sus componentes, muestra el mensaje de bienvenida
    /// e inicia el juego/// 
    /// </summary>
    public IEnumerator ActivaPanelAnfitrion()
    {
        panelAnfitrion.gameObject.SetActive(true);

        //Captura el script de control de diálogo del anfitrión
        dialogo = panelAnfitrion.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();        

        yield return StartCoroutine(dialogo.MuestraTexto("¡Bienvenid@!! Eres el anfitrión de Adivina Mi Número. Piensa en un número entre 1 y 100.\n" + 
        "Escríbelo en el cuadro de texto y pulsa Iniciar"));

        accionesAnfitrion.IniciaJuego();        
    }


    /// <summary>
    /// Activa el panel del jugador y muestra el mensaje de bienvenida
    /// </summary>
    public IEnumerator ActivaPanelJugador()
    {
        panelJugador.gameObject.SetActive(true);

        //Captura el script de control de diálogo del jugador
        dialogo = panelJugador.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();
        
        yield return StartCoroutine(dialogo.MuestraTexto("¡Bienvenid@!! Eres el jugador de Adivina Mi Número." + 
        " Vamos a esperar un momento a que el anfitrión dé comienzo al juego..."));        
    }


    //Para enviar la señal de sincronización en red sobre a quién le corresponde turno y qué acción
    public void ProximoEnJugar(string turnoDe, string accion)
    {
        if(turnoDe == "jugador")
        {
            switch(accion)
            {
                case("inicia_juego"):
                    Debug.Log("El jugador puede comenzar a jugar");                    
                    photonView.RPC(nameof(JugadorIniciaJuego), RpcTarget.OthersBuffered);
                break;                
            }
        }

        if(turnoDe == "anfitrión")
        {
            switch(accion)
            {                
                case("responde_al_jugador"):                
                    Debug.Log("Entra aquí");
                    photonView.RPC(nameof(AnfitrionRespondeAJugador), RpcTarget.OthersBuffered);                    
                break;
            }
        }
    }



    //Métodos de sincronización de las acciones del Jugador
    [PunRPC]
    private void JugadorIniciaJuego()
    {
        accionesJugador.IniciaJuego();
    }



    //Métodos de sincronización de las acciones del Anfitrión
    [PunRPC]
    private void AnfitrionRespondeAJugador()
    {
        Debug.Log("El anfitrión debe responder al número que ha dicho el jugador: " + numeroDelJugador);        
        accionesAnfitrion.Responder(numeroDelJugador);
    }
        
}
