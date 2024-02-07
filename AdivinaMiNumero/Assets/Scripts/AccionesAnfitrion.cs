using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System.Data;

//Script que conterola las acciones del panel del anfitrión

public class AccionesAnfitrion : MonoBehaviour
{
    [SerializeField] private GameObject botonIniciar, botonMayor, botonMenor, botonAcierto;
    [SerializeField] private TMP_InputField cuadroTexto;
   
    private ControlDialogo dialogo;//Para controlar los textos que van a mostrarse

    private int variableAnfitrion;//Para guardar el número que introduzca el anfitrión

    GameManager gameManager;//Script que controla el juego
    


    private void Start() {
        //Captura el script de control de diálogo del anfitrión
        dialogo = transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();

        //Captura el script que controla el juego
        gameManager = GameObject.FindObjectOfType<GameManager>();
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

    //El anfitrión pulsa sobre el botón Iniciar
    public void ButtonIniciar()
    {
        DesactivaBotonIniciar();
        DesactivaCuadroTexto();
        
        //Envía el número a adivinar al GameManager
        variableAnfitrion = int.Parse(cuadroTexto.text);
        gameManager.CambiaValorAnfitrion(variableAnfitrion);

        Debug.Log("Número a adivinar: " + gameManager.NumeroAAdivinar);

        //Indica al manager de quién es el turno siguiente y la acción que debe realizar
        gameManager.ProximoEnJugar("jugador", "inicia_juego");
        
        StartCoroutine(dialogo.MuestraTexto("El juego ha comenzado.\nEspera a que el jugador envíe su respuesta al número que cree que estás pensando..."));
    }

    //El anfitrión pulsa sobre el botón Es Mayor
    public void BotonMayor()
    {
        if(!(gameManager.NumeroAAdivinar > gameManager.NumeroDelJugador))
        {
            //Si el anfitrión intenta engañar al jugador
            StartCoroutine(dialogo.MuestraTexto("mmm... Creo que esa no es la respuesta correcta. ¡Inténtalo de nuevo!"));
        }
        else
        {
            DesactivaBotonMayor();
            DesactivaBotonMenor();
            DesactivaBotonAcierto();

            StartCoroutine(dialogo.MuestraTexto("¡Por supuesto! Envío tu respuesta al jugador. Esperemos un poco a ver qué número cree ahora que es..."));

            //Indica al manager de quién es el turno siguiente y la acción que debe realizar
            gameManager.ProximoEnJugar("jugador", "escribe_mayor");
            
        }
    }

    //El anfitrión pulsa sobre el botón Es Menor
    public void BotonMenor()
    {
        if(!(gameManager.NumeroAAdivinar < gameManager.NumeroDelJugador))
        {
            //Si el anfitrión intenta engañar al jugador
            StartCoroutine(dialogo.MuestraTexto("mmm... Creo que esa no es la respuesta correcta. ¡Inténtalo de nuevo!"));
        }
        else
        {            
            DesactivaBotonMayor();
            DesactivaBotonMenor();
            DesactivaBotonAcierto();

            StartCoroutine(dialogo.MuestraTexto("¡Perfecto! Envío tu respuesta al jugador. Vamos a esperar a que nos diga ahora qué número cree que es..."));

            //Indica al manager de quién es el turno siguiente y la acción que debe realizar
            gameManager.ProximoEnJugar("jugador", "escribe_menor");        
        }
    }

    //El anfitrión pulsa sobre el botón Has Acertado
    public void BotonAcierto()
    {

        Debug.Log("Número del jugador: " + gameManager.NumeroDelJugador + "\nNúmero del anfitrión: " + 
        gameManager.NumeroAAdivinar);
        if( gameManager.NumeroAAdivinar != gameManager.NumeroDelJugador)
        {            
            //Si el anfitrión intenta engañar al jugador
            StartCoroutine(dialogo.MuestraTexto("mmm... Creo que esa no es la respuesta correcta. ¡Inténtalo de nuevo!"));
        }
        else
        {
            DesactivaBotonMayor();
            DesactivaBotonMenor();
            DesactivaBotonAcierto();

            StartCoroutine(dialogo.MuestraTexto("¡Oh, la la!!!! El jugador nos ha vencido... Si quieres la revancha, escribe un nuevo número y pulsa Iniciar"));

            //Indica al manager de quién es el turno siguiente y la acción que debe realizar
            gameManager.ProximoEnJugar("jugador", "ha_acertado");            
        }
    }

    //El anfitrión puede iniciar un nuevo juego
    public void IniciaJuego()
    {
        ActivaCuadroTexto();
        ActivaBotonIniciar();
    }

    //El anfitrión recibe un número del jugador y debe responderle
    public void Responder(int numeroDelJugador)
    {
        StartCoroutine(dialogo.MuestraTexto("¡Oh!, ya tenemos una respuesta. El jugador cree que tu número es: " + numeroDelJugador +
        "\n ¿Qué quieres decirle? (Pulsa el botón adecuado)")); 
        ActivaBotonMayor();
        ActivaBotonMenor();
        ActivaBotonAcierto();        
    }
       
    
}
