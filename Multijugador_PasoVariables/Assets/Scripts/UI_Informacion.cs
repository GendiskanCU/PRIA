using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class UI_Informacion : MonoBehaviourPunCallbacks, IPunObservable //Hay que hacerlo observable
{
    public TextMeshProUGUI informacion;//Texto del banner
    public int playerNum;//Num. del jugador

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SetPlayerOrEnemy");//Lanza la corutina que comprueba si somos el jugador o el enemigo
    }

    IEnumerator SetPlayerOrEnemy()
    {
        yield return new WaitForSeconds(3.0f);

        if(photonView.IsMine)
        {
            informacion.text = "Soy el jugador";
        }
        if(!photonView.IsMine)
        {
            informacion.text = "Soy el enemigo";
        }
    }

    //Hay que implementar la interfaz IPunObservable
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
