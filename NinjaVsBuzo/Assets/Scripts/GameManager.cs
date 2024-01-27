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
        if(PhotonNetwork.IsMasterClient)//Si es el jugador 1, el master
        {
            PhotonNetwork.Instantiate("Frog", new Vector3(-11f, 2.5f, 0f), Quaternion.identity);
        }
        else//Si es el jugador 2
        {
            PhotonNetwork.Instantiate("VirtualBoy", new Vector3(11f, 2.5f, 0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
