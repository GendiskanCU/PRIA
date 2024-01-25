using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Incluimos las dependencias normales
using Photon.Pun;
using Photon.Realtime;

public class Connection : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //Conectamos al Master
        PhotonNetwork.ConnectUsingSettings();
        //Activamos la sincronización al cambiar de escena
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Método para controlar la acción del botón
    public void ButtonConnect()
    {
        RoomOptions options = new RoomOptions() {MaxPlayers = 2};

        //Se une o crea si no existe, a la habitación 1 con las opciones especificadas y tipo de espera por defecto
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }

    //Al entrar en una sala
    override public void OnJoinedRoom()
    {
        Debug.Log("Conectado a la sala " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Hay " + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores.");
    }

    //Al conectar al Master
    override public void OnConnectedToMaster()
    {
        Debug.Log("Conectado al Master.");
    }

    private void Update() {

        //Cuando haya suficientes jugadores conectados, pasamos a la siguiente escena y destruimos el actual
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel(1);
            Destroy(this);
        }
    }
}
