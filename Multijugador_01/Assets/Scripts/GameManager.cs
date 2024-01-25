using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Unity.Mathematics;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Comprueba si somos el cliente principal, y en caso afirmativo instancia al personaje adecuado
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("PlayerA", new Vector2(-6, 1), quaternion.identity);
        }
        else //Si no es el principal, instancia al otro personaje
        {
            PhotonNetwork.Instantiate("PlayerB", new Vector2(6, 1), quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
