using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System;
using Random = UnityEngine.Random;
using WebSocketSharp;
using UnityEditor.VersionControl;


public class CriarEConectar : MonoBehaviourPunCallbacks
{
    #region Campos Privados

    [SerializeField] private TMP_InputField _nickname;
    [SerializeField] private TMP_InputField _roomID;
    public string _roomName;
    private RoomOptions _options = new RoomOptions();

    #endregion

    #region Metodos Unity

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _options.MaxPlayers = 2;
        _options.IsVisible = true;
        _options.IsOpen = true;

        _nickname.text = PlayFabLogin.PFL.Nickname;


    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            PlayFabLeaderboard PFLeaderboard = FindObjectOfType<PlayFabLeaderboard>();
            //PFLeaderboard.UpdateLeaderboard();
            PFLeaderboard.RecuperarLeaderboard();
        }
    }

    #endregion

    #region Metodos Publicos

    public string GeraCodigo()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string code = "";
        int digitCount = 6;
        for (int i = 0; i < digitCount; i++)
        {
            code += chars[UnityEngine.Random.Range(0, chars.Length)];
        }
        Debug.Log(code);
        return code;
    }

    public void CriaSala(string roomName = "")
    {
        roomName = !roomName.IsNullOrEmpty() ? roomName : GeraCodigo();

        Debug.Log("SALA CRIADA");

        PhotonNetwork.CreateRoom(roomName, _options);
    }

    public void JoinRoom()
    {
        if (_roomID.text == null)
        {
            return;
        }

        PhotonNetwork.JoinRoom(_roomID.text);
    }

    public void CriarOuEntrarSala(bool isHost, string nomeSala)
    {
        if (isHost)
        {
            PhotonNetwork.CreateRoom(nomeSala, _options);
        }
        else
        {
            PhotonNetwork.JoinRoom(nomeSala);
        }

    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void MudaNome()
    {
        PhotonNetwork.LocalPlayer.NickName = _nickname.text;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
    }

    #endregion

    #region Callbacks Photon

    public override void OnJoinedRoom()
    {
        MudaNome();

        Debug.Log(PhotonNetwork.CurrentRoom.Name);

        PhotonNetwork.LoadLevel("LobbyGame");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"[PhotonNetwork] Falha ao entrar na sala, erro {returnCode}: {message}, vamos tentar novamente em 2s.");

        // coroutine para ficar tentando entrar na sala
        StartCoroutine(TentaEntrarSala(_roomName));
    }

    private IEnumerator TentaEntrarSala(string nomeSala)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log($"[PhotonNetwork] Tentando entrar na sala {nomeSala}");
        PhotonNetwork.JoinRoom(nomeSala);
    }

    #endregion
}