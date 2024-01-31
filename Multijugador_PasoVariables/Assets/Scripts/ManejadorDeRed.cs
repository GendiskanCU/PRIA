using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class ManejadorDeRed : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI informacion;
    public static ManejadorDeRed manejadorRed;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        informacion.text = "Conectando al servidor";
        Debug.Log(informacion.text);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Sala", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }
}
