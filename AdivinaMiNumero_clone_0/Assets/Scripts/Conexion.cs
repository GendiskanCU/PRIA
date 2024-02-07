using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Conexion : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text textoInformacion;
    [SerializeField] private Button botonEntrarJuego;

    // Start is called before the first frame update
    void Start()
    {
        //Conexión al servidor
        textoInformacion.text = "Conectando al servidor. Espera un momento, por favor...";
        PhotonNetwork.ConnectUsingSettings();
        //Activa la sincronización de escenas
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Al conectar al master
    public override void OnConnectedToMaster()
    {
        //Activa el botón para entrar al juego
        textoInformacion.text ="Conectad@ al servidor.";
        botonEntrarJuego.interactable = true;
    }

    //Botón para entrar en la sala de juego
    public void BtnEntrarJuego()
    {
        textoInformacion.text = "Conectando a la sala de juego. Un momento, por favor...";
        botonEntrarJuego.interactable = false;

        //La sala tendrá un límite de dos jugadores
        RoomOptions opcionesSalaJuego = new RoomOptions() {MaxPlayers = 2};

        //Se crea la sala, o se une a ella si ya existe
        PhotonNetwork.JoinOrCreateRoom("salajuego", opcionesSalaJuego, TypedLobby.Default);
    }

    //Al entrar en la sala de juego
    public override void OnJoinedRoom()
    {
        textoInformacion.text = "Conectad@ a la sala de juego.";
        if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            textoInformacion.text += " Esperando a que se una otr@ jugad@r...";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando se hayan unido dos jugadores a la sala de juego se carga la siguiente escena
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel(1);
            Destroy(this);
        }
        
    }
}
