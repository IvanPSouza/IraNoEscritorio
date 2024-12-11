using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<GameObject> _playersPanels;
    [SerializeField] private TMP_Text _textPlayerCount;
    int _playersCount;


    private void Awake()
    {
        ChecaJogadores();
    }

    private void Update()
    {
        ChecaJogadores();
    }

    private void ChecaJogadores()
    {
        _playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Player[] playersList = PhotonNetwork.PlayerList;

        if (_playersCount <= 0)
        {
            return;
        }

        _textPlayerCount.text = _playersCount.ToString();

        for (int i = 0; i < _playersCount; i++)
        {
            _playersPanels[i].SetActive(true);
            _playersPanels[i].GetComponentInChildren<TMP_Text>().text = playersList[i].NickName;
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}