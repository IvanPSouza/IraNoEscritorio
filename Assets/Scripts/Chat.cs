using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Chat : MonoBehaviour
{
    [SerializeField] private TMP_InputField _textMensagem;
    [SerializeField] private GameObject _conteudo;
    [SerializeField] private GameObject _Mensagem;
    private PhotonView _photonView;


    // Start is called before the first frame update
    void Start()
    {
         _photonView = GetComponent<PhotonView>();  
    }

    public void EnviarMensagem()
    {
        _photonView.RPC(
            "RecebeMensagem",
            RpcTarget.All,
            PhotonNetwork.LocalPlayer.NickName + ": " + _textMensagem.text
            );


        _textMensagem.text = "";
    }

    [PunRPC]
    public void RecebeMensagem(string mensagemRecebida)
    {
        GameObject mensagem = Instantiate(_Mensagem, _conteudo.transform);

        mensagem.GetComponent<TMP_Text>().text = mensagemRecebida;


        mensagem.GetComponent<RectTransform>().SetAsFirstSibling();
    }
        
}
