using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Player : MonoBehaviour
{

    //Para controlar los desplazamientos
    public float speed;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        //Comprueba si es el propietario antes de capturar el componente actual
        if(GetComponent<PhotonView>().IsMine)
        {
            rig = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Comprueba si es el propietario antes de realizar el movimiento del personaje
        if(GetComponent<PhotonView>().IsMine)
        {
            rig.velocity = new Vector2( Input.GetAxis("Horizontal") * speed, rig.velocity.y);
        }
    }
}
