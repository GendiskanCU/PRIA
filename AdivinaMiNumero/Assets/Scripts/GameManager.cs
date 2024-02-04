using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Paneles del anfitrión y del jugador
    [SerializeField] private GameObject panelAnfitrion;
    [SerializeField] private GameObject panelJugador;

    [SerializeField] private AccionesAnfitrion accionesAnfitrion;//Panel con las acciones de cada participante
    [SerializeField] private AccionesJugador accionesJugador;  
    private ControlDialogo dialogo;//Para controlar los textos que van a mostrarse

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivaPanelAnfitrion());
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

        yield return StartCoroutine(dialogo.MuestraTexto("¡Bienvenid@!! Eres el anfitrión del juego Adivina Mi Número. Piensa en un número entre 1 y 100.\n" + 
        "Escríbelo en el cuadro de texto y pulsa Iniciar"));

        AnfitrionIniciaJuego(); 
    }

    private void AnfitrionIniciaJuego()
    {       
        accionesAnfitrion.ActivaCuadroTexto();
        accionesAnfitrion.ActivaBotonIniciar();
    }




    /// <summary>
    /// Activa el panel del jugador y muestra el mensaje de bienvenida
    /// </summary>
    public void ActivaPanelJugador()
    {
        panelJugador.gameObject.SetActive(true);

        //Captura el script de control de diálogo del jugador
        dialogo = panelJugador.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();
        dialogo.MuestraTexto("Bienvenido, jugador");
    }
}
