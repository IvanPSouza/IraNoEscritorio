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
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucess, OnError);
    }
    void OnSucess(LoginResult result)
    {
        Debug.Log("sccessful login/account create!");

        //UpdateDisplayName(result.InfoResultPayload.PlayerProfile.user);

        //string name = null;
        //if (result.InfoResultPayload.PlayerProfile != null)
        //name = result.InfoResultPayload.PlayerProfile.DisplayName;

     //   if (name == null)
     //       nameWindow.Setctive(true);

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Erro while logging in/vreating account");
        Debug.Log(error.GenerateErrorReport());
    }

    private void UpdateDisplayName(string displayName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            },
            (UpdateUserTitleDisplayNameResult result) =>
            {
                Debug.Log("Display name updated.");
            },
            (PlayFabError error) =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }
}
