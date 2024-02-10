using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public float jumForce = 200;
    private Rigidbody2D rig;
    private Animator anim;

    //Para controlar cuándo puede saltar el personaje
    private bool canJump;

    //Punto de disparo
    private GameObject shootPoint;
    //Posiciones del punto de disparo según hacia dónde mira el personaje
    [SerializeField] private Vector3 initialPositionShootPoint = new Vector3(0.51f, -0.61f, 0);
    [SerializeField] private Vector3 flipPositionShootPoint = new Vector3(-0.62f, -0.61f, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)//Solo captura los componentes cuando sean los míos
        {
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            shootPoint = transform.GetChild(0).gameObject;
            shootPoint.transform.localPosition = initialPositionShootPoint;

            //Se le asigna la cámara principal (hay que tener en cuenta que las cámaras no se sincronizan)
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) +transform.forward * -10;
        }

        //Permite el salto
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)//Solo habrá movimiento si son mis componentes
        {
            //Movimiento. Se le da velocidad, en ambos ejes porque si no se quedaría parado
            rig.velocity = (transform.right * speed * Input.GetAxis("Horizontal")) + (transform.up * rig.velocity.y);

            //Giro del personaje al comenzar a moverse
            /*Así se haría sin Photon
            if (rig.velocity.x > 0.1f)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (rig.velocity.x < -0.1f)
                GetComponent<SpriteRenderer>().flipX = true;*/

            //Se debe utilizar el método RPC de Photon para que la rotación se sincronice adecuadamente
            //Se evita que RPC se ejecute en cada frame con la segunda condición
            if(rig.velocity.x > 0.1f && GetComponent<SpriteRenderer>().flipX)
            {
                 GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
                 //Recoloca el punto de disparo
                 shootPoint.transform.localPosition = (shootPoint.transform.localPosition == initialPositionShootPoint ) ? flipPositionShootPoint : initialPositionShootPoint;
            }
               
            else if(rig.velocity.x < -0.1f && !GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);
                //Recoloca el punto de disparo
                shootPoint.transform.localPosition = (shootPoint.transform.localPosition == initialPositionShootPoint ) ? flipPositionShootPoint : initialPositionShootPoint;
            }
                

            //Salto
            if (canJump && Input.GetButtonDown("Jump"))
            {
                rig.AddForce(transform.up * jumForce);
                canJump = false;
            }
                
            //Disparo. Se debe sincronizar en todos los clientes
            if(Input.GetButtonDown("Fire1"))
            {                
                GameObject shuriken = PhotonNetwork.Instantiate("Shuriken", shootPoint.transform.position, shootPoint.transform.rotation);
                //shuriken.GetComponent<Rigidbody2D>().AddForce(shootPoint.transform.forward * 2.0f, ForceMode2D.Impulse);                
            }

            //Animación
            anim.SetFloat("velocityX", Mathf.Abs(rig.velocity.x));
            anim.SetFloat("velocityY", rig.velocity.y);
        }
    }

    //Método que rota el sprite para uso con RPC
    [PunRPC]
    public void RotateSprite(bool rotate)
    {
        GetComponent<SpriteRenderer>().flipX = rotate;
        //Recoloca el punto de disparo al otro lado        
        //transform.GetChild(0).gameObject.transform.localPosition =
            //(shootPoint.transform.localPosition == initialPositionShootPoint ) ? flipPositionShootPoint : initialPositionShootPoint;
    }

    
    //Método para controlar cuando el personaje toque suelo
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Floor"))
            canJump = true;
    }
}
