using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;

public class CarregamentoEConecao : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _txtInfo;

    // Start is called before the first frame update
    void Start()
    {
        //Conecta no servidor photon com as configurações pre definidas
        Debug.Log("Conectando...");
        _txtInfo.text = "Conectando...";
        PhotonNetwork.ConnectUsingSettings();

    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        _txtInfo.text = "Tentando Connectar...";
        Debug.Log("Tentando Connectar...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _txtInfo.text = "Entrado no servidor";
        Debug.Log("Entrado no servidor");
        SceneManager.LoadScene("CreateGame");
    }
}
