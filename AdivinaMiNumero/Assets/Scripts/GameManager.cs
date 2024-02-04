using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Paneles del anfitrión y del jugador
    [SerializeField] private GameObject panelAnfitrion;
    [SerializeField] private GameObject panelJugador;

    //Para controlar los textos que van a mostrarse
    ControlDialogo dialogo;

    // Start is called before the first frame update
    void Start()
    {
        ActivaPanelJugador();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Activa el panel del Anfitrión y muestra el mensaje de bienvenida
    /// </summary>
    public void ActivaPanelAnfitrion()
    {
        panelAnfitrion.gameObject.SetActive(true);

        //Captura el script de control de diálogo del anfitrión
        dialogo = panelAnfitrion.gameObject.transform.GetChild(0).gameObject.GetComponent<ControlDialogo>();
        dialogo.MuestraTexto("Bienvenido, anfitrión");
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
