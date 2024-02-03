using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //La instanciación de cada personaje se hace con Photon

        //Utiliza el identificador ActorNumber para saber qué jugador soy
        int playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        //Instancia el personaje adecuado según quién sea el jugador
        switch(playerNumber)
        {
            case 1:
                PhotonNetwork.Instantiate("Frog", new Vector3(-11f, 2.5f, 0f), Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate("VirtualBoy", new Vector3(11f, 2.5f, 0f), Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate("MaskDude", new Vector3(0, 2.5f, 0f), Quaternion.identity);
                break;
        }

         /*
        if(PhotonNetwork.IsMasterClient)//Si es el jugador 1, el master
        {
            PhotonNetwork.Instantiate("Frog", new Vector3(-11f, 2.5f, 0f), Quaternion.identity);
        }
        else//Si es el jugador 2
        {
            PhotonNetwork.Instantiate("VirtualBoy", new Vector3(11f, 2.5f, 0f), Quaternion.identity);
        } */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
