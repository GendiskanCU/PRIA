using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class ManejadorPuntos : MonoBehaviourPunCallbacks
{

    public TMP_InputField VariableLocalInput;//Caja de entrada de texto

    public TextMeshProUGUI VariableLocalText;
    public TextMeshProUGUI VariableRedText;

    //Variable para modificar en red
    public string tomarDatosRival = "0";

    //Cuando se introducen valores en el InputField se sincronizan
    public void CambiarValor()
    {
        string valor = VariableLocalInput.text;
        VariableLocalText.text = valor;//El jugador local cambia el valor de su variable local
        //Avisa al otro jugador para que también cambie el valor que referencia a esa variable
        photonView.RPC(nameof(CambiarValorEnRed), RpcTarget.OthersBuffered, valor);
    }

    [PunRPC]
    private void CambiarValorEnRed(string nuevoValor)
    {
        VariableRedText.text = nuevoValor;//Cambia el valor del otro jugador
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
