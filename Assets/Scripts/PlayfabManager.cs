using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        login();
    }

    void login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucess, OnError);
    }
    void OnSucess(LoginResult result)
    {
        Debug.Log("sccessful login/account create!");

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Erro while logging in/vreating account");
        Debug.Log(error.GenerateErrorReport());
    }
}
