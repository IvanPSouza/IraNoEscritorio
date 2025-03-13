using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class CarregamentoEConexao : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _txtInfo;

    // Start is called before the first frame update
    public void Connect()
    {
        // conecta no servido photon com as configurações predefinidas
        Debug.Log("Conectando...");
        _txtInfo.text = "Conectando..";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        _txtInfo.text = "Conectado ao servidor photon....";
        Debug.Log("Conectado ao servidor photon....");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _txtInfo.text = "Entrei no lobby do servidor photon..";
        Debug.Log("Entrei no lobby do servidor photon..");
        SceneManager.LoadScene("CreateGame");
    }
}
